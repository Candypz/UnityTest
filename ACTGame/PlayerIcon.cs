using UnityEngine;
using System.Collections;

/*
 * player在MiniMap中的位置
 */

public class PlayerIcon : MonoBehaviour {
    private Transform playerIcon;

    // Use this for initialization
    void Start() {
        playerIcon = MiniMap.m_instance.GetPlayerIcon();
    }

    // Update is called once per frame
    void Update() {
        float y = transform.eulerAngles.y;
        playerIcon.localEulerAngles = new Vector3(0, 0, -y);
    }
}
