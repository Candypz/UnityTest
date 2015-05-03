using UnityEngine;
using System.Collections;

public class MonsterPlant : MonoBehaviour {
    public GameObject monsters;

    // Use this for initialization
    void Start() {
        InvokeRepeating("createMonster", 2, 2);
    }

    // Update is called once per frame
    void Update() {

    }

    void createMonster() {
        GameObject go = GameObject.Instantiate(monsters, new Vector3(42.52f, 0.96f, 7.45f), Quaternion.identity)as GameObject;
    }
}
