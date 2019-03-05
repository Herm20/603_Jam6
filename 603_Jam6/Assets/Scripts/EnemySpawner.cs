using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float beatInterval;

    private GameObject enemy;

    private float currentBeat;

    // Start is called before the first frame update
    void Start() {
        enemy = (GameObject)Resources.Load("Prefabs/Enemy/EnemyBug");
    }

    public void startNewWave(int num)
    {
        StartCoroutine(spawn(num));
    }

    IEnumerator spawn(int num)
    {
        int counter = 0;
        int currentBeat = -1;
        while (counter < num)
        {
            if (Beats._instance.counter % beatInterval == 0 & Beats._instance.counter != currentBeat){
                Instantiate(enemy, enemy.transform.position, Quaternion.identity);
                currentBeat = Beats._instance.counter;
                counter++;
            }
            yield return null;
        }
        yield return null;
    }

}
