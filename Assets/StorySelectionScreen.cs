using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using System;

public class StorySelectionScreen : MonoBehaviour
{
    [Inject]
    public StoryManager stories;
    [Inject]
    public StoryView.Factory factory;

    public Transform container;
    public List<StoryView> storyViewer;

    public System.Action<List<Story>> selectionCallback;

    public void Set(int storyLimit, System.Action<List<Story>> callback, Func<Story, bool> predicate = null)
    {
        gameObject.SetActive(true);

        selectionCallback = callback;

        foreach (StoryView view in storyViewer)
        {
            Destroy(view.gameObject);
        }

        storyViewer.Clear();

        foreach (Story story in stories.stories)
        {
            if (predicate == null || predicate(story))
            {
                var view = factory.Create(story, container);
                storyViewer.Add(view);
                view.SetSelectable(true);
            }
        }
    }

    public void EndSelection()
    {
        var stories = storyViewer.Where(sv => sv.isSelected).Select(s => s.story).ToList();
        selectionCallback.Invoke(stories);
        selectionCallback = null;
        gameObject.SetActive(false);
    }
}
