using System;
using System.IO;
using UnityEngine;

namespace Serialization
{
	public static class XmlSerializer
	{
		/// <summary>
		/// Serializes the object to the specified path.
		/// </summary>
		public static void Serialize<T>( T obj, string path )
		{
			try
			{
				if( !path.EndsWith(".xml") )
				{
					path += ".xml";
				}

				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
				FileStream stream = new FileStream(path, FileMode.Create);
				serializer.Serialize(stream, obj);
				stream.Close();
			}
			catch( Exception e )
			{
				Debug.LogError(e.Message + e.StackTrace);
			}
		}

		/// <summary>
		/// Deserializes the file located at path.
		/// </summary>
		public static T Deserialize<T>( string path )
		{
			try
			{
				if( !path.EndsWith(".xml") )
				{
					path += ".xml";
				}

				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
				FileStream stream = new FileStream(path, FileMode.Open);
				T obj = (T)serializer.Deserialize(stream);
				stream.Close();
				return obj;
			}
			catch( Exception e )
			{
				Debug.LogError(e.Message + e.StackTrace);
				return default(T);
			}
		}

		/// <summary>
		/// Deserializes the file located at a resources path.
		/// </summary>
		public static T DeserializeResource<T>( string path )
		{
			try
			{
				if( path.EndsWith(".xml") )
				{
					// Remove the extension.
					path = path.Substring(0, path.LastIndexOf(".xml"));
				}

				// Replace backward slashes with forward slashes.
				path = path.Replace('\\', '/');

				TextAsset textAsset = (TextAsset)Resources.Load(path);
				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
				StringReader reader = new StringReader(textAsset.text);
				T obj = (T)serializer.Deserialize(reader);
				reader.Close();
				return obj;
			}
			catch( Exception e )
			{
				Debug.LogError(e.Message + e.StackTrace);
				return default(T);
			}
		}
	}
}