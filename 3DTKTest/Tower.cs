using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
    private GameObject[] monster;
    //public GameObject towCollision;

    void Awake() {
        monster = GameObject.FindGameObjectsWithTag("Monster");
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerStay(Collider other) {
        foreach (GameObject go in monster) {
            if (other.collider.name == go.name || other.collider.name == go.name + "(Clone)") {
                Debug.Log(transform.parent.name);
            }
        }
    }
}
