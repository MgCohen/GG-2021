using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WriterManager : Persistable<WriterManager>, ITickable
{
    [Inject]
    private SignalBus signals;

    public List<Writer> writers = new List<Writer>();

    public void AddWorker(Writer writer)
    {
        writers.Add(writer);
        signals.Fire(new OnWriterAddedSignal(writer));
    }

    public override void BeforeSave()
    {
    }

    public override void OnLoad()
    {
        foreach(Writer writer in writers)
        {
            signals.Fire(new OnWriterAddedSignal(writer));
        }
    }

    public void Tick()
    {
        foreach (var writer in writers)
        {
            if (writer.workStatus == WorkStatus.Working)
            {
                writer.Work();
            }
        }
    }
}

