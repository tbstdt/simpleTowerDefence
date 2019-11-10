using TowerDefence;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public interface IEnemyMover
{
    void Init(NavMeshAgent agent, Transform enemyTransform);
    void GoToClosestTarget();
}

public class EnemyMover : IEnemyMover
{
    [Inject]
    private IPlayerUnitsHolder playerUnitsHolder;

    private NavMeshAgent agent;
    private Transform enemyTransform;

    public void Init(NavMeshAgent agent,  Transform enemyTransform)
    {
        this.agent = agent;
        this.enemyTransform = enemyTransform;
    }

    public void GoToClosestTarget()
    {
        var target = playerUnitsHolder.GetClosestTarget(enemyTransform.position);
        if (target != enemyTransform.position) agent.SetDestination(target);
    }
}
