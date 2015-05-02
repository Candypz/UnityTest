using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public static GameController game;
    public static int maxLives = 10;
    public static int remainLive = 10;
    public static int score = 200;

    public SpawnManear spawnManear;

    public static bool gameOver = false;
    public static bool passed = false;

    public static bool started = false;//游戏是否开始
    public static bool waitingForNextWave = false;//是否正在等待下一波的生成

    public static void enamyDir() {
        game.spawnManear.enamyDir();
        if (game.spawnManear.allEnemyDie()) {
            print("Yes!!");
            passed = true;
        }
    }

    public static void Spwan() {
        started = true;
        game.spawnManear.StartSpawn();
    }

    public static void GameOver() {
        print("over");
        gameOver = true;
    }

    // Use this for initialization
    void Start() {
        game = this;
    }

    // Update is called once per frame
    void Update() {

    }
}
