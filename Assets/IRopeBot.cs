using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class IRopeBot : MonoBehaviour
{
    public float thickness = 1f;
    public InputAction _segmentControl;
    public GameObject _ropePartPrefab;
    public GameObject _alien;
    public int _ropeLength;
    private Rigidbody _rigidbody;
    private LineRenderer _lineRenderer;
    private LinkedList<HingeJoint> _joints = new LinkedList<HingeJoint>();

    private bool _pressed;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _lineRenderer = GetComponent<LineRenderer>();
        _segmentControl.Enable();

        _alien.GetComponent<HingeJoint>().connectedBody = _rigidbody;
    }

    private void Update()
    {
        RenderTheFrickinRope();

        if(_pressed && _segmentControl.ReadValue<float>() < 0.5f)
        {
            _pressed = false;
            return;
        }

        if(_pressed)
        {
            return;
        }

        _pressed = _segmentControl.ReadValue<float>() > 0.5f;
        
        if (_pressed)
        {
            AddSegment();
        }
    }

    private void OnDisable()
    {
        _segmentControl.Disable();
    }

    private void AddSegment()
    {
        var lastInstance = _joints.Count > 0 ? _joints.Last().transform : transform;
        lastInstance.GetComponent<Collider>().enabled = false;

        var newInstance = Instantiate(_ropePartPrefab, lastInstance.position + Vector3.down * 0.8f, Quaternion.identity, transform.parent.transform);
        //newInstance.GetComponent<Collider>().enabled = true;
        var newJoint = newInstance.GetComponent<HingeJoint>();
        newJoint.connectedBody = lastInstance.GetComponent<Rigidbody>();
        _joints.AddLast(newJoint);

        _alien.GetComponent<HingeJoint>().connectedBody = newInstance.GetComponent<Rigidbody>();
        _alien.transform.position = newInstance.transform.position + Vector3.down * 0.8f;
    }

    private void RenderTheFrickinRope()
    {
        var positions = new List<Vector3>();
        positions.Add(transform.position);

        foreach(var child in _joints)
        {
            positions.Add(child.transform.position);
        }

        _lineRenderer.positionCount = positions.Count;
        _lineRenderer.SetPositions(positions.ToArray());
        _lineRenderer.startWidth = thickness;
        _lineRenderer.endWidth = thickness;
    }

    /*private void RemoveSegment()
    {
        var newJoint = newInstance.GetComponent<ConfigurableJoint>();
        _joints.AddFirst(newJoint);
    }*/
}
