using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraneControl : MonoBehaviour
{
    [SerializeField] private InputAction _rotate;
    [SerializeField] private float _acceleration;
    [SerializeField] private Transform _mainPart;
    [SerializeField] private float _friction;
    [SerializeField] private float _maxSpeed;

    private float speed;

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
        float accel = _acceleration * _rotate.ReadValue<float>();
        float wouldBeSpeed = (speed + Time.deltaTime * accel) * _friction;
        speed = Mathf.Clamp(wouldBeSpeed, -_maxSpeed, _maxSpeed);
        // float wouldBeSpeed = (speed + Time.deltaTime * accel) - Mathf.Sign(speed) * _friction * Time.deltaTime;
        // speed = speed < 0 ? Mathf.Clamp(wouldBeSpeed, -_maxSpeed, 0) : Mathf.Clamp(wouldBeSpeed, 0, _maxSpeed);
        _mainPart.localEulerAngles += Time.deltaTime * new Vector3(0,speed,0);
    }
}
