using UnityEngine;
using System.Collections;

public class PressAnyKeyToStart : MonoBehaviour {
    private bool isAnyKeyDown = false;//是否有任何按钮按下
    public GameObject buttonContainer;

    void Start() {

    }

    void Update() {
        if (isAnyKeyDown == false) {
            if (Input.anyKey) {
                ShowButton();
            }
        }
    }

    //显示开始按钮
    void ShowButton() {
        buttonContainer.SetActive(true);
        this.gameObject.SetActive(false);
        isAnyKeyDown = true;
    }
}
