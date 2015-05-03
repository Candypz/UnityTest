using UnityEngine;
using System.Collections;

public class PlantFrom : MonoBehaviour {
    public GameObject towerPrefab;
    private GameObject go;

    // Use this for initialization
    void Start() {

    }

    void OnMouseDown() {
        if (go == null&&GameController.score>=50) {
            go = Instantiate(towerPrefab, this.transform.position+new Vector3(0,2.78f,0), towerPrefab.transform.rotation) as GameObject;
            GameController.score -= 50;
        }
    }
}
