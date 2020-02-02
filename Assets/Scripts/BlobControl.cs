using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlobControl : MonoBehaviour
{
    private List<Transform> collidingObjects = new List<Transform>();
    private Transform connectedObject;
    private Rigidbody blobRigidbody;

    [SerializeField] private InputAction _grabAction;

    private bool _grabbing = false;

    // Start is called before the first frame update
    void Start()
    {
        blobRigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        _grabAction.Enable();
    }

    void OnDisable()
    {
        _grabAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        bool newGrabbing = _grabAction.ReadValue<float>() >= 0.5f;
            
        if (newGrabbing && !_grabbing)
            AttachCollidingObject();

        if (!newGrabbing && _grabbing)
            DetachCollidingObject();

        _grabbing = newGrabbing;
    }

    void AttachCollidingObject()
    {
        if(!collidingObjects.Any())
        {
            return;
        }

        var collidingObject = collidingObjects.First();
        collidingObject.SetParent(transform);
        collidingObject.GetComponent<Rigidbody>().isKinematic = true;
        //collidingObject.GetComponent<Rigidbody>().constraints= RigidbodyConstraints.FreezeAll;
        connectedObject = collidingObject;
        connectedObject.gameObject.GetComponentInChildren<DragableObject>().SetState(DragState.Grabbed);
    }

    void DetachCollidingObject()
    {
        if (connectedObject == null)
        {
            return;
        }

        connectedObject.SetParent(null);
        var body = connectedObject.GetComponent<Rigidbody>();
        body.isKinematic = false;
        body.velocity = blobRigidbody.velocity;
        //body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        connectedObject.gameObject.GetComponentInChildren<DragableObject>().SetState(DragState.Idle);
        connectedObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"## Collided with : {other.gameObject.name}");
        if (!other.gameObject.CompareTag("Draggable"))
            return;

        Debug.Log($"## Added : {other.gameObject.name}");
        collidingObjects.Add(other.gameObject.transform);
        other.gameObject.GetComponentInChildren<DragableObject>().SetState(DragState.InRange);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!collidingObjects.Contains(other.gameObject.transform))
            return;

        Debug.Log($"## Remove : {other.gameObject.name}");
        collidingObjects.Remove(other.gameObject.transform);
        other.gameObject.GetComponentInChildren<DragableObject>().SetState(DragState.Idle);
    }

}
