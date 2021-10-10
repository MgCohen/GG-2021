using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.UI;
using System;

public class WorkerView : MonoBehaviour
{
    [Inject]
    private SignalBus signals;

    [HideInInspector]
    public Writer writer;

    public TextMeshProUGUI nameText;

    public Transform themesContainer;

    [Inject]
    public ThemeView.Factory factory;
    [Inject]
    public StoryManager stories;

    private WorkStatus workStatus;

    public Button collectButton;
    public TextMeshProUGUI workText;
    public Button workButton;

    [Inject]
    public StoryMakingScreen storyMakingScreen;

    public void Init(Writer newWriter)
    {
        writer = newWriter;
        SetBasicVisuals();
        collectButton.onClick.AddListener(TryCollect);
        UpdateVisuals();
        workButton.onClick.AddListener(PickStory);
    }

    public void PickStory()
    {
        storyMakingScreen.Set(writer.workQuality, writer, s => writer.StartWorking(new Story(s)));
    }

    private void SetBasicVisuals()
    {
        nameText.text = writer.writerName;
        foreach(ThemeLevel theme in writer.workQuality)
        {
            ThemeView view =  factory.Create(theme, themesContainer);
        }
        workStatus = writer.workStatus;
    }
    private void Update()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if(workStatus != writer.workStatus)
        {
            ResetVisuals();
            workStatus = writer.workStatus;
        }
        switch (writer.workStatus)
        {
            case WorkStatus.Idle:
                SetIdleVisuals();
                break;
            case WorkStatus.Working:
                SetWorkingVisuals();
                break;
            case WorkStatus.finished:
                SetFinishedVisuals();
                break;
            default:
                break;
        }
    }

    public void TryCollect()
    {
        if(writer.workStatus == WorkStatus.finished)
        {
            var story = writer.CollectWork();
            stories.CollectStory(story);
        }
    }

    private void SetIdleVisuals()
    {
        workText.gameObject.SetActive(true);
        workButton.interactable = true;
        workText.text = "Waiting for work...";
    }

    private void SetWorkingVisuals()
    {
        workText.gameObject.SetActive(true);
        var timeLeft = new TimeSpan(writer.completionTime - DateTime.Now.Ticks);
        workText.text = "Working: " + timeLeft.Hours + "h " + timeLeft.Minutes + "m " + timeLeft.Seconds + "s"; 
    }

    private void SetFinishedVisuals()
    {
        collectButton.gameObject.SetActive(true);
    }

    private void ResetVisuals()
    {
        collectButton.gameObject.SetActive(false);
        workText.gameObject.SetActive(false);
        workButton.interactable = false;
    }

    public class Factory: PlaceholderFactory<WorkerView>
    {

    }
}
