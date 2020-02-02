using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCarButton : MonoBehaviour
{
    public CarGameController _controller;
    public Text _text;

    private bool _carActive;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        if(_carActive)
        {
            _controller.ResetCar();
            _carActive = false;
            _text.text = "Start";
            return;
        }

        _carActive = true;
        _controller.StartCar();
        _text.text = "Reset";
    }
}
