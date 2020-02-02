using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class IRopeBot : MonoBehaviour
{
    public float thickness = 1f;
    public float ropeSpeed = 1f;
    public float segmentSize = 0.4f;
    public InputAction _segmentControl;
    public GameObject _ropePartPrefab;
    public GameObject _alien;
    public float _ropeLength;
    private Rigidbody _rigidbody;
    private LineRenderer _lineRenderer;
    private LinkedList<HingeJoint> _joints = new LinkedList<HingeJoint>();
    [Header("sound settings")]
    private AudioSource _audioSource;
    public float _minPitch = 0.9f;
    public float _maxPitch = 1.1f;


    private bool _pressed;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _lineRenderer = GetComponent<LineRenderer>();
        _segmentControl.Enable();

        _alien.GetComponent<HingeJoint>().connectedBody = _rigidbody;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float inputValue = _segmentControl.ReadValue<float>();
        _audioSource.pitch = Mathf.Lerp(_minPitch, _maxPitch, inputValue * .5f + .5f);
        if (Mathf.Approximately(inputValue, 0f))
        {
            _audioSource.Stop();
        }
        else if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        _ropeLength += inputValue * Time.deltaTime * ropeSpeed;
        RenderTheFrickinRope();

        var roundedRopeLength = Mathf.Round(_ropeLength);
        if (roundedRopeLength > _joints.Count)
        {
            for(int i = 0; i < roundedRopeLength - _joints.Count; i++)
            {
                AddSegment();
            }
        }

        if (_joints.Count > roundedRopeLength)
        {
            for (int i = 0; i < _joints.Count - roundedRopeLength; i++)
            {
                RemoveSegment();
            }
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

        var newInstance = Instantiate(_ropePartPrefab, lastInstance.position /*+ Vector3.down * segmentSize*/, lastInstance.rotation, transform.parent.transform);
        //newInstance.GetComponent<Collider>().enabled = true;
        var newJoint = newInstance.GetComponent<HingeJoint>();
        newJoint.anchor = new Vector3(0, segmentSize, 0);
        newJoint.connectedAnchor = new Vector3(0, -segmentSize, 0);
        newJoint.connectedBody = lastInstance.GetComponent<Rigidbody>();
        _joints.AddLast(newJoint);

        _alien.GetComponent<HingeJoint>().connectedBody = newInstance.GetComponent<Rigidbody>();
        _alien.transform.position = newInstance.transform.position /*+ Vector3.down * segmentSize*/;
    }

    private void RemoveSegment()
    {
        if(_joints.Count == 0)
        {
            return;
        }

        var lastInstance = _joints.Last().transform;

        _joints.Remove(lastInstance.GetComponent<HingeJoint>());
        Destroy(lastInstance.gameObject);

        _alien.GetComponent<HingeJoint>().connectedBody = _joints.Count > 0 ? _joints.Last().GetComponent<Rigidbody>() : transform.GetComponent<Rigidbody>();
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
        _lineRenderer.SetWidth(thickness, thickness);
    }
}
