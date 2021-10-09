using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriterService
{
    public Writer CreateNewWriter(int level)
    {
        return new Writer(new List<ThemeLevel>(), 2, "Juska");
    }
}
