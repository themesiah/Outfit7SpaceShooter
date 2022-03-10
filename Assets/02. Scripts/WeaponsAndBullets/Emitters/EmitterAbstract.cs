using UnityEngine;

namespace SpaceShooter.WeaponsAndBullets
{
    // This abstract class allows us to serialize an emitter on the inspector. Otherwise, an interface can't be serialized
    public abstract class EmitterAbstract : MonoBehaviour, IEmitter
    {
        public abstract void Emit();
    }
}