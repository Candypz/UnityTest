using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
    public GameObject towerPrefab;
    private GameObject go;
    private Transform thisT;

    // Use this for initialization
    void Awake() {
        thisT = this.transform;
    }

    // Update is called once per frame
    void OnMouseDown() {
        if (go == null&&GameController.score>=50) {
            go = Instantiate(towerPrefab, this.transform.position, towerPrefab.transform.rotation) as GameObject;
            GameController.score -= 50;
        }
    }
}
