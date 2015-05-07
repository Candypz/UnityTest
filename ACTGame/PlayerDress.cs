using UnityEngine;
using System.Collections;

public class PlayerDress : MonoBehaviour {
    public SkinnedMeshRenderer headRender;
    public SkinnedMeshRenderer handRender;
    public SkinnedMeshRenderer[] bodyArray;

    void Start() {
        InitDress();
    }

    void InitDress() {
        int headMeshIndex = PlayerPrefs.GetInt("HeadMeshIndex");
        int handMeshIndex = PlayerPrefs.GetInt("HandMeshIndex");
        int colorIndex = PlayerPrefs.GetInt("ColorIndex");

        handRender.sharedMesh = MenuController.m_instance.handMeshArray[handMeshIndex];
        headRender.sharedMesh = MenuController.m_instance.headMeshArray[headMeshIndex];

        foreach (SkinnedMeshRenderer render in bodyArray) {
            render.material.color = MenuController.m_instance.colorArray[colorIndex];
        }

    }
}
