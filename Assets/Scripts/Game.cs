using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace TowerDefence
{
    public class Game : MonoBehaviour
    {
        [Inject] private Assets gameAssets;
        [Inject] private IEnemySpawner spawner;
     
        public Transform EnemySpawnPosition;

        private void Start()
        {
            StartCoroutine(Spawn());
        }


        private IEnumerator Spawn()
        {
            spawner.SpawnEnemy(gameAssets.EnemyUnits[0], EnemySpawnPosition.position);
            yield return new WaitForSeconds(3);
            spawner.SpawnEnemy(gameAssets.EnemyUnits[1], EnemySpawnPosition.position);
        }
    }
}