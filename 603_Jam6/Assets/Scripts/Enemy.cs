using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed;
    private WayPoints wayPoints;

    private int waypointIndex = 0;

    void Start() {
       wayPoints = GameObject.FindGameObjectWithTag("WayPoints").GetComponent<WayPoints>(); 
    }    

    void Update(){
        transform.position = Vector2.MoveTowards(transform.position, wayPoints.waypoints[waypointIndex].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, wayPoints.waypoints[waypointIndex].position) < 0.1f){
            if(waypointIndex < wayPoints.waypoints.Length - 1) {
                waypointIndex++;
                Vector3 direction = wayPoints.waypoints[waypointIndex].position - transform.position;
                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else Destroy(gameObject);
        } 
    }
}
