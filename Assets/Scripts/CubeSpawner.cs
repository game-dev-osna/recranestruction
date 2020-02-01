using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var c = GameObject.CreatePrimitive(PrimitiveType.Cube);
            c.transform.localScale = 0.1f * Vector3.one;
            c.transform.position = transform.position;
            c.AddComponent<Rigidbody>();
        }
    }
}