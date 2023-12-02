using Portfolio.AssetCollections;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AssetCollectionInstaller", menuName = "Installers/AssetCollectionInstaller")]
public class AssetCollectionInstaller : ScriptableObjectInstaller<AssetCollectionInstaller> {
    
    public SceneIndexLibrary SceneIndexLibrary;

    public override void InstallBindings() {
        Container.BindInstance(SceneIndexLibrary);
    }
}