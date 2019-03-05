using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacing : MonoBehaviour { 
    void OnMouseDown(){
        if (Input.GetMouseButtonDown(0)){ 
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                Vector3 towerPosition = new Vector3(0, 0, 0);

                if (transform.localScale.x >= transform.localScale.y) {
                    towerPosition.y = transform.position.y + 0.5f;
                    towerPosition.x = worldPoint.x;
                }
                else{
                    towerPosition.y = worldPoint.y;
                    towerPosition.x = transform.position.x;
                }

                GameObject tower = (GameObject)Resources.Load("Prefabs/Towers/FireTower");
                Instantiate(tower, towerPosition, Quaternion.identity);
            }   
        }
    }
}
