using UnityEngine;
using System.Collections;

public class MonsterManage : MonoBehaviour {
    public Wave[] waves;
    public static int remainEnamy = 0;//剩余敌人的个数
    private Wave crtWave;//当前生成的wave
    private int waveIndex = 0;//wave的索引
    private bool canSpawn = false;//是否可生成
    private int count = 0;


    // Update is called once per frame
    void Update() {
        if (!canSpawn) {
            return;
        }
        count++;
        if (crtWave.CanSpawn() && (count % (60 * crtWave.delay) == 0)) {
            crtWave.SpawnEnay();
            remainEnamy++;
        }

        if (!crtWave.CanSpawn()) {//当前wave的敌人已经全部生成完
            if (waveIndex < waves.Length - 1) {//如果还有下一波
                waveIndex++;
                count = 0;
                crtWave = waves[waveIndex];
                //等待下一波的生成
                StartCoroutine(waitForNext(crtWave.waitTime));
            }
            else {
                canSpawn = false;
                print("敌人全部出完");
            }
        }

    }

    IEnumerator waitForNext(float waitTime) {
        canSpawn = false;
        GameController.waitingForNextWave = true;
        yield return new WaitForSeconds(waitTime);
        GameController.waitingForNextWave = false;
        canSpawn = true;
    }

    public void startSpawn() {
        crtWave = waves[waveIndex];
        canSpawn = true;
    }

    public void startNexWave() {
        GameController.waitingForNextWave = false;
        StopCoroutine(waitForNext(crtWave.waitTime));
        canSpawn = true;
    }

    public void enamyDir() {
        remainEnamy--;
    }

    public bool allEnemyDir() {
        return crtWave.ancount == crtWave.numb && remainEnamy == 0;
    }
}
