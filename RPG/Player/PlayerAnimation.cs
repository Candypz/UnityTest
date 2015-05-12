using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    private PlayerMove move;

    void Start() {
        move = gameObject.GetComponent<PlayerMove>();
    }

    void LateUpdate() {
        if (move.state == PlayerState.Moving) {
            PlayAnim("Run");
        }
        else if (move.state == PlayerState.Idle) {
            PlayAnim("Idle");
        }
    }

    void PlayAnim(string animName) {
        animation.CrossFade(animName);
    }
}
