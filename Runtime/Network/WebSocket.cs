using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;
using UnityEngine;

public class WebSocket : MonoBehaviour
{
	// Constants.
	public const int READ_BUFFER_SIZE = 4 * 1024;
	public const int MAX_READ_SIZE = 1 * 1024 * 1024;

	// Delegates.
	public delegate void OnOpenDelegate(string url);
	public delegate void OnMessageDelegate(string message);
	public delegate void OnCloseDelegate(string url);

	// Events.
	public OnOpenDelegate onOpen;
	public OnMessageDelegate onMessage;
	public OnCloseDelegate onClose;

	private string url = "";
	private ClientWebSocket socket;
	public WebSocketState state { get; private set; }
	private UTF8Encoding encoder = new UTF8Encoding();

	private ConcurrentQueue<string> receiveQueue = new ConcurrentQueue<string>();
	private BlockingCollection<ArraySegment<byte>> sendQueue = new BlockingCollection<ArraySegment<byte>>();

	private Thread receiveThread;
	private Thread sendThread;

	private void Update()
	{
		// Pool socket state.
		if (socket != null && state != socket.State)
		{
			state = socket.State;
			switch (state)
			{
				case WebSocketState.Open:
					{
						onOpen?.Invoke(this.url);
						break;
					}
				case WebSocketState.Closed:
					{
						onClose?.Invoke(this.url);
						this.url = "";
						socket = null;
						break;
					}
				default:
					{
						break;
					}
			}
		}

		// Pool receive message queue.
		string json;
		while (receiveQueue.TryDequeue(out json))
		{
			onMessage.Invoke(json);
		}
	}

	private async void OnDestroy()
	{
		await Disconnect();
	}

	public async Task Connect(string url)
	{
		if (socket != null && (socket.State != WebSocketState.None || socket.State != WebSocketState.Closed))
		{
			return;
		}

		try
		{
			ClearQueues();

			this.url = url;
			socket = new ClientWebSocket();
			await socket.ConnectAsync(new Uri(this.url), CancellationToken.None);
			while (socket.State == WebSocketState.Connecting)
			{
				Task.Delay(50).Wait();
			}

			if (socket.State == WebSocketState.Open)
			{
				//Debug.Log("Client connected to " + this.url);

				receiveThread = new Thread(ReceiveThread);
				sendThread = new Thread(SendThread);
				receiveThread.Start();
				sendThread.Start();
			}
			else
			{
				Debug.LogError("Connect socket state: " + socket.State);
			}
		}
		catch (Exception exception)
		{
			Debug.LogError(exception.Message);
		}
	}

#pragma warning disable CS1998
	public async Task Disconnect()
	{
		if (socket == null || socket.State == WebSocketState.Closed)
		{
			return;
		}

		try
		{
#pragma warning disable CS4014
			socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
#pragma warning restore CS4014
			socket.Dispose();
			if (socket.State == WebSocketState.Closed)
			{
				//Debug.Log("Client disconnected from " + this.url);

				receiveThread.Abort();
				sendThread.Abort();
				ClearQueues();
			}
			else
			{
				Debug.LogError("Disconnect socket state: " + socket.State);
			}
		}
		catch (Exception exception)
		{
			Debug.LogError(exception.Message);
		}
	}
#pragma warning restore CS1998

	private async Task<string> Receive()
	{
		byte[] buffer = new byte[READ_BUFFER_SIZE];
		MemoryStream stream = new MemoryStream(); // Stores an unknown number of chunks.
		ArraySegment<byte> arrayBuffer = new ArraySegment<byte>(buffer);

		WebSocketReceiveResult chunk = null;
		if (socket.State == WebSocketState.Open)
		{
			do
			{
				chunk = await socket.ReceiveAsync(arrayBuffer, CancellationToken.None);
				stream.Write(arrayBuffer.Array, arrayBuffer.Offset, chunk.Count);

				//Debug.Log("Chunk size: " + chunkResult.Count);
				if (chunk.Count > MAX_READ_SIZE)
				{
					Debug.LogError("Received message is bigger than expected (" + chunk.Count + " > " + MAX_READ_SIZE + ")");
				}
			}
			while (!chunk.EndOfMessage);

			stream.Seek(0, SeekOrigin.Begin);
			if (chunk.MessageType == WebSocketMessageType.Text) // UTF-8 JSON type messages.
			{
				return MemoryStreamToString(stream, Encoding.UTF8);
			}
		}

		return "";
	}

	public void Send(string message)
	{
		if (socket == null || socket.State != WebSocketState.Open)
		{
			return;
		}

		//Debug.Log("Sent: " + message);

		byte[] buffer = encoder.GetBytes(message);

		ArraySegment<byte> sendBuffer = new ArraySegment<byte>(buffer);
		sendQueue.Add(sendBuffer);
	}

	private async void ReceiveThread()
	{
		string message;
		while (true)
		{
			message = await Receive();
			if (message != null && message.Length > 0)
			{
				receiveQueue.Enqueue(message);

				//Debug.Log("Received: " + message);
			}
			else
			{
				Task.Delay(50).Wait();
			}
		}
	}

	private async void SendThread()
	{
		ArraySegment<byte> sendBuffer;
		while (true)
		{
			while (!sendQueue.IsCompleted)
			{
				sendBuffer = sendQueue.Take();

				await socket.SendAsync(sendBuffer, WebSocketMessageType.Text, true /* end of message */, CancellationToken.None);
			}
		}
	}

	private void ClearQueues()
	{
		string receiveItem;
		while (!receiveQueue.IsEmpty)
		{
			receiveQueue.TryDequeue(out receiveItem);
		}

		ArraySegment<byte> sendItem;
		while (sendQueue.Count != 0)
		{
			sendQueue.TryTake(out sendItem);
		}
	}

	private static string MemoryStreamToString(MemoryStream stream, Encoding encoding)
	{
		string str = "";
		if (encoding == Encoding.UTF8)
		{
			using (StreamReader reader = new StreamReader(stream, encoding))
			{
				str = reader.ReadToEnd();
			}
		}

		return str;
	}
}