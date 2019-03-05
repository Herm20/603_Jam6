using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private GameObject enem;
    private float startTime;
    private float distance;
    private Vector3 startPosition;

    private GameObject[] enemies;

    private float maxLength = 3;

    private float speed = 5;

    private bool isEnemy = false;
    // Start is called before the first frame update
    void Start()
    {   
        
        startPosition = transform.position;
        startTime = Time.time;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length != 0) {
            float distanceToClosestEnemy = Mathf.Infinity;
            foreach(GameObject e in enemies){
                float currentLength = Vector2.Distance(e.transform.position, startPosition);
                if (currentLength < distanceToClosestEnemy){
                    enem = e;
                    distanceToClosestEnemy = currentLength;
                } 

            }

            if(gameObject.name == "GreenAttack(Clone)"){
                enem = enemies[enemies.Length - 1];
                speed = 20;
            }   
        
            distance = Vector2.Distance(transform.position, enem.transform.position);

            

            if (distance > maxLength & gameObject.name != "GreenAttack(Clone)") Destroy(gameObject);
            isEnemy = true;
        }

        if (enem == null) Debug.Log("Enemy is null");
    }

    // Update is called once per frame
    void Update()
    {
        if (enem == null) Destroy(gameObject);
        if (isEnemy & enem != null){
            float timeInterval = Time.time - startTime;
            gameObject.transform.position = Vector3.Lerp(startPosition, enem.transform.position, timeInterval * speed / distance);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D){
		Destroy(gameObject);

        Enemy e = collider2D.GetComponent<Enemy>();

        if(e != null){
            if(gameObject.name == "BlueAttack(Clone)"){
                e.speedReduce(1);
                e.damage(3);
            }

            if(gameObject.name == "GreenAttack(Clone)"){
                e.damage(3);
            }

            if(gameObject.name == "OrangeAttack(Clone)"){
                e.damage(6);
            }
        }
	}
}
