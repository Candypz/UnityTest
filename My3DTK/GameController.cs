using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public static GameController game;
    public static int maxLives = 10;
    public static int remainLive = 10;
    public static int score = 200;

    public  MonsterManage monsterManage;

    public static bool gameOver = false;
    public static bool passed = false;

    public static bool started = false;
    public static bool waitingForNextWave = false;

    public static bool isRestar = true;

    public static void enamyDir() {
        game.monsterManage.enamyDir();
        if (game.monsterManage.allEnemyDir()) {
            print("Yes!!");
            passed = true;
        }
    }

    public static void spwan() {
        started = true;
        game.monsterManage.startSpawn();
    }

    public static void GameOver() {
        print("over");
        gameOver = true;
    }

    void Start() {
        game = this;
    }
}
