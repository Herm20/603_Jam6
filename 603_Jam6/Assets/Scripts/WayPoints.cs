
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public List<Transform> wayPoints;

    private void Awake()
    {
        wayPoints = new List<Transform>(gameObject.GetComponentsInChildren<Transform>());
        wayPoints.RemoveAt(0);
    }
    private void OnDrawGizmos()
    {
        foreach ( var wayPoint in wayPoints) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(wayPoint.position, 0.2f);
        }
    }
}
