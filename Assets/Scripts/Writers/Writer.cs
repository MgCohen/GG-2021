using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Zenject;

[System.Serializable]
public class Writer
{
    public Writer(List<ThemeLevel> themes, int speed, string writerName)
    {
        workQuality = themes;
        this.speed = speed;
        this.writerName = writerName;
    }


    [Inject]
    private GameUtility utility;

    [SerializeField]
    public List<ThemeLevel> workQuality;

    public string writerName;
    public int speed;

    public WorkStatus workStatus = WorkStatus.Idle;
    public Story currentWork;
    public long completionTime;

    public void StartWorking(Story work)
    {
        completionTime = CalculateWorkTime(work);
        workStatus = WorkStatus.Working;
        currentWork = work;
    }

    public int CalculateWorkTime(Story work)
    {
        int counter = 0;
        foreach (var themeLevel in work.themes)
        {
            counter += (themeLevel.level * utility.StoryTicksPerLevel) / speed;
        }
        return counter;
    }

    public void Work()
    {
        if (DateTime.Now.Ticks > completionTime)
        {
            workStatus = WorkStatus.finished;
        }
    }

    public Story CollectWork()
    {
        workStatus = WorkStatus.Idle;
        return currentWork;
    }

}

public enum WorkStatus
{
    Idle,
    Working,
    finished
}