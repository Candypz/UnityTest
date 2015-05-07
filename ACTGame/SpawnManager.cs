using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 怪物boss生成器
 */

public class SpawnManager : MonoBehaviour {
    public Spawn[] monsterSpawn;
    public Spawn[] bossSpawn;
    public List<GameObject> enemyList = new List<GameObject>();
    public AudioClip gameVictory;

    public static SpawnManager m_instance;

    void Awake() {
        m_instance = this;
    }

    // Use this for initialization
    void Start() {
        StartCoroutine(enemySpawn());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator enemySpawn() {
        //第一波敌人的生成
        foreach (Spawn s in monsterSpawn) {
            enemyList.Add(s.monsterSpawn());
        }

        while (enemyList.Count > 0) {
            yield return new WaitForSeconds(0.2f);
        }

        //第二波敌人的产生
        foreach (Spawn s in monsterSpawn) {
            enemyList.Add(s.monsterSpawn());
        }
        yield return new WaitForSeconds(1f);
        foreach (Spawn s in monsterSpawn) {
            enemyList.Add(s.monsterSpawn());
        }

        while (enemyList.Count > 0) {
            yield return new WaitForSeconds(0.2f);
        }

        //第三波敌人的产生
        foreach (Spawn s in monsterSpawn) {
            enemyList.Add(s.monsterSpawn());
        }
        yield return new WaitForSeconds(1f);
        foreach (Spawn s in monsterSpawn) {
            enemyList.Add(s.monsterSpawn());
        }
        yield return new WaitForSeconds(1f);
        foreach (Spawn b in bossSpawn) {
            enemyList.Add(b.monsterSpawn());
        }

        while (enemyList.Count > 0) {
            yield return new WaitForSeconds(0.2f);
        }
        //游戏胜利
        AudioSource.PlayClipAtPoint(gameVictory, transform.position, 0.5f);

    }
}
