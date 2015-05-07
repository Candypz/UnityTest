using UnityEngine;
using System.Collections;

/*
 * Boss的攻击
 */

public class SoulBossATKAndDamage : ATKAndDamage {
    private Transform player;
    public AudioClip bossAttackAudio;//boss攻击声音

    void Awake() {
        base.Awake();
        player = GameObject.Find("Player").transform;
    }

    public void Attack1() {
        AudioSource.PlayClipAtPoint(bossAttackAudio, transform.position, 0.2f);
        if (Vector3.Distance(transform.position, player.position) < attackDistance) {
            player.GetComponent<ATKAndDamage>().TakeDamage(normalAttack);
        }
    }

    public void Attack2() {
        AudioSource.PlayClipAtPoint(bossAttackAudio, transform.position, 0.2f);
        if (Vector3.Distance(transform.position, player.position) < attackDistance) {
            player.GetComponent<ATKAndDamage>().TakeDamage(normalAttack);
        }
    }
}
