using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GooController : MonoBehaviour
{
    [SerializeField] private InputAction _movement;
    [SerializeField] private float _speed;
    public Vector2 thrust;

    private Rigidbody _gooBody;


    // Start is called before the first frame update
    void Start()
    {
        _movement.Enable();
        _gooBody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        _movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var value = _movement.ReadValue<Vector2>();
        thrust = value;
        var movementVector = new Vector3(value.x, 0f, value.y);
        _gooBody.AddForce(movementVector * Time.deltaTime * _speed * 10000);
    }
}
