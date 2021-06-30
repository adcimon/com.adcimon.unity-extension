using System;
using System.IO;
using UnityEngine;

namespace Serialization
{
    public static class JsonSerializer
    {
        /// <summary>
        /// Serializes the object to the specified path.
        /// </summary>
        public static void Serialize<T>( T obj, string path )
        {
            try
            {
                if( !path.EndsWith(".json") )
                {
                    path += ".json";
                }

                string content = JsonUtility.ToJson(obj);
                StreamWriter stream = new StreamWriter(path);
                stream.Write(content);
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
                if( !path.EndsWith(".json") )
                {
                    path += ".json";
                }

                StreamReader stream = new StreamReader(path);
                string content = stream.ReadToEnd();
                stream.Close();
                return JsonUtility.FromJson<T>(content);
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
                if( path.EndsWith(".json") )
                {
                    // Remove the extension.
                    path = path.Substring(0, path.LastIndexOf(".json"));
                }

                // Replace backward slashes with forward slashes.
                path = path.Replace('\\', '/');

                TextAsset textAsset = (TextAsset)Resources.Load(path);
                return JsonUtility.FromJson<T>(textAsset.text);
            }
            catch( Exception e )
            {
                Debug.LogError(e.Message + e.StackTrace);
                return default(T);
            }
        }
    }
}