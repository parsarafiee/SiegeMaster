using System;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class ObjectPool
    {
        #region Singleton
        private static ObjectPool _instance;

        private ObjectPool()
        {
            _poolDict = new Dictionary<ValueType, Stack<IPoolable>>();
        }

        public static ObjectPool Instance
        {
            get { return _instance ??= new ObjectPool(); }
        }
        #endregion

        private readonly Dictionary<ValueType, Stack<IPoolable>> _poolDict;
        
        public void Pool(ValueType componentType, IPoolable toPool)
        {
            if(!_poolDict.ContainsKey(componentType))
                _poolDict.Add(componentType, new Stack<IPoolable>());
            
            _poolDict[componentType].Push(toPool);
            toPool.Pool();
            
        }

        public IPoolable Depool(ValueType componentType)
        {
            if (!_poolDict.ContainsKey(componentType))
                return null; 

            if (_poolDict[componentType].Count <= 0) return null;
            _poolDict[componentType].Peek().Depool();
            return _poolDict[componentType].Pop();
        }
    }
}

