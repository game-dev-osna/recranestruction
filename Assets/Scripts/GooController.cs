using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GooController : MonoBehaviour
{
    [SerializeField] private InputAction _movement;
    [SerializeField] private float _speed;
    public Transform bottom;

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
        var movementVector = new Vector3(value.x, 0f, value.y);
        _gooBody.AddForceAtPosition(movementVector * Time.deltaTime * _speed, bottom.position);
    }
}
