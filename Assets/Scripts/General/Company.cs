using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    public int level;
    public int xp;
    public int gold;

    public void AddReward(int goldAmount, int xpAmount)
    {
        xp += xpAmount;
        gold += goldAmount;
    }
}
