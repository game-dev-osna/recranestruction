using System.Collections.Generic;
using UnityEngine;

public enum BuildingBlockType { Type0, Type1, Type2};

public class BuildingBlockScript : MonoBehaviour
{
    private List<SphereCollider> sCollider = new List<SphereCollider>();
    private List<GameObject> collidingObjects = new List<GameObject>();

    public GameObject ghost;
    public float overlapFactor = 00.0f;

    public int gridSize = 2;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoundingSpheres();
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }


    // Update is called once per frame
    void Update()
    {

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        foreach (var mf in GetComponentsInChildren<MeshFilter>())
        {
            Gizmos.matrix = mf.transform.localToWorldMatrix;
            Mesh m = mf.sharedMesh;
            Gizmos.DrawWireCube(m.bounds.center, m.bounds.size);
        }
    }

    void GenerateBoundingSpheres()
    {
        MeshFilter mf = GetComponentInChildren<MeshFilter>();
        Mesh m = mf.sharedMesh;

        Vector3 meshExtents = m.bounds.extents;
        meshExtents.Scale(mf.transform.localScale);

        Debug.Log($"Extends: {meshExtents}");

        Vector3 center = mf.gameObject.transform.localPosition;
        Debug.Log($"Center: {transform.position}, {mf.gameObject.transform.localPosition}, {center}");
        //center.Scale(mf.transform.localScale);


        float radius = (meshExtents.x)/((float)gridSize * 2.0f);

        for (float x = -meshExtents.x + radius; x < meshExtents.x; x += radius * 2.0f)
        {
            for (float y = -meshExtents.y + radius; y < meshExtents.y; y += radius * 2.0f)
            {
                for (float z = -meshExtents.z + radius; z < meshExtents.z; z += radius * 2.0f)
                {
                    AddBoundingSphere(center + new Vector3(x, y, z), radius);
                }
            }
        }
    }

    void AddBoundingSphere(Vector3 center, float radius)
    {
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.center = center;
        sphereCollider.radius = radius;
        sphereCollider.isTrigger = true;
        sCollider.Add(sphereCollider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != ghost)
            return;
        collidingObjects.Add(other.gameObject);
        overlapFactor = (float)collidingObjects.Count/(float)sCollider.Count;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!collidingObjects.Contains(other.gameObject))
            return;
        collidingObjects.Remove(other.gameObject);
        overlapFactor = (float)collidingObjects.Count / (float)sCollider.Count;
    }

}
