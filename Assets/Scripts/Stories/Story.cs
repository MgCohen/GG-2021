using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Story
{
    public Story(List<ThemeLevel> themes)
    {
        foreach(var theme in themes)
        {
            this.themes.Add(new ThemeLevel(theme.theme, theme.level));
        }
    }

    public List<ThemeLevel> themes = new List<ThemeLevel>();
}
