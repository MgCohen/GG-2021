using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Theme")]
public class Theme : ScriptableObject
{
    [Inject]
    GameUtility utility;

    [Inject]
    private void Init()
    {

        utility.themes.Add(this);
    }

    public Sprite icon;
}

[System.Serializable]
public class ThemeLevel
{
    public ThemeLevel(Theme theme, int level)
    {
        this.theme = theme;
        this.level = level;
    }

    public Theme theme;
    public int level;
}
