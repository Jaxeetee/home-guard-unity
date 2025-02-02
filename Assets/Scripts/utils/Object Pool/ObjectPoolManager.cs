using System.Collections;
using System.Collections.Generic;
using MyUtils;
using UnityEngine;


namespace MyUtils
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private static Dictionary<string, PooledObjectItem> pooledDictionary = new Dictionary<string, PooledObjectItem>();

        public static void CreatePool(string key, GameObject item, GameObject parent = null, int amtOfCopies = 10)
        {
            pooledDictionary.Add(key, new PooledObjectItem(item, parent, amtOfCopies));
        }

        public static GameObject GetObject(string key)
        {
            Debug.Log(pooledDictionary.Keys.Count);
            return pooledDictionary.TryGetValue(key, out var pool) ? pool.GetObject() : null;
        }

        public static void ReturnToPool(string key, GameObject returnObject)
        {
            if (pooledDictionary.TryGetValue(key, out var pool))
            {
                pool.ReturnToPool(returnObject);
            }
        }

    }

}
