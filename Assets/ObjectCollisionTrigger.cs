using UnityEngine;

public enum WaterLevelTriggerType { Lost, Moved}

public class ObjectCollisionTrigger : MonoBehaviour
{
    public WaterLevelTriggerType _type;
    public WaterLevelController _controller;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Draggable"))
        {
            return;
        }

        if (other.transform.parent != null)
        {
            return;
        }

        if (_type == WaterLevelTriggerType.Lost)
        {
            _controller.OnObjectWasLost(other.transform);
        }

        if (_type == WaterLevelTriggerType.Moved)
        {
            _controller.OnObjectWasMoved(other.transform);
        }
    }
}
