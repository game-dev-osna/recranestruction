using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JurassicCar : MonoBehaviour
{
    public Transform exploder;
    public float _explosionEngineForce = 0.5f;
    private Rigidbody _rigidbody;
    public Transform target;
    // Start is called before the first frame update

    private Vector3 _startPosition;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = (target.position - transform.position).normalized;
        _rigidbody.AddForce(direction * _explosionEngineForce);
    }

    public void MakeTheCarMoveIntoTheDirectionItIsCurrentlyLookingAtWithAPredefinedVelocityPlease(float force)
    {
        _explosionEngineForce = force;
    }

    public void ResetCar()
    {
        transform.position = _startPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _explosionEngineForce = 0f;
    }
}
