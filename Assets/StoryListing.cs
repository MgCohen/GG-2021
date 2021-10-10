using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class StoryListing : MonoBehaviour
{
    [Inject]
    public StoryView.Factory factory;
    [Inject]
    public StoryManager stories;
    [Inject]
    public SignalBus signals;

    public Transform viewContainer;

    public List<StoryView> views = new List<StoryView>();
    
    [Inject]
    public void Init()
    {
        foreach (var story in stories.stories)
        {
            var view = factory.Create(story, viewContainer);
            views.Add(view);
        }

        signals.Subscribe<OnWorkCollectedSignal>(UpdateStories);
        signals.Subscribe<OnWorkUsedSignal>(UpdateStories);
    }

    public void UpdateStories()
    {
        foreach (var view in views)
        {
            Destroy(view.gameObject);
        }

        views.Clear();
        Debug.Log(stories.stories.Count);
        foreach (var story in stories.stories)
        {
            var view = factory.Create(story, viewContainer);
            views.Add(view);
        }
    }


}
