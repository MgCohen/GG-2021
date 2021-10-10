using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DealView : MonoBehaviour
{
    [Inject]
    private SignalBus signals;

    [Inject]
    private StoryManager stories;

    private Deal deal;

    private bool isDone = false;

    [Inject]
    private void Init(Deal deal, Transform parent)
    {
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        this.deal = deal;
        signals.Subscribe<OnWorkCollectedSignal>(s => UpdateView());
        signals.Subscribe<OnWorkUsedSignal>(s => UpdateView());
    }

    public void SetDeal()
    {

    }

    public void TryCompleteDeal()
    {
        //popup 
    }

    public void Complete()
    {
        isDone = true;
    }

    private void UpdateView()
    {
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


    public class Factory : PlaceholderFactory<Deal, Transform, DealView>
    {

    }
}
