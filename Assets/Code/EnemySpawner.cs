
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
    
    public void SpawnEnemy(AttackingUnit enemy, Vector3 position)
    {
        Debug.Log("EnemySpawner SpawnEnemy");
        var ai = enemyAIFactory.Create(enemy.prefab);
        ai.transform.position = position;
    }
}
