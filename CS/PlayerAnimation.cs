using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    private CharacterController charecterController;

    // Use this for initialization
    void Start() {
        charecterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        if (charecterController.isGrounded == false) {
            //play falling animation
            playState("soldierFalling");
        }
        else {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f) {
                playState("soldierWalk");
            }
            else {
                playState("soldierIdle");
            }
        }
    }

    void playState(string animName) {
        //CrossFade 会有0.2秒的缓冲渐变过去
        animation.CrossFade(animName, 0.2f);
        //play 先停止当前动画，直接播放animName
        //animation.Play(animName);
    }
}
