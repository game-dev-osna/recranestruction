using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObject : MonoBehaviour
{
    public float interval = 0.5f;
    public Material solidMat;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(HandlePosition), 0f, interval);
        _rigidbody = GetComponent<Rigidbody>();
        SetRigidbody(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandlePosition()
    {
        transform.position += Vector3.down * 0.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Draggable"))
        {
            CancelInvoke(nameof(HandlePosition));
            SetRigidbody(true);
            InvokeRepeating(nameof(CheckSolid), 1f, 0.5f);
        }
    }

    private void SetRigidbody(bool state)
    {
        if(!state)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            return;
        }

        _rigidbody.isKinematic = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
    }

    private void CheckSolid()
    {
        Debug.Log(_rigidbody.velocity.magnitude);
        if (_rigidbody.velocity.magnitude < 0.2f)
        {
            MakeSolid();
            CancelInvoke(nameof(CheckSolid));
        }
    }

    private void MakeSolid()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<MeshRenderer>().material = solidMat;
    }
}
