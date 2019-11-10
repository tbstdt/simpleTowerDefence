using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Object = UnityEngine.Object;

namespace TowerDefence
{
    [RequireComponent(typeof(NavMeshAgent))]
   
    public class EnemyAI : MonoBehaviour, IDamageable
    {
        [Inject] private IEnemyMover mover;
        [Inject(Id = "EnemyHolder")] private IUnitsHolder enemyHolder;
        
        private Attacker attacker;
        private AttackingUnit enemyData;
       

        public Transform Transform { get; private set; }
        [SerializeField]
        private int hp;

        public class Factory : PlaceholderFactory<Object, EnemyAI>
        {
        }

        public void Init(AttackingUnit data)
        {
            enemyData = data;
            hp = data.Hp;
            Transform = transform;
            var agent = GetComponent<NavMeshAgent>();
            agent.Warp(transform.position);
            mover.Init(agent, transform);
            attacker = new Attacker(transform);
            
            InvokeRepeating(nameof(TrayAttack), 1, enemyData.AttackRate);
        }

        private void Update()
        {
            CheckDeath();
        }
        
        public void AddDamage(int damage)
        {
            hp -= damage;
        }
        
        public void CheckDeath()
        {
            if (hp <= 0)
            {
                enemyHolder.RemoveUnit(gameObject);
                Destroy(gameObject);
            }
        }
     
        private void TrayAttack()
        {
           var success = attacker.Attack(enemyData.Damage, enemyData.MinAttackDistance);
           mover.Stop(success);
          
           if (success) return;
           // get new target
           mover.GoToClosestTarget();
           attacker.SetTarget(mover.GetClosestTower());
        }
    }
    
   
}