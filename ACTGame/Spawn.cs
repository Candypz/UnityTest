using UnityEngine;
using System.Collections;

/*
 * 生成怪物的容器
 */

public class Spawn : MonoBehaviour {
    public GameObject prefab;

    public GameObject monsterSpawn() {
        return GameObject.Instantiate(prefab, transform.position, transform.rotation) as GameObject;
    }
}
