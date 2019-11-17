using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu(fileName = "new AttackingUnit", menuName = "AttackingUnit")]
    public class AttackingUnit : ScriptableObject
    {
        public int Hp;
        public int Damage;
        public float AttackRate;
        public float MinAttackDistance;
        public bool IsTower;
        public GameObject[] Prefab;
        public bool isRangeAttack;
        public GameObject[] Projectile;
    }
}