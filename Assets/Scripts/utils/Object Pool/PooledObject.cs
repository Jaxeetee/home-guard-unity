using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtils
{
    public static class PooledObject
    {
        private static Dictionary<string, ObjectPool> pooledDictionary = new Dictionary<string, ObjectPool>();

        public static void NewObjectPool(string key, GameObject item, GameObject parent = null, int amtOfCopies = 10)
        {
            pooledDictionary.Add(key, new ObjectPool(item, parent, amtOfCopies));
        }

        public static GameObject GetObject(string key)
        {
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
