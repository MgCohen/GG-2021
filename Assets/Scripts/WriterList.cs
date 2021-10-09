using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WriterList : MonoBehaviour
{
    [Inject]
    private WorkerView.Factory factory;
    [Inject]
    private SignalBus signals;

    [Inject]
    private WriterManager manager;
    [Inject]
    private WriterService service;

    public Transform listContainer;

    public void Start()
    {
        signals.Subscribe<OnWriterAddedSignal>(OnWriterHired);
    }

    public void OnWriterHired(OnWriterAddedSignal signal)
    {
        var workView = factory.Create();
        workView.transform.SetParent(listContainer);
        workView.transform.localScale = Vector3.one;
        workView.Init(signal.Writer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            manager.AddWorker(service.CreateNewWriter(1));
        }
    }


}
