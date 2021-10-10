using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using TMPro;

public class DealListing : MonoBehaviour
{

    public List<DealView> deals = new List<DealView>();

    public Transform container;

    [Inject]
    private DealManager dealManager;
    [Inject]
    private DealView.Factory factory;
    [Inject]
    private SignalBus signals;

    public TextMeshProUGUI timer;

    private void Start()
    {
        signals.Subscribe<OnDealRefreshSignal>(ResetDeals);
        ResetDeals();
    }

    private void Update()
    {
        if (timer == null) return;

        timer.text = new TimeSpan((dealManager.nextReset - DateTime.Now.Ticks)).ToString();
    }

    private void ResetDeals()
    {
        foreach (var deal in deals)
        {
            GameObject.Destroy(deal.gameObject);
        }

        deals.Clear();

        foreach(Deal deal in dealManager.deals)
        {
            DealView dv = factory.Create(deal, container);
            deals.Add(dv);
        }
    }
}
