using UnityEngine;
using System.Collections;

public class WeaponNpc : NPC {

    public void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            audio.Play();
            WeaponShop.m_instance.TransformState();
        }
    }
}
