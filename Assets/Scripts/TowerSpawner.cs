using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TowerDefence;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class TowerSpawner : MonoBehaviour
{

    [Inject] private Assets gameAssets;
    [Inject] private IPlayerUnitsHolder towerHolder;
    

    private GameObject place;

    public bool isTowerMustPlaced;
    // Update is called once per frame
    void Update()
    {
        if (isTowerMustPlaced)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit) && hit.transform.CompareTag("TowerField"))
            {
                var placePosition = new Vector3(Mathf.Ceil(hit.point.x - 0.5f) ,0f, Mathf.Ceil(hit.point.z));
               
                if (place == null) place = Instantiate(gameAssets.TargetPlacePrefab, placePosition, Quaternion.identity);
                else
                {
                    place.SetActive(true);
                    place.transform.position = placePosition;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    var tower = Instantiate(gameAssets.PlayerUnits[0].prefab, placePosition, Quaternion.identity);
                    towerHolder.AddNewTarget(tower.transform);
                   // isTowerMustPlaced = false;
                }
            }
            else
            {
                if (place != null)
                place.SetActive(false);
            }
        }
        
        
    }
}
