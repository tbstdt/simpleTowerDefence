using System.Collections.Generic;
using System.Linq;
using TowerDefence;
using UnityEngine;
using Zenject;




public class UnitsHolder : IUnitsHolder
{
    private List<GameObject> targets = new List<GameObject>();

    public GameObject GetClosestUnit(Vector3 myPos)
    {
        if (targets == null || !targets.Any()) return null;
        var closestTarget = targets.First();
        foreach (var target in targets)
        {
            var targetPos = target.transform.position;
            if (Vector3.Distance(closestTarget.transform.position, myPos) > Vector3.Distance(targetPos, myPos))
                closestTarget = target;
        }
        return closestTarget;
    }

    public void AddNewUnit(GameObject unit)
    {
       targets.Add(unit);
    }

    public void RemoveUnit(GameObject unit)
    {
        targets.Remove(unit);
    }
}
