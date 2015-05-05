using UnityEngine;
using System.Collections;

public class ATKAndDamage : MonoBehaviour {
    public float hp = 100;
    public float normalAttack = 50;
    public float attackDistance = 1;
    protected Animator animator;

    protected void Awake() {
        animator = this.GetComponent<Animator>();
    }

    public virtual void TakeDamage(float damage) {
        if (hp > 0) {
            hp -= damage;
        }

        if (hp > 0) {
            if (this.tag == "SoulBoss" || this.tag == "SoulMonster") {
                animator.SetTrigger("Damage");
            }
        }
        else {
            animator.SetBool("Dead", true);
            if (this.tag == "SoulBoss" || this.tag == "SoulMonster") {
                SpawnManager.m_instance.enemyList.Remove(this.gameObject);
                Destroy(this.gameObject, 1);
                this.GetComponent<CharacterController>().enabled = false;
            }
        }


        if (this.tag == "SoulBoss") {
            GameObject.Instantiate(Resources.Load("HitBoss"), transform.position + Vector3.up, transform.rotation);
        }
        else if (this.tag == "SoulMonster") {
            GameObject.Instantiate(Resources.Load("HitMonster"), transform.position + Vector3.up, transform.rotation);
        }
    }


}
