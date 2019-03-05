using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private int lastMatchedBeat;
    private Vector3 speed;
    private bool freeze;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject modelHandler;
    private Animator anim;
    private Selector selector;
    // Start is called before the first frame update
    void Start()
    {
        freeze = false;
        anim = modelHandler.GetComponent<Animator>();
        selector = gameObject.GetComponentInChildren<Selector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze) {
            if (Input.GetButtonDown("Build"))
            {
                
                if (Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
                {
                    // ok for build
                    anim.Play("Interacting", -1, 0);
                    if (GameManager._instance.isAvailable(selector.transform.position))
                    {
                        GameManager._instance.addTower(selector.transform.position);
                    }
                    else
                    {
                        GameManager._instance.chargeTower(selector.transform.position);
                    }
                    lastMatchedBeat = Beats._instance.counter;
                }
                else
                {
                    anim.Play("Nope", -1, 0);
                }
            }
            if (Input.GetButtonDown("Revert"))
            {
                // switch the tower
               
                if (Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
                {
                    anim.Play("Interacting", -1, 0);
                    GameManager._instance.revert(selector.transform.position);
                }else
                {
                    anim.Play("Nope", -1, 0);
                }
            }

            
            if (Input.GetButtonDown("Fire2"))
            {
                // switch the tower
              
                if (Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
                {
                    // ok for build
                    anim.Play("Interacting", -1, 0);
                    GameManager._instance.switchTowerType(false);
                    lastMatchedBeat = Beats._instance.counter;
                }else
                {
                    anim.Play("Nope", -1, 0);
                }
            }
            
            if (Input.GetButtonDown("Fire3"))
            {
                // switch the tower
                
                if (Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
                {
                    // ok for build
                    anim.Play("Interacting", -1, 0);
                    GameManager._instance.switchTowerType(true);
                    lastMatchedBeat = Beats._instance.counter;
                }else
                {
                    anim.Play("Nope", -1, 0);
                }
            }
               

            if (Input.GetButtonDown("Left"))
            {
                // ok for move Horizontal
               
                if (Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
                {
                    // ok for attack
                    anim.Play("Interacting", -1, 0);
                    lastMatchedBeat = Beats._instance.counter;
                    moveTo(-1, 0);
                }else
                {
                    anim.Play("Nope", -1, 0);
                }
            }

            if (Input.GetButtonDown("Right"))
            {
               
                if (Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
                {
                    // ok for attack
                    anim.Play("Interacting", -1, 0);
                    lastMatchedBeat = Beats._instance.counter;
                    moveTo(1, 0);
                }else
                {
                    anim.Play("Nope", -1, 0);
                }
            }

            if (Input.GetButtonDown("Up"))
            {
               
                if (Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
                {
                    // ok for attack
                    anim.Play("Interacting", -1, 0);
                    lastMatchedBeat = Beats._instance.counter;
                    moveTo(0, 1);
                }else
                {
                    anim.Play("Nope", -1, 0);
                }
            }
            if (Input.GetButtonDown("Down"))
            {
               
                if (Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
                {
                    // ok for attack
                    anim.Play("Interacting", -1, 0);
                    lastMatchedBeat = Beats._instance.counter;
                    moveTo(0, -1);
                }else
                {
                    anim.Play("Nope", -1, 0);
                }
            }
            
        }

    }
    private void moveTo(int xDiff, int yDiff){
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        cellPosition += new Vector3Int(xDiff, yDiff, 0);
        var newPos = grid.GetCellCenterWorld(cellPosition);
        StartCoroutine(movement(newPos));
    }


    IEnumerator movement(Vector3 target) {
        freeze = true;
        int counter = 0;
        while (counter < 5) {
            var x  = Mathf.SmoothDamp(transform.position.x,target.x,ref speed.x, 0.03f);
            var y = Mathf.SmoothDamp(transform.position.y, target.y, ref speed.y, 0.03f);
            transform.position = new Vector3(x, y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            counter++;
        }
        selector.boundTile();
        freeze = false;
        yield return null;
    }
}
