using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StoryView : MonoBehaviour
{
   
    public void SetSelectable()
    {

    }


    public class Factory: PlaceholderFactory<Story, Transform, StoryView>
    {

    }
}
