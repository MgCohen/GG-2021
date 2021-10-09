using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DealView : MonoBehaviour
{


    public class Factory: PlaceholderFactory<Deal, Transform, DealView>
    {

    }
}
