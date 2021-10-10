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

    private WorkStatus workStatus;

    public Button collectButton;
    public TextMeshProUGUI workText;
    public Button workButton;

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
        Debug.Log(1);
    }

    private void SetBasicVisuals()
    {
        nameText.text = writer.writerName;
        foreach(ThemeLevel theme in writer.workQuality)
        {
            ThemeView view =  factory.Create(theme);
            view.transform.SetParent(themesContainer);
            view.transform.localScale = Vector3.one;
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
            signals.Fire(new OnWorkCollectedSignal(story));
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
        workText.text = new TimeSpan((DateTime.Now.Ticks - writer.completionTime)).ToString();
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
