using UnityEngine;
using System.Collections;

public class CustonManager : MonoBehaviour {
    public static CustonManager m_instance;
    public Texture2D cursorAttack;
    public Texture2D cursorLockTarget;
    public Texture2D cursorNormal;
    public Texture2D cursorPick;
    public Texture2D cursorNpcTalk;

    private Vector2 hotspot = Vector2.zero;
    private CursorMode mode = CursorMode.Auto;//鼠标指针的类型

    void Start() {
        m_instance = this;
    }

    public void SetCursorNormal() {
        Cursor.SetCursor(cursorNormal, hotspot, mode);
    }

    public void SetCursorNpcTalk() {
        Cursor.SetCursor(cursorNpcTalk, hotspot, mode);
    }

}
