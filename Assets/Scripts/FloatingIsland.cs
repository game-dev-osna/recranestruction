using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingIsland : MonoBehaviour
{
    public float speed = 1f;

    public Transform waypointArea;

    private Bounds area;
    private Vector3 target;
    private Rigidbody rb;
    private Vector3 waypoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = transform.position;
        area = new Bounds(waypointArea.transform.position, waypointArea.transform.localScale);
        waypoint = transform.position;
    }

    void FixedUpdate()
    {
        float dist = (waypoint - transform.position).magnitude;
        if (dist < .01f) {
            waypoint = GenerateNewWaypoint();
            return;
        }
        Vector3 direction = (waypoint - transform.position) / dist;
        target += speed * Time.deltaTime * direction;
        rb.MovePosition(Vector3.Lerp(transform.position, target, speed * Time.deltaTime));
    }

    private Vector3 GenerateNewWaypoint()
    {
        float x = UnityEngine.Random.Range(area.min.x,area.max.x);
        float y = UnityEngine.Random.Range(area.min.y,area.max.y);
        float z = UnityEngine.Random.Range(area.min.z,area.max.z);
        return new Vector3(x, y, z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            other.transform.parent = transform;
            if (other.TryGetComponent<Collider>(out Collider coll)) {
                coll.enabled = false;
            }
            if (other.TryGetComponent<Rigidbody>(out Rigidbody rb)) {
                rb.isKinematic = true;
            }
        }
    }
}
