using UnityEngine;
using Zenject;

namespace TowerDefence
{
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