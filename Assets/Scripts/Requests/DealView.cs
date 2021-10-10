using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using TMPro;

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
    [Inject]
    private ThemeView.Factory factory;

    public Button selectButton;
    private Deal deal;

    private bool isDone = false;

    public TextMeshProUGUI xpAmount;
    public TextMeshProUGUI goldAmount;

    public Transform themeListContainer;

    public GameObject doneText;
    public GameObject completableText;

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
        xpAmount.text = deal.xpReward.ToString();
        goldAmount.text = deal.goldReward.ToString();
        foreach (var tl in deal.requisities)
        {
            factory.Create(tl, themeListContainer);
        }
    }

    public void TryCompleteDeal()
    {
        selectionScreen.Set(1, Complete, s => deal.CanBeCompletedBy(s));
    }

    public void Complete(List<Story> spentStories)
    {
        isDone = true;
        foreach (var story in spentStories)
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
        completableText.SetActive(true);
        selectButton.interactable = true;
    }

    private void SetAsBlocked()
    {
        completableText.SetActive(false);
        selectButton.interactable = false;
    }

    private void SetAsDone()
    {
        //hide values
        xpAmount.gameObject.SetActive(false);
        goldAmount.gameObject.SetActive(false);
        //hide themes
        doneText.SetActive(true);
    }


    public class Factory : PlaceholderFactory<Deal, Transform, DealView>
    {

    }
}
