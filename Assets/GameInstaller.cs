using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public ThemeView themeViewPrefab;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SaveManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WriterManager>().AsSingle().NonLazy();
        Container.Bind<GameUtility>().AsSingle();

        Container.DeclareSignal<OnWorkCollectedSignal>();
        Container.DeclareSignal<OnWriterAddedSignal>();

        Container.BindFactory<ThemeLevel, ThemeView, ThemeView.Factory>().FromComponentInNewPrefab(themeViewPrefab);
    }
}