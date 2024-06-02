using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{
	public static class BinarySerializer
	{
		/// <summary>
		/// Serializes the object into a byte array.
		/// </summary>
		public static byte[] Serialize<T>(this T obj)
		{
			MemoryStream stream = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, obj);
			stream.Position = 0;
			return stream.ToArray();
		}

		/// <summary>
		/// Deserializes the specified byte array into an object.
		/// </summary>
		public static T Deserialize<T>(this byte[] byteArray)
		{
			MemoryStream stream = new MemoryStream(byteArray);
			BinaryFormatter formatter = new BinaryFormatter();
			stream.Position = 0;
			return (T)formatter.Deserialize(stream);
		}
	}
}