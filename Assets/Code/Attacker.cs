using UnityEngine;

namespace TowerDefence
{
    public class Attacker
    {
        private IDamageable target;
        private Transform myTransform;
     

        public Attacker(Transform my)
        {
            myTransform = my;
        }
        
        public void SetTarget(IDamageable target)
        {
            this.target = target;
        }

        public bool IsCanAttack(float minDistance)
        {
            if (target == null || target.Transform == null) return false;
            if (target.CheckDeath()) return false;
            
            var distance = Vector3.Distance(target.Transform.position, myTransform.position);
            if (distance > minDistance) return false;
           
            return true;
        }

        public void Attack(int damage)
        {
            target.AddDamage(damage);
        }
    }
}