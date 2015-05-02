using UnityEngine;
using System.Collections;

public class SpawnManear : MonoBehaviour {
    public Wave[] waves;//所有wave数据

    public static int remainEnamy = 0;//剩余敌人个数
    private Wave crtWave;//当前生成wave
    private int waveIndex = 0;//wave的索引

    private bool canSpawn = false;
    private int count = 0;

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
            if (waveIndex < waves.Length - 1) {//还有下一波
                waveIndex++;
                count = 0;
                crtWave = waves[waveIndex];
                //开始等待下一波的生成
                StartCoroutine(WaitForNext(crtWave.waitTime));
            }
            else {
                canSpawn = false;
                print("敌人全部出完");
            }
        }
    }

    //等待下一波生成
    IEnumerator WaitForNext(float waitTime) {
        canSpawn = false;
        GameController.waitingForNextWave = true;
        yield return new WaitForSeconds(waitTime);
        GameController.waitingForNextWave = false;
        canSpawn = true;
    }

    //直接生成下一波
    public void StartNextWave() {
        GameController.waitingForNextWave = false;
        StopCoroutine(WaitForNext(crtWave.waitTime));
        canSpawn = true;
    }

    public void StartSpawn() {
        crtWave = waves[waveIndex];
        canSpawn = true;
    }



    public void enamyDir() {
        remainEnamy--;
    }

    public bool allEnemyDie() {
        return false;//ancount == numb && remainEnamy == 0;
    }
}
