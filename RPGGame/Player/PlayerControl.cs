using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public static PlayerControl m_instance;

    public float playerMoveSpeed = 4.2f;


    public bool isDie = false;
    public bool isRun = false;
    public bool isWalk = false;


    void Awake() {
        m_instance = this;
    }

    
}
