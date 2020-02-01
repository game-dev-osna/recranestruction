using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingIsland : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 waypoint;
    public Bounds waypointArea;

    private Vector3 target;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = transform.position;
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
        float x = UnityEngine.Random.Range(waypointArea.min.x,waypointArea.max.x);
        float y = UnityEngine.Random.Range(waypointArea.min.y,waypointArea.max.y);
        float z = UnityEngine.Random.Range(waypointArea.min.z,waypointArea.max.z);
        return new Vector3(x, y, z);
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Draggable"))
    //     other.transform.parent = transform;
    // }
}
