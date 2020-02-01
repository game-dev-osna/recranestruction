using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlobControl : MonoBehaviour
{

    private List<Transform> connectedObjects = new List<Transform>();
    private List<Transform> collidingObjects = new List<Transform>();
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

        if (newGrabbing)
            AttachCollidingObjects();

        if (!newGrabbing && _grabbing)
            DetachCollidingObjects();

        _grabbing = newGrabbing;
    }

    void AttachCollidingObjects()
    {
        foreach (Transform collider in collidingObjects)
        {
            if(connectedObjects.Contains(collider))
            {
                continue;
            }

            collider.SetParent(transform);
            collider.GetComponent<Rigidbody>().isKinematic = true;
            connectedObjects.Add(collider);
        }
    }

    void DetachCollidingObjects()
    {
        for (int i = connectedObjects.Count - 1; i > -1; i--)
        {
            Transform collider = connectedObjects[i];
            collider.SetParent(null);
            var body = collider.GetComponent<Rigidbody>();
            body.isKinematic = false;
            body.velocity = blobRigidbody.velocity;

            connectedObjects.Remove(collider);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"## Collided with : {other.gameObject.name}");
        if (!other.gameObject.CompareTag("Draggable"))
            return;

        Debug.Log($"## Added : {other.gameObject.name}");
        collidingObjects.Add(other.gameObject.transform);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!collidingObjects.Contains(other.gameObject.transform))
            return;

        Debug.Log($"## Remove : {other.gameObject.name}");
        collidingObjects.Remove(other.gameObject.transform);
    }

}
