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
    // Start is called before the first frame update
    void Start()
    {
        freeze = false;
        anim = modelHandler.GetComponent<Animator>();
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
                moveTo(-1, 0);
                // Vector3 target = new Vector3(transform.position.x + -1 * 1.145f, transform.position.y, transform.position.z);
                // StartCoroutine(movement(target));
            }

            if (Input.GetButtonDown("Right") && Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
            {
                // ok for move Horizontal
                lastMatchedBeat = Beats._instance.counter;
                // Vector3 target = new Vector3(transform.position.x + 1 * 1.145f, transform.position.y, transform.position.z);
                // StartCoroutine(movement(target));
                moveTo(1, 0);
            }

            if (Input.GetButtonDown("Up") && Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
            {
                // ok for move Vertical
                lastMatchedBeat = Beats._instance.counter;
                // Vector3 target = new Vector3(transform.position.x, transform.position.y + 1 * 0.9f, transform.position.z);
                // StartCoroutine(movement(target));
                moveTo(0, 1);
            }

            if (Input.GetButtonDown("Down") && Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
            {
                // ok for move Vertical
                lastMatchedBeat = Beats._instance.counter;
                moveTo(0, -1);
                // Vector3 target = new Vector3(transform.position.x, transform.position.y + -1 * 0.9f, transform.position.z);

            }
        }

    }
    private void moveTo(int xDiff, int yDiff){
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        print("current position in grid: " + cellPosition);
        cellPosition += new Vector3Int(xDiff, yDiff, 0);
        var newPos = grid.GetCellCenterWorld(cellPosition);
        print("old pos: " + transform.position);
        print("new pos: " + newPos);
        StartCoroutine(movement(newPos));
    }
    private void OnDrawGizmos() {
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        var newPos = grid.GetCellCenterWorld(cellPosition);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(newPos, 0.2f);
    }

    IEnumerator movement(Vector3 target) {
        freeze = true;
        anim.Play("Interacting", -1, 0);
        int counter = 0;
        while (counter < 5) {
            var x  = Mathf.SmoothDamp(transform.position.x,target.x,ref speed.x, 0.03f);
            var y = Mathf.SmoothDamp(transform.position.y, target.y, ref speed.y, 0.03f);
            transform.position = new Vector3(x, y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            counter++;
        }
        freeze = false;
        yield return null;
    }
}
