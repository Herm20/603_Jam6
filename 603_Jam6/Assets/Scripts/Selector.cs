using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
    }

    // Update is called once per frame


    public void boundTile()
    {
        // Bound the nearest location;
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        var newPos = grid.GetCellCenterWorld(cellPosition);
        transform.position = newPos;
    }
}
