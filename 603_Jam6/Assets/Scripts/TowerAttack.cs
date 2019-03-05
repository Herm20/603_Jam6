using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour {

    public GameObject bullet;

    //public Transform firePoint;

    private bool isGenerated = false;

    private float CurrentBeat;

    public float beatInterval = 3;

    private int counter;

    public bool active;
    void Start(){
        
        //Instantiate(bullet, transform.position, Quaternion.identity);
        CurrentBeat = 0;
        active = false;
        counter = 0;
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.4f);
    }

    void Update(){
        if (active)
        {
            if (CurrentBeat != Beats._instance.counter) isGenerated = false;
            if(((Beats._instance.counter % beatInterval) == 0) & isGenerated == false){
                Instantiate(bullet, transform.position, Quaternion.identity);
                CurrentBeat = Beats._instance.counter;
                isGenerated = true;
            }
        }
    }

    public void chargeTowerUp()
    {
        counter++;
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.4f + counter * 0.2f);
        if (counter == 2)
        {
            active = true;
        }
    }

}
