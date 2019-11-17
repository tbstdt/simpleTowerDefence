using System.Collections;
using DG.Tweening;
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
     
        [SerializeField] private int hp;
        [SerializeField] private Animator animator;
       
        public Transform CenterPos;
        
        private Attacker attacker;
        private AttackingUnit enemyData;
        private EnemyState state = EnemyState.Idle;
        private NavMeshAgent agent;
        private TowerAI target;

        public Transform Transform { get; private set; }
       

        public class Factory : PlaceholderFactory<Object, EnemyAI> {}

        public void Init(AttackingUnit data)
        {
            enemyData = data;
            hp = data.Hp;
            Transform = transform;
            agent = GetComponent<NavMeshAgent>();
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
        
        public bool CheckDeath()
        {
            if (hp <= 0)
            {
                StartCoroutine(Death());
                return true;
            }
            return false;
        }

        private IEnumerator Death()
        {
            UpdateAnimation(EnemyState.Death);
            enemyHolder.RemoveUnit(gameObject);
            mover.Stop(true);
            yield return new WaitForSeconds(1f);
            
            while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;
                
            animator.transform.DOMoveY(-1, 10).SetEase(Ease.Linear) .OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
     
        private void TrayAttack()
        {
            if (state == EnemyState.Death) return;
            UpdateAnimation(EnemyState.Attack);
            
            var success = attacker.IsCanAttack(enemyData.MinAttackDistance);
            if (success)
            {
                transform.LookAt(target.transform);
                
                if (enemyData.isRangeAttack && enemyData.Projectile.Length > 0 && enemyData.Projectile?[0] != null)
                    RangeAttack();
                else
                    attacker.Attack(enemyData.Damage);
            }
            
            mover.Stop(success);

            if (success) return;
            // get new target
            UpdateAnimation(EnemyState.Move);
            mover.GoToClosestTarget();
            target = mover.GetClosestTower();
            attacker.SetTarget(target);
        }

        private void RangeAttack()
        {
            var projectile = Instantiate(enemyData.Projectile[0], CenterPos.position, Quaternion.identity, transform);
            
            projectile.transform.DOMove(target.CenterPos.position, enemyData.AttackRate/2)
                .SetEase(Ease.Linear)
                .OnComplete(()=>
                {
                    attacker.Attack(enemyData.Damage);
                    Destroy(projectile);
                });
        }

        private void UpdateAnimation(EnemyState state)
        {
            this.state = state;
            animator.SetInteger("Animation", (int)state);
        }
    }
    
   
}