using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public ThemeView themeViewPrefab;
    public WorkerView workViewPrefab;

    public Theme[] themes;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.BindInterfacesAndSelfTo<SaveManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WriterManager>().AsSingle().NonLazy();
        Container.Bind<WriterService>().AsSingle();
        Container.Bind<GameUtility>().AsSingle();


        Container.DeclareSignal<OnWorkCollectedSignal>();
        Container.DeclareSignal<OnWriterAddedSignal>();

        Container.BindFactory<ThemeLevel, ThemeView, ThemeView.Factory>().FromComponentInNewPrefab(themeViewPrefab);
        Container.BindFactory<WorkerView, WorkerView.Factory>().FromComponentInNewPrefab(workViewPrefab);

        foreach(var theme in themes)
        {
            Container.QueueForInject(theme);
        }
    }
}