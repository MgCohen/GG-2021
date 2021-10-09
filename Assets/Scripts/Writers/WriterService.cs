using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WriterService
{
    [Inject]
    GameUtility utility;

    public Writer CreateNewWriter(int level)
    {
        Theme randomTheme = utility.themes[Random.Range(0, 3)];
        int value = Random.Range(level - 1, level + 1);
        Debug.Log(randomTheme);
        List<ThemeLevel> themes = new List<ThemeLevel>() { new ThemeLevel(randomTheme, value) };
        return new Writer(themes, 2, "Juska");
    }
}
