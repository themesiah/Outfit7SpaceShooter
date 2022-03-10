using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Pools;

namespace SpaceShooter.Extensions 
{
    [System.Serializable]
    public class AddressableScriptablePool : ScriptablePool
    {
        protected override void CreateInstanceOnIndex(int index)
        {
            var op = data.PrefabReference.InstantiateAsync(poolParent);
            op.Completed += (handler) =>
            {
                GameObject instance = handler.Result;
                instances[index] = instance;
                freeInstances.Enqueue(instance);
                instances[index].GetComponent<PoolObjectDestroyer>()?.InitWithPool(this);
            };
        }
    }
}