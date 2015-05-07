using UnityEngine;
using System.Collections;

//攻击接口

public class ATKAndDamage : MonoBehaviour {
    public float hp = 100;
    public float normalAttack = 50;
    public float attackDistance = 1;
    public AudioClip deadAudio;//boss死亡声音
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
            AudioSource.PlayClipAtPoint(deadAudio, transform.position, 0.2f);
            if (this.tag == "SoulBoss" || this.tag == "SoulMonster") {
                SpawnManager.m_instance.enemyList.Remove(this.gameObject);
                SpawnAward();
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

    //生成奖励物品
    void SpawnAward() {
        int count = Random.Range(1, 3);
        for (int i = 0; i < count; ++i) {
            int index = Random.Range(0, 2);
            if (index == 0) {
                GameObject.Instantiate(Resources.Load("Item_DualSword"), transform.position + Vector3.up, Quaternion.identity);
            }
            else if(index==1) {
                GameObject.Instantiate(Resources.Load("Item_Gun"), transform.position + Vector3.up, Quaternion.identity);
            }
        }
    }

}
