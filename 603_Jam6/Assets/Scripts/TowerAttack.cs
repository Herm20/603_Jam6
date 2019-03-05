using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour {

    public GameObject bullet;

    //public Transform firePoint;

    private bool isGenerated = false;

    private float CurrentBeat;

    public float beatInterval = 3;


    void Start(){
        
        //Instantiate(bullet, transform.position, Quaternion.identity);
        CurrentBeat = 0;
        
    }

    void Update(){
        if (CurrentBeat != Beats._instance.counter) isGenerated = false;
        if(((Beats._instance.counter % beatInterval) == 0) & isGenerated == false){
            Instantiate(bullet, transform.position, Quaternion.identity);
            CurrentBeat = Beats._instance.counter;
            isGenerated = true;
        }
    }

}
