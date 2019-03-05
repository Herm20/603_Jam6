using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private GameObject enemy;
    private float startTime;
    private float distance;
    private Vector3 startPosition;

    public float speed = 5;

    private bool isEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        startPosition = transform.position;
        startTime = Time.time;

        if (enemies.Length != 0) {
            enemy = enemies[0];
            float length = Vector2.Distance(startPosition, enemy.transform.position);
            foreach(GameObject e in enemies){
                float currentLength = Vector2.Distance(e.transform.position, startPosition);
                if (currentLength < length) enemy = e;
            }
        
            distance = Vector2.Distance (transform.position, enemy.transform.position);
            isEnemy = true;
        }
            
        

        if (enemy == null) Debug.Log("Enemy is null");
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy & enemy != null){
            float timeInterval = Time.time - startTime;
            gameObject.transform.position = Vector3.Lerp(startPosition, enemy.transform.position, timeInterval * speed / distance);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D){
		Destroy(gameObject);
	}
}
