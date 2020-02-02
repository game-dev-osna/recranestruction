using UnityEngine;

public enum DragState { Idle, InRange, Grabbed }

public class DragableObject : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Rigidbody _rigidbody;
    private Color _defaultColor;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _defaultColor = _renderer.material.GetColor("_Color");
        SetState(DragState.Idle);
    }

    public void SetState(DragState state)
    {
        switch(state)
        {
            case DragState.Grabbed: HandleGrabbed(); break;
            case DragState.InRange: HandleInRange(); break;
            default: HandleIdle(); break;
        }
    }

    private void HandleIdle()
    {
        SetColor(_defaultColor);
        //_rigidbody.isKinematic = true;
        //_rigidbody.constraints= RigidbodyConstraints.FreezeAll;
    }

    private void HandleGrabbed()
    {
        SetColor(Color.blue);
    }

    private void HandleInRange()
    {
        SetColor(Color.green);
    }

    private void SetColor(Color color)
    {
        _renderer.material.SetColor("_Color", color);
    }
}
