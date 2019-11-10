using UnityEngine;
using Zenject;

namespace TowerDefence
{
    public class TowerFactory : IFactory<Object, TowerAI>
    {
        readonly DiContainer _container;

        public TowerFactory(DiContainer container)
        {
            _container = container;
        }

        public TowerAI Create(Object prefab)
        {
            return _container.InstantiatePrefabForComponent<TowerAI>(prefab);
        }
    }
}