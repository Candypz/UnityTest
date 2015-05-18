using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
    public bool isMove = false;
    private CharacterController charcterController;
    private Animator animator;
    private float h = 0f;
    private float v = 0f;
    private PlayerDir playerDir;

    void Awake() {
        charcterController = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        playerDir = this.GetComponent<PlayerDir>();
    }

    void Update() {
        MouseMove();
        //KeyBoardMove();
    }

    //键盘操作
    void KeyBoardMove() {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) > 0f || Mathf.Abs(v) > 0f) {
            isMove = true;
            Vector3 targetDir = new Vector3(h, 0, v);
            transform.LookAt(targetDir + transform.position);
            charcterController.SimpleMove(transform.forward * PlayerControl.m_instance.playerMoveSpeed);
            animator.SetBool("isRun", true);
        }
        else {
            isMove = false;
            animator.SetBool("isRun", false);
        }
    }

    //鼠标操作
    void MouseMove() {
        float distance = Vector3.Distance(playerDir.targetPoint, transform.position);
        if (distance > 0.3f) {
            isMove = true;
            animator.SetBool("isRun", true);
            charcterController.SimpleMove(transform.forward * PlayerControl.m_instance.playerMoveSpeed);
        }
        else {
            isMove = false;
            animator.SetBool("isRun", false);
        }
    }
}
