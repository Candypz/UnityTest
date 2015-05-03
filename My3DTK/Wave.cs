using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {
    public GameObject monsterPrefab;
    public int ancount = 10;
    public int delay = 2;//间隔时间
    public float waitTime = 5;//本波等待时间
    public Transform spwnPoint;//出生点
    public int numb = 0;//生成敌人的总数

    public void SpawnEnay() {
        numb++;
        GameObject go = Instantiate(monsterPrefab, spwnPoint.position, spwnPoint.rotation) as GameObject;
    }

    public bool CanSpawn() {
        return numb < ancount;
    }

}
