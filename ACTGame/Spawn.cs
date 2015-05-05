using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
    public GameObject prefab;

    public GameObject monsterSpawn() {
        return GameObject.Instantiate(prefab, transform.position, transform.rotation) as GameObject;
    }
}
