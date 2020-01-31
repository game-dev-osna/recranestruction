﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraneControl : MonoBehaviour
{
    [SerializeField] private InputAction _rotate;
    [SerializeField] private float _rotationSpeed;
    
    void OnEnable()
    {
        _rotate.Enable();
    }

    void OnDisable()
    {
        _rotate.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var value = _rotate.ReadValue<float>();
        transform.localEulerAngles += new Vector3(0,value,0) * Time.deltaTime * _rotationSpeed;
    }
}
