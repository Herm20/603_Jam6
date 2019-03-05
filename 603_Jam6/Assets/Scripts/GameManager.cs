using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private int lastMatchedBeat;
    private EnemySpawner generator;

    public int levelFactor;

    private int arrivedEnemy;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        towers = new List<GameObject>();
        gold = 20;// test
        goldText.text = " " + gold;
        currentTower = TowerType.IceTower;
        generator = GetComponent<EnemySpawner>();
        levelFactor = 1;
        arrivedEnemy = 20;

    }

    private void Update()
    {
        if (Beats._instance.counter == 10 && lastMatchedBeat != Beats._instance.counter)
        {
            generator.startNewWave(5);
            lastMatchedBeat = Beats._instance.counter;
        }else if (Beats._instance.counter == 30 && lastMatchedBeat != Beats._instance.counter)
        {
            generator.startNewWave(7);
            lastMatchedBeat = Beats._instance.counter;
        }else if(Beats._instance.counter == 60 && lastMatchedBeat != Beats._instance.counter)
        {
            generator.startNewWave(10);
            lastMatchedBeat = Beats._instance.counter;
        }else if(Beats._instance.counter == 90 && lastMatchedBeat != Beats._instance.counter)
        {
            generator.startNewWave(15);
            lastMatchedBeat = Beats._instance.counter;
            levelFactor = 2;
        }else if(Beats._instance.counter == 120 && lastMatchedBeat != Beats._instance.counter)
        {
            generator.startNewWave(20);
            lastMatchedBeat = Beats._instance.counter;
        }else if(Beats._instance.counter == 150 && lastMatchedBeat != Beats._instance.counter)
        {
            generator.startNewWave(25);
            lastMatchedBeat = Beats._instance.counter;
        }else if(Beats._instance.counter == 180 && lastMatchedBeat != Beats._instance.counter)
        {
            generator.startNewWave(30);
            lastMatchedBeat = Beats._instance.counter;
            levelFactor = 3;
        }
        else if(Beats._instance.counter == 210 && lastMatchedBeat != Beats._instance.counter)
        {
            generator.startNewWave(40);
            lastMatchedBeat = Beats._instance.counter;
        }
        else if(Beats._instance.counter == 240 && lastMatchedBeat != Beats._instance.counter)
        {
            generator.startNewWave(60);
            lastMatchedBeat = Beats._instance.counter;
        }else if(Beats._instance.counter == 270 && lastMatchedBeat != Beats._instance.counter)
        {
            generator.startNewWave(60);
            lastMatchedBeat = Beats._instance.counter;
        }       
        
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main");
        }
    }

    public bool isAvailable(Vector3 pos)
    {
        Vector3Int cellPosition = grid.WorldToCell(pos);
        foreach (var tower in towers)
        {
            Vector3Int towerPosition = grid.WorldToCell(tower.transform.position);
            if (towerPosition.x == cellPosition.x && towerPosition.y == cellPosition.y) {
                return false;
            }
        }
        return true;
    }
    
    public void revert(Vector3 pos)
    {
        Vector3Int cellPosition = grid.WorldToCell(pos);
        foreach (var tower in towers)
        {
            Vector3Int towerPosition = grid.WorldToCell(tower.transform.position);
            if (towerPosition.x == cellPosition.x && towerPosition.y == cellPosition.y) {
                towers.Remove(tower);
                Destroy(tower);
                addGold(10);
                break;
            }
        }
    }

    public void addArrived()
    {
        arrivedEnemy -= 1;
        if (arrivedEnemy < 0)
        {
            goldText.text = "game over";
            Time.timeScale = 0;
            SceneManager.LoadScene("Credits");
        }       
    }

    public void chargeTower(Vector3 pos)
    {
        Vector3Int cellPosition = grid.WorldToCell(pos);
        foreach (var tower in towers)
        {
            Vector3Int towerPosition = grid.WorldToCell(tower.transform.position);
            if (towerPosition.x == cellPosition.x && towerPosition.y == cellPosition.y) {
                // if the tower is not completed
                // add here
                if (!tower.GetComponent<TowerAttack>().active)
                {
                    tower.GetComponent<TowerAttack>().chargeTowerUp();
                }
                break;
            }
        }
    }
    
    public void addTower(Vector3 pos)
    {
        if (gold < costEachTower)
        {
            return;
        }
        gold -= costEachTower;
        goldText.text = " " + gold;
        print(currentTower);
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
