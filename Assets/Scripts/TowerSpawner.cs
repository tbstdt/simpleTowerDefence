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
    [Inject (Id = "TowerHolder")] private IUnitsHolder towerHolder;
    [Inject] private TowerAI.Factory towerFactory;
    

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
                //placePosition += new Vector3(-0.5f,0,0);
                
                if (place == null) place = Instantiate(gameAssets.TargetPlacePrefab, placePosition, Quaternion.identity);
                else
                {
                    place.SetActive(true);
                    place.transform.position = placePosition;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    var towerdata = gameAssets.PlayerUnits[0];
                    var tower = towerFactory.Create(towerdata.Prefab[0]);
                    tower.transform.position = placePosition;
                    tower.Init(towerdata);
                    towerHolder.AddNewUnit(tower.gameObject);
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
