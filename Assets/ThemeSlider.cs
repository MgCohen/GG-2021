using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class ThemeSlider : MonoBehaviour
{
    public Slider slider;
    public Transform themeViewContainer;
    public TextMeshProUGUI valueText;

    [Inject]
    public ThemeView.Factory factory;

    public int currentValue;
    public Theme theme;

    [Inject]
    public void Set(ThemeLevel themeLevel, Transform parent)
    {
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        factory.Create(themeLevel, themeViewContainer);
        slider.onValueChanged.AddListener(UpdateValue);
        slider.maxValue = themeLevel.level;
        theme = themeLevel.theme;
    }

    public void UpdateValue(float value)
    {
        currentValue = (int)value;
        valueText.text = value.ToString();
    }

    public class Factory : PlaceholderFactory<ThemeLevel, Transform, ThemeSlider>
    {

    }
}
