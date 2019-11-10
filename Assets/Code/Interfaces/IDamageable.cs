using UnityEngine;

namespace TowerDefence
{
    public interface IDamageable
    {
        void AddDamage(int damage);
        void CheckDeath();

        Transform Transform {get;}
    }
}