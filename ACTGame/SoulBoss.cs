using UnityEngine;
using System.Collections;

public class SoulBoss : MonoBehaviour {
    private Transform player;
    private CharacterController cc;
    private Animator animator;
    public float attackDistance = 1.5f;//攻击距离也是寻路的目标距离
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
        if (playerAtkAndDamage.hp <= 0) {//主角死亡
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
                int numb = Random.Range(0, 2);
                if (numb == 0) {
                    animator.SetTrigger("Attack1");
                }
                else {
                    animator.SetTrigger("Attack2");
                }
                attackTImer = 0;
            }
            else {
                animator.SetBool("Walk", false);
            }
        }
        else {//进行跟踪，跑向跟踪
            attackTImer = attackTime;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BossRun01")) {
                cc.SimpleMove(transform.forward * speed);
            }
            animator.SetBool("Walk", true);
        }
    }
}
