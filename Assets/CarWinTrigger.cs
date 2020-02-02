using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWinTrigger : MonoBehaviour
{
    public CarGameController _controller;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Car"))
        {
            _controller.OnWin();
            Debug.LogError("gewonnen!");
        }
    }
}
