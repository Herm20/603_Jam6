using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    private Grid grid;
    private List<GameObject> towers;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        towers = new List<GameObject>();
    }

    public bool isAvaliable(Vector3 pos)
    {
        Vector3Int cellPosition = grid.WorldToCell(pos);
        foreach (var tower in towers)
        {
            Vector3Int towerPosition = grid.WorldToCell(tower.transform.position);
            if (towerPosition.x == cellPosition.x && towerPosition.y == cellPosition.y) {
                // if the tower is not completed
                // add here
                return false;
            }
        }
        return true;
    }
    
    public void addTower(GameObject tower)
    {
        towers.Add(tower);
    }
}
