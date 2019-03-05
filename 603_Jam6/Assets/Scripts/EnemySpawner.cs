using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float beatInterval = 3;

    private GameObject enemy;

    private bool isGenerated;

    private float currentBeat;

    
    // Start is called before the first frame update
    void Start() {
        isGenerated = false; 
        currentBeat = 0;
        enemy = (GameObject)Resources.Load("Prefabs/Enemy/EnemyBug");
    }

    // Update is called once per frame
    void Update() {

        if (currentBeat != Beats._instance.counter) isGenerated = false;
        if (((Beats._instance.counter % beatInterval) == 0) & isGenerated == false){
            Instantiate(enemy, enemy.transform.position, Quaternion.identity);
            currentBeat = Beats._instance.counter;
            isGenerated = true;
        }

        
    }
}
