using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerATKAndDamage : ATKAndDamage {
    public float attackB = 70;
    public float attackRange = 100;
    public float attackGun = 40;

    public void AttackA() {
        GameObject enemy = null;
        float distance = attackDistance;
        foreach (GameObject go in SpawnManager.m_instance.enemyList) {
            float temp = Vector3.Distance(go.transform.position, transform.position);
            if (temp < distance) {
                enemy = go;//里主角最近的
                distance = temp;//最小距离
            }
        }
        if (enemy == null) {

        }
        else {
            Vector3 targetPos = enemy.transform.position;
            targetPos.y = this.transform.position.y;
            this.transform.LookAt(targetPos);
            enemy.GetComponent<ATKAndDamage>().TakeDamage(normalAttack);//伤害
        }
    }

    public void AttackB() {
        GameObject enemy = null;
        float distance = attackDistance;
        foreach (GameObject go in SpawnManager.m_instance.enemyList) {
            float temp = Vector3.Distance(go.transform.position, transform.position);
            if (temp < distance) {
                enemy = go;//里主角最近的
                distance = temp;//最小距离
            }
        }
        if (enemy == null) {

        }
        else {
            Vector3 targetPos = enemy.transform.position;
            targetPos.y = this.transform.position.y;
            this.transform.LookAt(targetPos);
            enemy.GetComponent<ATKAndDamage>().TakeDamage(attackB);//伤害
        }
    }

    public void AttackRange() {
        List<GameObject> enamyList = new List<GameObject>();
        float distance = attackDistance;
        foreach (GameObject go in SpawnManager.m_instance.enemyList) {
            float temp = Vector3.Distance(go.transform.position, transform.position);
            if (temp < distance) {
                enamyList.Add(go);
                //go.GetComponent<ATKAndDamage>().TakeDamage(attackRange);
            }
        }
        foreach (GameObject go in enamyList) {
            go.GetComponent<ATKAndDamage>().TakeDamage(attackRange);
        }
    }

    public void AttackGun() {

    }
}
