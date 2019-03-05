using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacing : MonoBehaviour {

    //public string towerPath;
    
    // public Camera camera;
    //public GameObject camera;

    private Grid grid;

    void Start(){
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
    }
    void OnMouseDown(){
        if (Input.GetMouseButtonDown(0)){ 
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(worldPoint);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                Vector3 towerPosition = new Vector3(0, 0, 0);

                if (transform.localScale.x >= transform.localScale.y) {
                    towerPosition.y = transform.position.y + 0.4f;
                    towerPosition.x = worldPoint.x;
                }
                else{
                    towerPosition.y = worldPoint.y + 0.3f;
                    towerPosition.x = transform.position.x;
                }
                
                GameObject tower = (GameObject)Resources.Load("Prefabs/Towers/GreenTower");
                Instantiate(tower, towerPosition, Quaternion.identity);
            }   
        }

    }

}
