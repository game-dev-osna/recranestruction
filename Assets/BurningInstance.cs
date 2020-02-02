using System.Linq;
using UnityEngine;

public class BurningInstance : MonoBehaviour
{
    public FireLevelGameManager _manager;

    private void RemoveFire()
    {
        _manager.FireRemoved();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water") || other.gameObject.GetComponentInChildren<GooController>() != null)
        {
            RemoveFire();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water") || collision.gameObject.GetComponentInChildren<GooController>() != null)
        {
            RemoveFire();
        }
    }
}
