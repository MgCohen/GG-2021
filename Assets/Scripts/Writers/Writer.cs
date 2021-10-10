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
    public long speed;

    public WorkStatus workStatus = WorkStatus.Idle;
    public Story currentWork;
    public long completionTime;

    public void StartWorking(Story work)
    {
        Debug.Log(1);
        completionTime = CalculateWorkTime(work);
        workStatus = WorkStatus.Working;
        currentWork = work;
    }

    public long CalculateWorkTime(Story work)
    {
        long counter = 0;
        foreach (var themeLevel in work.themes)
        {
            //Debug.Log(utility.StoryTicksPerLevel);
            counter += (themeLevel.level * 60) / speed;
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