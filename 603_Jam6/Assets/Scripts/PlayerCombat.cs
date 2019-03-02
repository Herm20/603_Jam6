using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private int lastMatchedBeat;
    private Vector3 speed;
    private bool freeze;
    // Start is called before the first frame update
    void Start()
    {
        freeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze) {
            if (Input.GetButtonDown("Attack") && Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter) {
                // ok for attack
                lastMatchedBeat = Beats._instance.counter;
            }

            if (Input.GetButtonDown("Left") && Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
            {
                // ok for move Horizontal
                lastMatchedBeat = Beats._instance.counter;
                Vector3 target = new Vector3(transform.position.x + -1 * 1.145f, transform.position.y, transform.position.z);
                StartCoroutine(movement(target));
            }

            if (Input.GetButtonDown("Right") && Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
            {
                // ok for move Horizontal
                lastMatchedBeat = Beats._instance.counter;
                Vector3 target = new Vector3(transform.position.x + 1 * 1.145f, transform.position.y, transform.position.z);
                StartCoroutine(movement(target));
            }

            if (Input.GetButtonDown("Up") && Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
            {
                // ok for move Vertical
                lastMatchedBeat = Beats._instance.counter;
                Vector3 target = new Vector3(transform.position.x, transform.position.y + 1 * 0.9f, transform.position.z);
                StartCoroutine(movement(target));
            }

            if (Input.GetButtonDown("Down") && Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
            {
                // ok for move Vertical
                lastMatchedBeat = Beats._instance.counter;
                Vector3 target = new Vector3(transform.position.x, transform.position.y + -1 * 0.9f, transform.position.z);
                StartCoroutine(movement(target));
            }
        }


    }

    IEnumerator movement(Vector3 target) {
        freeze = true;
        int counter = 0;
        while (counter < 5) {
            var x  = Mathf.SmoothDamp(transform.position.x,target.x,ref speed.x, 0.05f);
            var y = Mathf.SmoothDamp(transform.position.y, target.y, ref speed.y, 0.05f);
            transform.position = new Vector3(x, y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            counter++;
        }
        freeze = false;
        yield return null;
    }
}
