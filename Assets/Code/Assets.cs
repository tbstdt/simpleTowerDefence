using System;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class Assets
    {
        public GameObject TargetPlacePrefab;
        
        public AttackingUnit[] EnemyUnits;
        public AttackingUnit[] PlayerUnits;
    }
}