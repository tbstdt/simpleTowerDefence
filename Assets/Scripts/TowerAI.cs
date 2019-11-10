using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace TowerDefence
{
    public class TowerAI : MonoBehaviour, IDamageable
    {
        private Attacker attacker;
        private AttackingUnit towerData;
      

        public Transform Transform { get; private set; }
        [SerializeField]
        private int hp;
        

        [Inject (Id = "TowerHolder")] private IUnitsHolder towerHolder;
        [Inject (Id = "EnemyHolder")] private IUnitsHolder enemyHolder;
        
        public class Factory : PlaceholderFactory<Object, TowerAI>
        {
        }

        public void Init(AttackingUnit data)
        {
            towerData = data;
            hp = data.Hp;
            Transform = transform;
            attacker = new Attacker(transform);
            InvokeRepeating(nameof(TrayAttack), 1, towerData.AttackRate);
        }
        
        private void TrayAttack()
        {
            var success = attacker.Attack(towerData.Damage, towerData.MinAttackDistance);
            if (success) return;
            FindTarget();
        }

        public void AddDamage(int damage)
        {
            hp -= damage;
        }
        
        private void Update()
        {
            CheckDeath();
        }
        
        private void FindTarget()
        {
            var target = enemyHolder.GetClosestUnit(Transform.position);
            if (target == null) return;
            attacker.SetTarget(target.GetComponent<EnemyAI>());
            
        }

        public void CheckDeath()
        {
            if (hp <= 0)
            {
                towerHolder.RemoveUnit(gameObject);
                Destroy(gameObject);
            }
        }
       
    }
    
   
}