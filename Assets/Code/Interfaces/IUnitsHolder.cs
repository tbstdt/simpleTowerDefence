using UnityEngine;

namespace TowerDefence
{
    public interface IUnitsHolder
    {
        GameObject GetClosestUnit(Vector3 myPos);
        void AddNewUnit(GameObject unit);
        void RemoveUnit(GameObject unit);
    }
}