using UnityEngine;
using System.Collections;

public class JoyStick : MonoBehaviour {
    private bool m_isPress = false;
    private Transform button;
    public static float h = 0;
    public static float v = 0;

    void Awake() {
        button = transform.Find("Stick");
    }

    void OnPress(bool isPress) {//按下抬起触发
        this.m_isPress = isPress;
        if (isPress == false) {
            button.localPosition = Vector3.zero;
            h = 0;
            v = 0;
        }
    }

    void Update() {
        if (m_isPress) {
            //print(UICamera.lastTouchPosition);
            Vector2 touchPos = UICamera.lastTouchPosition;
            touchPos -= new Vector2(91, 91);
            float distance = Vector2.Distance(Vector2.zero, touchPos);
            if (distance > 73) {
                touchPos = touchPos.normalized * 73;
                button.localPosition = touchPos;
            }
            else {
                button.localPosition = touchPos;
            }

            h = touchPos.x / 73;
            v = touchPos.y / 73;
        }
    }
}
