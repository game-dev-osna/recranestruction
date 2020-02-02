using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{

    float sWidth;
    float sHeight;

    RectTransform canvasTransform;

    // Start is called before the first frame update
    void Start()
    {
        sWidth = Screen.width;
        sHeight = Screen.height;

        canvasTransform = GetComponent<RectTransform>();

        //Debug.Log($"Width: {sWidth} Height: {sHeight}");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
