using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
    private CharacterController cc;
    private Animator animator;
    public float speed = 4;

    void Awake() {
        cc = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
    }

    void Update() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //虚拟杆控制
        if (JoyStick.h != 0 || JoyStick.v != 0) {
            h = JoyStick.h;
            v = JoyStick.v;
        }

        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f) {
            animator.SetBool("Walk", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun")) {
                Vector3 targetDir = new Vector3(h, 0, v);
                transform.LookAt(targetDir + transform.position);
                cc.SimpleMove(transform.forward * speed);
            }
        }
        else {
            animator.SetBool("Walk", false);
        }
    }
}
