using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public abstract class Persistable<T> : IInitializable, IDisposable
{
    [Inject]
    private SaveManager saveManager;

    public void Dispose()
    {
        BeforeSave();
        saveManager.Save(this);
    }

    public abstract void BeforeSave();

    public void Initialize()
    {
        saveManager.Load(this);
        OnLoad();
    }

    public abstract void OnLoad();
}
