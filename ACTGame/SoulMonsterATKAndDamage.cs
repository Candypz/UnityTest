using UnityEngine;
using System.Collections;

//怪物攻击

public class SoulMonsterATKAndDamage : ATKAndDamage {
    private Transform player;

    void Awake() {
        base.Awake();
        player = GameObject.Find("Player").transform;
    }

    public void MonAttack() {
        if (Vector3.Distance(transform.position, player.position) < attackDistance) {
            player.GetComponent<ATKAndDamage>().TakeDamage(normalAttack);
        }
    }
}
