using TowerDefence;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
   
    public Assets GameAssets;
    
    public override void InstallBindings()
    {
        Container.BindInstance(GameAssets).AsSingle();
        
        Container.Bind<IEnemyMover>().To<EnemyMover>().AsTransient();
        Container.Bind<IEnemySpawner>().To<EnemySpawner>().AsSingle();
        Container.Bind<IUnitsHolder>().WithId("EnemyHolder").To<UnitsHolder>().AsCached();
        Container.Bind<IUnitsHolder>().WithId("TowerHolder").To<UnitsHolder>().AsCached();
        
        Container.BindFactory<Object, EnemyAI, EnemyAI.Factory>().FromFactory<EnemyFactory>();
        Container.BindFactory<Object, TowerAI, TowerAI.Factory>().FromFactory<TowerFactory>();
    }
}