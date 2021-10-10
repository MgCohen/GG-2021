using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StorySelectionScreen : MonoBehaviour
{
    [Inject]
    public StoryManager stories;
    [Inject]
    public StoryView.Factory factory;

    public Transform container;
    public List<StoryView> storyViewer;

    public void Set()
    {
        foreach(Story story in stories.stories)
        {
            var view = factory.Create(story, container);
            storyViewer.Add(view);
            view.SetSelectable();
        }
    }
}
