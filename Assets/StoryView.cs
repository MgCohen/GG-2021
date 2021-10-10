using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;


public class StoryView : MonoBehaviour
{
    public Story story;

    public bool isSelected = false;
    public Button button;
    public Image backGround;

    public Color selectedColor;
    public Color normalColor;

    private void Start()
    {
        button.onClick.AddListener(ToggleSelection);
    }

    [Inject]
    public void Init(Story story, Transform parent)
    {
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        this.story = story;
    }

    public void SetSelectable(bool state)
    {
        button.interactable = state;
    }

    public void ToggleSelection()
    {
        isSelected = !isSelected;
        backGround.color = isSelected ? selectedColor : normalColor;
    }



    public class Factory: PlaceholderFactory<Story, Transform, StoryView>
    {

    }
}
