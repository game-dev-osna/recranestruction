using UnityEngine;

public class FloorMarker : MonoBehaviour
{
    public GameObject _floorMaker;
    public LayerMask _layerMask;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.down * 0.5f, Vector3.down, out hit, Mathf.Infinity, _layerMask))
        {
            _floorMaker.transform.position = hit.point;
        }
    }
}
