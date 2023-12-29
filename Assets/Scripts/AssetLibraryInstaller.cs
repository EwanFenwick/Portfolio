using Portfolio.AssetLibraries;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AssetLibraryInstaller", menuName = "Installers/AssetLibraryInstaller")]
public class AssetLibraryInstaller : ScriptableObjectInstaller<AssetLibraryInstaller>  {
    
    public SceneIndexLibrary SceneIndexLibrary;

    public override void InstallBindings() {
        Container.BindInstance(SceneIndexLibrary).AsSingle();
    }
}