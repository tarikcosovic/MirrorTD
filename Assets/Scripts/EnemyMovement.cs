using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 10.0f;

    private Vector3 dir;
    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        target = Waypoints.waypoints[0];
        dir = target.position - transform.position;
    }

    private void Update()
    {
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
            GetNextWaypoint();
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            enabled = false;
            return;
        }

        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
        dir = target.position - transform.position;
    }
}
