﻿using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 20;
        style.normal.textColor = new Color(0, 255, 255);
        GUI.Label(new Rect(20, 12, 100, 30), "Life" + GameController.remainLive + "/" + GameController.maxLives, style);

        GUI.Label(new Rect(160, 12, 100, 30), "Score" + GameController.score, style);

        if (!GameController.started && GUI.Button(new Rect(Screen.width - 80, 10, 70, 35), "Spwan")) {
            GameController.spwan();
        }
        if (GameController.waitingForNextWave && GUI.Button(new Rect(Screen.width - 80, 10, 70, 35), "NexWave")) {
            GameController.game.monsterManage.startNexWave();
        }

        if ((GameController.gameOver || GameController.passed)&&GameController.isRestar) {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Restar")) {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}
