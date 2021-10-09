using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.UI;

public class ThemeView : MonoBehaviour
{
    public GameObject numberHolder;
    public TextMeshProUGUI themeValue;
    public Image icon;
    
    [Inject]
    public void Init(ThemeLevel level)
    {
        Debug.Log(level);
        icon.sprite = level.theme.icon;
        if(level.level <= 1)
        {
            numberHolder.SetActive(false);
            return;
        }

        themeValue.text = level.level.ToString();
    }

    public class Factory: PlaceholderFactory<ThemeLevel, ThemeView>
    {

    }
}
