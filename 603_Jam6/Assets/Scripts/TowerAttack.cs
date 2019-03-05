using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour {

    private GameObject bullet;

    public Transform firePoint;

    private bool isGenerated = false;

    private float CurrentBeat;

    public float beatInterval = 3;


    void Start(){
        bullet = (GameObject)Resources.Load("Prefabs/Attacks/GreenAttack");
        Instantiate(bullet, firePoint.position, Quaternion.identity);

        CurrentBeat = Beats._instance.counter;
        
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
