using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamedevsToolbox.ScriptableArchitecture.Pools
{
    [System.Serializable]
    public class ScriptablePool
    {
        private const string POOL_PARENT_NAME = "----[POOL {0} OBJECTS]----";

        [SerializeField]
        protected ScriptablePoolData data = default;

        protected GameObject[] instances;
        private List<GameObject> usedInstances;
        protected Queue<GameObject> freeInstances;
        protected Transform poolParent = null;

        private bool isInitialized = false;

        #region Public API
        public void InitPool()
        {
            if (isInitialized == true)
                return;
            isInitialized = true;
            GetPoolParent();
            instances = new GameObject[data.NumberOfInstances];
            usedInstances = new List<GameObject>();
            freeInstances = new Queue<GameObject>();

            for (int i = 0; i < data.NumberOfInstances; ++i)
            {
                CreateInstanceOnIndex(i);
            }
        }

        public GameObject GetInstance(Transform instanceParent = null, Vector3 instancePosition = default, Quaternion instanceRotation = default)
        {
            if (!CheckPoolInitialized())
                return null;

            GameObject instance = null;
            if (freeInstances.Count > 0)
            {
                instance = GetFreeInstance();
            }
            else
            {
                switch (data.Behaviour)
                {
                    case ScriptablePoolData.InstancesFullBehaviour.ReturnNull:
                        break;
                    case ScriptablePoolData.InstancesFullBehaviour.ReturnFirstUsed:
                        instance = GetUsedInstance();
                        break;
                    case ScriptablePoolData.InstancesFullBehaviour.DynamicThenFirstUsed:
                        if (instances.Length >= data.DynamicMaxInstances)
                        {
                            instance = GetUsedInstance();
                        }
                        else
                        {
                            instance = ResizeAndAdd();
                        }
                        break;
                    case ScriptablePoolData.InstancesFullBehaviour.DynamicThenNull:
                        if (instances.Length >= data.DynamicMaxInstances)
                        {
                            instance = null;
                        }
                        else
                        {
                            instance = ResizeAndAdd();
                        }
                        break;
                    case ScriptablePoolData.InstancesFullBehaviour.DynamicInfinite:
                        instance = ResizeAndAdd();
                        break;
                }
            }

            if (instance != null)
            {
                instance.transform.position = instancePosition;
                instance.transform.rotation = instanceRotation;
                instance.transform.SetParent(instanceParent);
            }
            return instance;
        }

        public void FreeInstance(GameObject instance)
        {
            if (!CheckPoolInitialized())
                return;

            if (usedInstances.Contains(instance))
            {
                usedInstances.Remove(instance);
                freeInstances.Enqueue(instance);
                instance.transform.SetParent(poolParent);
            }
        }

        public void SoftFreeAll()
        {
            if (!CheckPoolInitialized())
                return;

            for (int i = usedInstances.Count-1; i >= 0; --i)
            {
                var instance = usedInstances[i];
                usedInstances.Remove(instance);
                freeInstances.Enqueue(instance);
                instance.transform.SetParent(poolParent);
            }
        }

        public void FreeAll()
        {
            if (!CheckPoolInitialized())
                return;

            isInitialized = false;
            usedInstances.Clear();
            freeInstances.Clear();
            instances = null;
            poolParent = null;
        }

        public int UsedCount => usedInstances != null ? usedInstances.Count : 0;
        public int FreeCount => freeInstances != null ? freeInstances.Count : 0;
        #endregion

        #region Private API
        private void GetPoolParent()
        {
            if (poolParent == null)
            {
                string poolName = string.Format(POOL_PARENT_NAME, data.name);
                GameObject p = GameObject.Find(poolName);
                if (p == null)
                {
                    p = new GameObject(poolName);
                    p.SetActive(false);
                }
                poolParent = p.transform;
            }
        }

        protected virtual void CreateInstanceOnIndex(int index)
        {
            instances[index] = GameObject.Instantiate(data.Prefab, poolParent);
            freeInstances.Enqueue(instances[index]);
            instances[index].GetComponent<PoolObjectDestroyer>()?.InitWithPool(this);
        }

        private GameObject GetFreeInstance()
        {
            GameObject instance = freeInstances.Dequeue();
            usedInstances.Add(instance);
            return instance;
        }

        private GameObject GetUsedInstance()
        {
            GameObject instance = usedInstances[0];
            usedInstances.Remove(instance);
            usedInstances.Add(instance);
            return instance;
        }

        private GameObject ResizeAndAdd()
        {
            int instancesToCreate = System.Math.Max(data.DynamicInstancePace, 1);
            System.Array.Resize(ref instances, instances.Length + instancesToCreate);
            for (int i = instances.Length - instancesToCreate; i < instances.Length; ++i)
            {
                CreateInstanceOnIndex(i);
            }

            return GetFreeInstance();
        }

        private bool CheckPoolInitialized()
        {
            if (!isInitialized)
            {
                Utils.Logger.Logger.LogWarning("You didn't initialized the pool with name " + data.name + ". It will only return null.");
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}