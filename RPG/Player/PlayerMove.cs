using UnityEngine;
using System.Collections;

public enum PlayerState {
    Moving,
    Idle
}

public class PlayerMove : MonoBehaviour {
    public float speed = 10f;
    public bool isMoving = false;
    public PlayerState state = PlayerState.Idle;
    private PlayerDir dir;
    private CharacterController controller;

    // Use this for initialization
     void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        dir = gameObject.GetComponent<PlayerDir>();
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(dir.targetPosint, transform.position);
        if (distance > 0.3f) {
            isMoving = true;
            state = PlayerState.Moving;
            controller.SimpleMove(transform.forward * speed);
        }
        else {
            isMoving = false;
            state = PlayerState.Idle;
        }
    }
}
