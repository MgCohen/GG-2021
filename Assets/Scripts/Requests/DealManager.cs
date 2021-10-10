using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DealManager : Persistable<DealManager>, ITickable
{

    [Inject]
    private SignalBus signals;
    [Inject]
    private DealService dealService;

    public List<Deal> deals = new List<Deal>();

    public override void BeforeSave()
    {
    }

    public override void OnLoad()
    {
        if (nextReset == 0)
        {
            SetFirstResetDate();
        }
    }

    public long nextReset;

    public void SetFirstResetDate()
    {
        var Today = DateTime.Today;
        nextReset = new DateTime(Today.Year, Today.Month, Today.Day, 23, 59, 59).Ticks;
    }

    public void Tick()
    {
        if (DateTime.Now.Ticks >= nextReset)
        {
            nextReset = new DateTime(nextReset).AddDays(1).Ticks;
            deals.Clear();

            for (int i = 0; i < 4; i++)
            {
                deals.Add(dealService.GenerateDeal());
            }

            signals.Fire(new OnDealRefreshSignal());
        }
    }
}
