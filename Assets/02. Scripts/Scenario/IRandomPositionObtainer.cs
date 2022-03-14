using UnityEngine;

namespace SpaceShooter.Scenario
{
    public interface IRandomPositionObtainer
    {
        Vector3 GetRandomPosition();
    }
}