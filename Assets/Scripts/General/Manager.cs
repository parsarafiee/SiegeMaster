using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable InconsistentNaming

namespace General
{
    #region Interfaces and Abstract Classes for Managers

    public interface IManager : IUpdatable
    {
        public abstract void Clean();
    }


    /// <summary>
    /// Classes that inherit that class are Singleton Manager.
    /// </summary>
    public abstract class WrapperManager : IManager
    {
        public abstract void Init();
        public abstract void PostInit();
        public abstract void Refresh();
        public abstract void FixedRefresh();
        public abstract void LateRefresh();

        public abstract void Clean();
    }

    public interface ICollectionManager<in T>
    {
        public abstract void Add(T obj);
        public abstract void Remove(T obj);
    }

    #endregion

    #region Manager<T> (Generic Manager that can be instantiated, not a Singleton)

    /// <summary>
    /// Manager that can manage any collection of any type of object. can be instantiated with the new operator.
    /// </summary>
    /// <typeparam name="T">Type of  the objects to Manage</typeparam>
    public class Manager<T> : IManager, ICollectionManager<T> where T : IUpdatable
    {
        #region Variables & Properties

        protected readonly HashSet<T> _collection;
        private readonly Stack<T> _toAdd;
        private readonly Stack<T> _toRemove;
        
        #endregion

        public Manager()
        {
            _collection = new HashSet<T>();
            _toAdd = new Stack<T>();
            _toRemove = new Stack<T>();
        }

        #region Public Methods

        public void Init()
        {
            AddStackItemsToCollection();
            InitCollection();
        }

        public void PostInit()
        {
            PostInitCollection();
        }

        public void Refresh()
        {
            RemoveStackItemsFromCollection();
            UpdateCollection();
            AddStackItemsToCollection();
        }

        public void FixedRefresh()
        {
            FixedUpdateCollection();
        }

        public void LateRefresh()
        {
            LateRefreshCollection();
        }

        public void Add(T item)
        {
            _toAdd.Push(item);
        }

        public void Remove(T item)
        {
            _toRemove.Push(item);
        }

        public void Clean()
        {
            CleanManager();
        }

        #endregion

        #region Protected Method

/*
        /// <summary>
        /// !!!DANGEROUS METHOD!!!
        /// <list type="bullet"><item>
        /// <description>Can be use on Override Init to fill your collection
        /// with objects of type 'T' that are already present on the scene.</description></item>
        /// </list>
        /// If use elsewhere, you could have the same object multiple time
        /// in your collection or some other unintended bug.
        /// </summary>
        protected void FindAllObjectsOfTypeToCollection()
        {
            var hashSet = new HashSet<T>(UnityEngine.Object.FindObjectsOfType<T>().ToList());

            foreach (var item in hashSet)
            {
                Add(item);
            }

            Debug.LogWarning(
                "Be sure this 'FindAllObjectsOfTypeToCollection()' is called during an initialization phase or in other optimal condition");
        }
*/
        #endregion

        #region Private Methods

        private void InitCollection()
        {
            foreach (var item in _collection)
            {
                item.Init();
            }
        }

        private void PostInitCollection()
        {
            foreach (var item in _collection)
            {
                item.PostInit();
            }
        }

        private void AddStackItemsToCollection()
        {
            while (_toAdd.Count > 0)
            {
                _collection.Add(_toAdd.Pop());
            }
        }

        private void RemoveStackItemsFromCollection()
        {
            while (_toRemove.Count > 0)
            {
                _collection.Remove(_toRemove.Pop());
            }
        }

        private void UpdateCollection()
        {
            foreach (var item in _collection)
            {
                item.Refresh();
            }
        }

        private void FixedUpdateCollection()
        {
            foreach (var item in _collection)
            {
                item.FixedRefresh();
            }
        }
        private void LateRefreshCollection()
        {
            foreach (var item in _collection)
            {
                item.LateRefresh();
            }
        }

        private void CleanManager()
        {
            _collection.Clear();
            _toAdd.Clear();
            _toRemove.Clear();
        }

        #endregion
    }

    #endregion

    #region Manager <T,M> (Singleton Wrapper for Manager)

