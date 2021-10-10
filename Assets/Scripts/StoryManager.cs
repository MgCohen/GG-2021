using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class StoryManager : Persistable<StoryManager>
{
    [Inject]
    private SignalBus signals;

    public List<Story> stories = new List<Story>();

    public override void BeforeSave()
    {

    }

    public override void OnLoad()
    {

    }

    public bool CanCompleteDeal(Deal deal)
    {
        foreach(ThemeLevel tl in deal.requisities)
        {
            foreach(var story in stories)
            {
                if(!story.themes.Exists(t => t.theme == tl.theme && t.level >= tl.level))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void SpendStory(Story story)
    {
        stories.Remove(story);
        signals.Fire(new OnWorkUsedSignal(story));
    }
}
