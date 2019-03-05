using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour {

    private GameObject bullet;

    public string prefabPath = "Prefabs/Attacks/GreenAttack";

    public Transform firePoint;

    private bool isGenerated = false;

    private float CurrentBeat;

    public float beatInterval = 3;


    void Start(){
        bullet = (GameObject)Resources.Load(prefabPath);
        Instantiate(bullet, firePoint.position, Quaternion.identity);

        CurrentBeat = 0;
        
    }

    void Update(){
        if (CurrentBeat != Beats._instance.counter) isGenerated = false;
        if(((Beats._instance.counter % beatInterval) == 0) & isGenerated == false){
            Instantiate(bullet, firePoint.position, Quaternion.identity);
            CurrentBeat = Beats._instance.counter;
            isGenerated = true;
        }
    }

}
