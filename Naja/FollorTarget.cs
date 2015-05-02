using UnityEngine;
using System.Collections;

public class FollorTarget : MonoBehaviour {
    private GameObject player;

    // Use this for initialization
    void Awake() {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = transform.position;
        pos.x = player.transform.position.x;
        transform.position = pos;
    }
}
