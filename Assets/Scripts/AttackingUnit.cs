using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu(fileName = "new AttackingUnit", menuName = "AttackingUnit")]
    public class AttackingUnit : ScriptableObject
    {
        public int Hp;
        public int Damage;
        public float AttackRate;

        public GameObject prefab;
    }
}