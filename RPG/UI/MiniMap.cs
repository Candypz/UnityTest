using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

    private Camera miniMapCamera;

    void Awake() {
        miniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
    }
    public void OnPlusButton() {//放大
        if (miniMapCamera.orthographicSize > 3) {
            miniMapCamera.orthographicSize--;
        }
    }

    public void OnMinusButton() {//缩小
        if (miniMapCamera.orthographicSize < 8) {
            miniMapCamera.orthographicSize++;
        }
    }
}
