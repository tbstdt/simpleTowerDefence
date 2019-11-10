using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


public interface IPlayerUnitsHolder
{
    Vector3 GetClosestTarget(Vector3 enemyPos);
    void AddNewTarget(Transform target);
    void RemoveTarget(Transform target);
}

public class PlayerUnitsHolder : IPlayerUnitsHolder
{
    private List<Transform> targets = new List<Transform>();

    public Vector3 GetClosestTarget(Vector3 enemyPos)
    {
        if (targets == null || !targets.Any()) return enemyPos;
        var closestTarget = targets.First().transform.position;
        foreach (var target in targets)
        {
            var targetPos = target.transform.position;
            if (Vector3.Distance(closestTarget, enemyPos) > Vector3.Distance(targetPos, enemyPos))
                closestTarget = targetPos;
        }
        return closestTarget;
    }

    public void AddNewTarget(Transform target)
    {
       targets.Add(target);
    }

    public void RemoveTarget(Transform target)
    {
        targets.Remove(target);
    }
}
