﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TowerType
{
    FireTower,
    IceTower,
    RangedTower
}

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    private Grid grid;
    private List<GameObject> towers;
    [SerializeField] private int costEachTower;
    [SerializeField] private Text goldText;
    private int gold;
    
    [SerializeField] private Sprite[] Images;
    [SerializeField] private Image Image;
    private TowerType currentTower;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        towers = new List<GameObject>();
        gold = 99;// test
        currentTower = TowerType.IceTower;
    }

    public bool isAvailable(Vector3 pos)
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
    
    public void addTower(Vector3 pos)
    {
        if (gold < costEachTower)
        {
            return;
        }
        gold -= costEachTower;
        goldText.text = " " + gold;
        switch (currentTower)
        {
                case TowerType.IceTower:
                    GameObject tower1 = (GameObject)Resources.Load("Prefabs/Towers/BlueTower");
                    var temp1 = Instantiate(tower1, pos, Quaternion.identity);
                    towers.Add(temp1);
                    break;
                case TowerType.FireTower:
                    GameObject tower2 = (GameObject)Resources.Load("Prefabs/Towers/OrangeTower");
                    var temp2 = Instantiate(tower2, pos, Quaternion.identity);
                    towers.Add(temp2);
                    break;
                case TowerType.RangedTower:
                    GameObject tower3 = (GameObject)Resources.Load("Prefabs/Towers/GreenTower");
                    var temp3 = Instantiate(tower3, pos, Quaternion.identity);
                    towers.Add(temp3);
                    break;
        }
        
    }
    
    public void addGold(int amount)
    {
        gold += amount;
        goldText.text = " " + gold;
    }

    public void switchTowerType(bool forward)
    {
        if (forward)
        {
            if ((int) currentTower == 2)
            {
                currentTower = (TowerType)0;
            }
            else
            {
                currentTower = (TowerType) ((int) currentTower + 1);
            }
        }
        else
        {
            if ((int) currentTower == 0)
            {
                currentTower = (TowerType)2;
            }
            else
            {
                currentTower = (TowerType) ((int) currentTower - 1);
            }
        }

        Image.sprite = Images[(int) currentTower];
    }
}
