using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Sets;
using SpaceShooter.WeaponsAndBullets;

namespace SpaceShooter.Extensions
{
    [CreateAssetMenu(menuName = "Space Shoter/Pools/Bullet Pool Container Reference")]
    public class RuntimeSingleBulletPoolContainer : RuntimeSingle<BulletPoolContainer>
    {
    }
}