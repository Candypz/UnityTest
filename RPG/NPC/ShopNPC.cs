using UnityEngine;
using System.Collections;

public class ShopNPC : NPC {

    public void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            audio.Play();
            Shop.m_instance.TransformState();
        }
    }
}


