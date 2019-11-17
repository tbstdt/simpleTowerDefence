using TowerDefence;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public interface IEnemyMover
{
    void Init(NavMeshAgent agent, Transform myTransform);
    void GoToClosestTarget();
    TowerAI GetClosestTower();
    void Stop(bool isStop);
}

public class EnemyMover : IEnemyMover
{
    [Inject (Id = "TowerHolder")] private IUnitsHolder towerHolder;

    private NavMeshAgent agent;
    private Transform myTransform;

    private GameObject closestTower;

    public void Init(NavMeshAgent agent,  Transform myTransform)
    {
        this.agent = agent;
        this.myTransform = myTransform;
    }

    public TowerAI GetClosestTower()
    {
        return closestTower == null ? null : closestTower.GetComponent<TowerAI>();
    }
    

    public void GoToClosestTarget()
    {
        closestTower = towerHolder.GetClosestUnit(myTransform.position);
        if (closestTower == null || agent == null || myTransform == null || agent.navMeshOwner == null) return;
        agent.SetDestination(closestTower.transform.position);
    }

    public void Stop(bool isStop) => agent.isStopped = isStop; 
   
}
