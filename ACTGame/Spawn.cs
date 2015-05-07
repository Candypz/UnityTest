using UnityEngine;
using System.Collections;

//容器

public class Spawn : MonoBehaviour {
    public GameObject prefab;

    public GameObject monsterSpawn() {
        return GameObject.Instantiate(prefab, transform.position, transform.rotation) as GameObject;
    }
}