    /// <summary>
    /// Manager that is a Singleton.
    /// </summary>
    /// <typeparam name="T">Type to Manage</typeparam>
    /// <typeparam name="M">Manager type</typeparam>
    public abstract class Manager<T, M> : WrapperManager, ICollectionManager<T>
        where T : IUpdatable where M : class, IManager, new()
    {
        #region Singleton

        private static M _instance;
        public static M Instance => _instance ??= new M();

        protected Manager()
        {
            collection = new HashSet<T>();
            _toAdd = new Stack<T>();
            _toRemove = new Stack<T>();
        }

        #endregion

        #region Variables & Properties

        protected readonly HashSet<T> collection;
        private readonly Stack<T> _toAdd;
        private readonly Stack<T> _toRemove;
        

        #endregion

        #region Public Methods

        public override void Init()
        {
            AddStackItemsToCollection();
            InitCollection();
        }

        public override void PostInit()
        {
            PostInitCollection();
        }

        public override void Refresh()
        {
            RemoveStackItemsFromCollection();
            UpdateCollection();
            AddStackItemsToCollection();
        }

        public override void FixedRefresh()
        {
            FixedUpdateCollection();
        }

        public override void LateRefresh()
        {
            LateRefreshCollection();
        }

        public void Add(T item)
        {
            _toAdd.Push(item);
        }

        public void Remove(T item)
        {
            _toRemove.Push(item);
        }

        public override void Clean()
        {
            CleanManager();
        }

        #endregion
        
        #region Private Methods

        private void InitCollection()
        {
            foreach (var item in collection)
            {
                item.Init();
            }
        }

        private void PostInitCollection()
        {
            foreach (var item in collection)
            {
                item.PostInit();
            }
        }

        private void AddStackItemsToCollection()
        {
            while (_toAdd.Count > 0)
            {
                collection.Add(_toAdd.Pop());
            }
        }

        private void RemoveStackItemsFromCollection()
        {
            while (_toRemove.Count > 0)
            {
                collection.Remove(_toRemove.Pop());
            }
        }

        private void UpdateCollection()
        {
            foreach (var item in collection)
            {
                item.Refresh();
            }
        }

        private void FixedUpdateCollection()
        {
            foreach (var item in collection)
            {
                item.FixedRefresh();
            }
        }
        private void LateRefreshCollection()
        {
            foreach (var item in collection)
            {
                item.LateRefresh();
            }
        }

        private void CleanManager()
        {
            collection.Clear();
            _toAdd.Clear();
            _toRemove.Clear();
        }

        #endregion
    }

    #endregion

    #region Manager<T,E,A,M> (Singleton Wrapper that includes Manager, Factory and Object Pool)

    /// <summary>
    /// Singleton Manager that also contains a Factory using an Object Pool.
    /// Refer Yourself to the class factory&lt;T,E,A&gt; for more information
    /// concerning the factory.
    /// </summary>
    /// <typeparam name="T">Type to Manage</typeparam>
    /// <typeparam name="E">Enum listing different type of 'T' for the factory</typeparam>
    /// <typeparam name="A">Arguments to provide to the factory</typeparam>
    /// <typeparam name="M">Manager Type</typeparam>
    public abstract class Manager<T, E, A, M> : Manager<T, M>, IFactory<T, E, A>
        where T : IUpdatable, ICreatable<A>, IPoolable
        where E : Enum
        where A : ConstructionArgs
        where M : class, IManager, new()
    {
        protected Manager()
        {
            if (string.IsNullOrEmpty(PrefabLocation))
                Debug.LogError("The PrefabLocation Property of " + typeof(M) + " has not been set");
            else _factory = new Factory<T, E, A>(PrefabLocation);
        }

        #region Variables & Properties

        protected abstract string PrefabLocation { get; }
        private readonly Factory<T, E, A> _factory;

        #endregion

        #region Public Methods

        public override void Init()
        {
            _factory.Init();
           base.Init();
        }

        public T Create(ValueType type, A constructionArgs)
        {
            var toReturn = _factory.Create(type, constructionArgs);
            Add(toReturn);
            return toReturn;
        }

        public void Pool(ValueType type,T toPool)
        {
            ObjectPool.Instance.Pool(type, toPool);
            Remove( toPool);
        }


        #endregion
    }

    #endregion
    
}