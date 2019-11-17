using System.Collections;
using UnityEngine;

namespace TowerDefence
{
    public interface IDamageable
    {
        void AddDamage(int damage);
        bool CheckDeath();

        Transform Transform {get;}
    }
}