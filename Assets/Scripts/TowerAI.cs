using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Object = UnityEngine.Object;

namespace TowerDefence
{
    public class TowerAI : MonoBehaviour, IDamageable
    {
        private Attacker attacker;
        private EnemyAI target;

        public Transform Transform { get; private set; }
        [SerializeField] private int level;
        [SerializeField] private int hp;
        [SerializeField] private bool isMainTower;
        [SerializeField] private AttackingUnit towerData;
       
        public Transform CenterPos;
        
        [Inject (Id = "TowerHolder")] private IUnitsHolder towerHolder;
        [Inject (Id = "EnemyHolder")] private IUnitsHolder enemyHolder;
        
        public class Factory : PlaceholderFactory<Object, TowerAI>
        {
        }

        private void Start()
        {
            if (isMainTower)
            {
                Init(towerData);
                towerHolder.AddNewUnit(gameObject);
            }
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
            var success = attacker.IsCanAttack(towerData.MinAttackDistance);
            if (success)
            {
                if (towerData.Projectile.Length > 0 && towerData.Projectile?[level] != null)
                {
                    var projectile = Instantiate(towerData.Projectile[level], CenterPos.position, Quaternion.identity, transform);
                    projectile.transform.DOMove(target.CenterPos.position,0.5f)
                        .SetEase(Ease.Linear).OnComplete(() =>
                    {
                        attacker.Attack(towerData.Damage);
                        Destroy(projectile);
                    });
                }
                return;
            }
           
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
            var targetObject = enemyHolder.GetClosestUnit(Transform.position);
            if (targetObject == null) return;
            target = targetObject.GetComponent<EnemyAI>();
            attacker.SetTarget(target);
        }

        public bool CheckDeath()
        {
            if (hp <= 0)
            {
                towerHolder.RemoveUnit(gameObject);
                StartCoroutine(Death());
                return true;
            }
            return false;
        }

        private IEnumerator Death()
        {
            //TODO: Animation
            yield return null;
            Destroy(gameObject);
        }
    }
    
   
}