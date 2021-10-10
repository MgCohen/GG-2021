using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public ThemeView themeViewPrefab;
    public WorkerView workViewPrefab;
    public DealView dealViewPrefab;
    public StoryView storyViewPrefab;

    public GameObject storySelectionScreen;

    public Theme[] themes;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.BindInterfacesAndSelfTo<SaveManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WriterManager>().AsSingle().NonLazy();
        Container.Bind<StoryManager>().AsSingle().NonLazy();
        Container.Bind<DealManager>().AsSingle().NonLazy();
        Container.Bind<WriterService>().AsSingle();
        Container.Bind<GameUtility>().AsSingle();
        Container.Bind<DealService>().AsSingle();

        Container.DeclareSignal<OnWorkCollectedSignal>();
        Container.DeclareSignal<OnWorkUsedSignal>();
        Container.DeclareSignal<OnWriterAddedSignal>();
        Container.DeclareSignal<OnDealRefreshSignal>();

        Container.Bind<StorySelectionScreen>().FromComponentsOn(storySelectionScreen).AsSingle();

        Container.BindFactory<ThemeLevel, ThemeView, ThemeView.Factory>().FromComponentInNewPrefab(themeViewPrefab);
        Container.BindFactory<WorkerView, WorkerView.Factory>().FromComponentInNewPrefab(workViewPrefab);
        Container.BindFactory<Story, Transform, StoryView, StoryView.Factory>().FromComponentInNewPrefab(storyViewPrefab);
        Container.BindFactory<Deal, Transform, DealView, DealView.Factory>().FromComponentInNewPrefab(dealViewPrefab);

        foreach (var theme in themes)
        {
            Container.QueueForInject(theme);
        }
    }
}