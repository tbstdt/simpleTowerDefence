
using TowerDefence;
using UnityEngine;
using Zenject;

public interface IEnemySpawner
{
    void SpawnEnemy(AttackingUnit enemy, Vector3 position);
}

public class EnemySpawner : IEnemySpawner
{
    [Inject] private EnemyAI.Factory enemyAIFactory;
    [Inject(Id = "EnemyHolder")] private IUnitsHolder enemyHolder;
     

    public void SpawnEnemy(AttackingUnit enemy, Vector3 position)
    {
        var ai = enemyAIFactory.Create(enemy.prefab);
        ai.transform.position = position;
        ai.Init(enemy);
        enemyHolder.AddNewUnit(ai.gameObject);
    }
}
