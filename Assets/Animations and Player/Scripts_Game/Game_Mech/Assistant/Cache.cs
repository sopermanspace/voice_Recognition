using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Cache : MonoBehaviour
{

 // Define a dictionary to store cached data
    private static Dictionary<string, object> _cache = new Dictionary<string, object>();

    // Define a method to store data in the cache
    public static void SetCache(string key, object data)
    {
        // Add or update the data in the cache dictionary
        if (_cache.ContainsKey(key))
        {
            _cache[key] = data;
        }
        else
        {
            _cache.Add(key, data);
        }
    }

    // Define a method to retrieve data from the cache
    public static object GetCache(string key)
    {
        // Check if the key exists in the cache dictionary
        if (_cache.ContainsKey(key))
        {
            // Return the cached data
            return _cache[key];
        }
        else
        {
            // Return null if the key is not found in the cache
            return null;
        }
    }

    // Define a method to clear the cache
    public static void ClearCache()
    {
        _cache.Clear();
    }

}

