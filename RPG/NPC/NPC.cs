using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

    public void OnMouseExit() {
        CustonManager.m_instance.SetCursorNormal();
    }

    public void OnMouseEnter() {
        CustonManager.m_instance.SetCursorNpcTalk();
    }


}
