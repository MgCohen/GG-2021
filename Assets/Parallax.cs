using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    public Vector2 speed;
    public Image image;

    private void Update()
    {
        image.material.mainTextureOffset += speed * Time.deltaTime;
    }
}
