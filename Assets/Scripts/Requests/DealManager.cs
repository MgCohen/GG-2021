using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DealManager : Persistable<DealManager>, ITickable
{
    public override void BeforeSave()
    {
    }

    public override void OnLoad()
    {
    }

    public long nextReset;

    public List<DealView> deals = new List<DealView>();

    [Inject]
    private DealService dealService;

    [Inject]
    private DealView.Factory factory;

    [Inject]
    private Transform container;

    public void Init()
    {
        if (nextReset == 0)
        {
            SetFirstResetDate();
        }
    }

    public void SetFirstResetDate()
    {
        var Today = DateTime.Today;
        nextReset = new DateTime(Today.Year, Today.Month, Today.Day, 23, 59, 59).Ticks;
    }

    public void Tick()
    {
        if (DateTime.Now.Ticks >= nextReset)
        {

        }
    }

    private void ResetDeals()
    {
        nextReset = new DateTime(nextReset).AddDays(1).Ticks;
        foreach (var deal in deals)
        {
            GameObject.Destroy(deal.gameObject);
        }
        deals.Clear();
        for (int i = 0; i < 4; i++)
        {
            Deal newDeal = dealService.GenerateDeal();
            DealView dv = factory.Create(newDeal, container);
        }
    }
}
