using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {
    public GameObject enmayPrefab;
    public int ancount = 10;
    public int delay = 2;//间隔时间

    public float waitTime = 5;//本波等待时间
    
    public Transform spawnPoint;
    public Path path;
    public int numb = 0;//生成敌人的总数

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //生成敌人
    public void SpawnEnay() {
        numb++;
        GameObject go = Instantiate(enmayPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        Enemy e = go.GetComponent<Enemy>();
        e.path = path;
        e.Init();
    }

    //是否还可以生成敌人
    public bool CanSpawn() {
        return numb < ancount;
    }
}
