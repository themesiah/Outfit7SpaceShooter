using GamedevsToolbox.ScriptableArchitecture.Sets;
using SpaceShooter.Actors;
using UnityEngine;

namespace SpaceShooter.Extensions
{
    [CreateAssetMenu(menuName = "Space Shoter/Ship Improvement Manager Reference")]
    public class RuntimeSingleShipImprovement : RuntimeSingle<ShipImprovementManager>
    {
    }
}