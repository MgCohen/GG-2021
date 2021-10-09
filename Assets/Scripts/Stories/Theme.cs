using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Theme")]
public class Theme : ScriptableObject
{
    public Sprite icon;
}

public class ThemeLevel
{
    public Theme theme;
    public int level;
}
