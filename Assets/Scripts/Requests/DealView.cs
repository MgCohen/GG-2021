using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class DealView : MonoBehaviour
{
    [Inject]
    private SignalBus signals;
    [Inject]
    StorySelectionScreen selectionScreen;
    [Inject]
    private StoryManager stories;
    [Inject]
    private Company company;

    public Button selectButton;
    private Deal deal;

    private bool isDone = false;

    [Inject]
    private void Init(Deal deal, Transform parent)
    {
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        this.deal = deal;
        SetDeal();
        signals.Subscribe<OnWorkCollectedSignal>(UpdateView);
        signals.Subscribe<OnWorkUsedSignal>(UpdateView);

        selectButton.onClick.AddListener(TryCompleteDeal);
    }

    public void SetDeal()
    {

    }

    public void TryCompleteDeal()
    {
        selectionScreen.Set(1, Complete, s => deal.CanBeCompletedBy(s));
    }

    public void Complete(List<Story> spentStories)
    {
        isDone = true;
        foreach(var story in spentStories)
        {
            stories.SpendStory(story);
        }
        company.AddReward(deal.goldReward, deal.xpReward);
    }

    private void UpdateView()
    {
        if (isDone) return;

        if (stories.CanCompleteDeal(deal))
        {
            SetAsCompletable();
            return;
        }
        SetAsBlocked();
    }

    private void SetAsCompletable()
    {

    }

    private void SetAsBlocked()
    {

    }

    private void SetAsDone()
    {

    }


    public class Factory : PlaceholderFactory<Deal, Transform, DealView>
    {

    }
}
