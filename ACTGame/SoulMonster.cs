using UnityEngine;
using System.Collections;

//怪物设置

public class SoulMonster : MonoBehaviour {
    private Transform player;
    private CharacterController cc;
    private Animator animator;
    public float attackDistance = 0.7f;//攻击距离也是寻路的目标距离
    public float speed = 2;
    public float attackTime = 3;//攻击时间
    private float attackTImer = 0;
    private PlayerATKAndDamage playerAtkAndDamage;

    void Start() {
        animator = this.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        cc = this.GetComponent<CharacterController>();
        playerAtkAndDamage = player.GetComponent<PlayerATKAndDamage>();
        attackTImer = attackTime;
    }

    // Update is called once per frame
    void Update() {
        if (playerAtkAndDamage.hp <= 0) {
            animator.SetBool("Walk", false);
            return;
        }

        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        float distance = Vector3.Distance(targetPos, transform.position);

        if (distance <= attackDistance) {//在攻击范围内
            attackTImer += Time.deltaTime;
            if (attackTImer > attackTime) {
                animator.SetTrigger("Attack");
                attackTImer = 0;
            }
            else {
                animator.SetBool("Walk", false);
            }
        }
        else {//进行跟踪，跑向跟踪
            attackTImer = attackTime;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("MonRun")) {
                cc.SimpleMove(transform.forward * speed);
            }
            animator.SetBool("Walk", true);
        }
    }
}
