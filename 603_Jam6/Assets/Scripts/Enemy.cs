using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int lastMatchedBeat;
    private Vector3 speed;
    private bool freeze;
    private WayPoints wayPoints;
    private int waypointIndex = 0;
    private Grid grid;
    [SerializeField] private Animator anim;
    [SerializeField]private GameObject modelHandler;
    void Start()
    {
        wayPoints = GameObject.FindGameObjectWithTag("WayPoints").GetComponent<WayPoints>();
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        freeze = false;
    }


    void Update()
    {
        if (!freeze&&Beats._instance.inBeat && lastMatchedBeat != Beats._instance.counter)
        {
            if (isArrived(wayPoints.wayPoints[waypointIndex].position))
            {
                if (waypointIndex < wayPoints.wayPoints.Count - 1)
                {
                    waypointIndex++;
                }
                else Destroy(gameObject);
            }
            MoveTowardsTarget(wayPoints.wayPoints[waypointIndex].position);
            lastMatchedBeat = Beats._instance.counter;
        }

    }

    //helper 
    private bool isArrived(Vector3 TargetPos) {
        Vector3Int currentPosition = grid.WorldToCell(transform.position);
        Vector3Int targetPosition = grid.WorldToCell(TargetPos);
        if (currentPosition.x == targetPosition.x && currentPosition.y == targetPosition.y) {
            return true;
        }
        return false;
    }

    private void MoveTowardsTarget(Vector3 TargetPos)
    {
        // we did not have path finding in both x and y changes at the same time. make sure each way point only move on one axis
        Vector3Int currentCell = grid.WorldToCell(transform.position);
        Vector3Int targetCell = grid.WorldToCell(TargetPos);
        int xDiff = 0;
        int yDiff = 0;
        if (currentCell.x < targetCell.x)
        {
            // then it is right, next go right
            xDiff += 1;
            //assign move direction
            modelHandler.transform.right = transform.right;
            modelHandler.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (currentCell.x > targetCell.x)
        {
            // then it is left, next go left
            xDiff -= 1;
            modelHandler.transform.right = transform.right;
            modelHandler.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (currentCell.y < targetCell.y)
        {
            // then it is up, next go up
            yDiff += 1;
            modelHandler.transform.right = transform.up;
            modelHandler.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (currentCell.y > targetCell.y)
        {
            // then it is left, next go left
            yDiff -= 1;
            modelHandler.transform.right = transform.up;
            modelHandler.transform.localScale = new Vector3(-1, 1, 1);
        }
        moveTo(xDiff, yDiff);
    }

    private void moveTo(int xDiff, int yDiff)
    {
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        cellPosition += new Vector3Int(xDiff, yDiff, 0);
        var newPos = grid.GetCellCenterWorld(cellPosition);
        StartCoroutine(movement(newPos));
    }

    IEnumerator movement(Vector3 target)
    {
        freeze = true;
        anim.Play("bugJumpping", -1, 0);
        int counter = 0;
        while (counter < 5)
        {
            var x = Mathf.SmoothDamp(transform.position.x, target.x, ref speed.x, 0.03f);
            var y = Mathf.SmoothDamp(transform.position.y, target.y, ref speed.y, 0.03f);
            transform.position = new Vector3(x, y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
            counter++;
        }
        freeze = false;
        yield return null;
    }



}