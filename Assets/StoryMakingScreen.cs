using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using System;
using UnityEngine.UI;
using TMPro;

public class StoryMakingScreen : MonoBehaviour
{
    [Inject]
    public ThemeSlider.Factory factory;

    public List<ThemeSlider> sliders = new List<ThemeSlider>();

    public Transform sliderContainer;
    public Writer writer;

    public Action<List<ThemeLevel>> selectCallback;

    public TextMeshProUGUI timeText;

    public void Set(List<ThemeLevel> themes, Writer writer, Action<List<ThemeLevel>> callback)
    {
        gameObject.SetActive(true);

        this.writer = writer;
        ResetSliders();
        SpawnSliders(themes);
        selectCallback = callback;
    }

    private void SpawnSliders(List<ThemeLevel> themes)
    {
        foreach (var theme in themes)
        {
            Debug.Log(theme);
            var slider = factory.Create(theme, sliderContainer);
            sliders.Add(slider);
            slider.transform.SetSiblingIndex(1);
        }
    }

    private void ResetSliders()
    {
        foreach (var slider in sliders)
        {
            Destroy(slider.gameObject);
        }
        sliders.Clear();
    }

    public void Callback()
    {
        if (selectCallback != null)
        {
            selectCallback.Invoke(RetrieveValues());
        }
        selectCallback = null;
        gameObject.SetActive(false);
    }

    public List<ThemeLevel> RetrieveValues()
    {
        List<ThemeLevel> list = new List<ThemeLevel>();
        foreach (var slider in sliders)
        {
            list.Add(new ThemeLevel(slider.theme, slider.currentValue));
        }
        return list;
    }

    private void Update()
    {
        var timeLeft  = new TimeSpan(writer.CalculateWorkTime(new Story(RetrieveValues())));
        timeText.text = "Time To Complete: " + timeLeft.Hours + "h " + timeLeft.Minutes + "m " + timeLeft.Seconds + "s";
    }
}
