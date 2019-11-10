using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Object = UnityEngine.Object;

namespace TowerDefence
{
    [RequireComponent(typeof(NavMeshAgent))]
   
    public class EnemyAI : MonoBehaviour
    {
        [Inject]
        private IEnemyMover mover;

        public class Factory : PlaceholderFactory<Object, EnemyAI>
        {
        }

        private void Start()
        {
            print("EnemyAI start");
            var agent = GetComponent<NavMeshAgent>();
            mover.Init(agent, transform);
        }

        private void Update()
        {
            mover.GoToClosestTarget();
        }
    }
    
    public class EnemyFactory : IFactory<Object, EnemyAI>
    {
        readonly DiContainer _container;

        public EnemyFactory(DiContainer container)
        {
            _container = container;
        }

        public EnemyAI Create(Object prefab)
        {
            return _container.InstantiatePrefabForComponent<EnemyAI>(prefab);
        }
    }
}