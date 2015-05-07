using UnityEngine;
using System.Collections;

public class EnmeyIcon : MonoBehaviour {
    private Transform icon;
    private Transform player;
    //Vector3 iconPos = new Vector3();

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player").transform;
        if (this.tag == "SoulMonster") {
            icon = MiniMap.m_instance.GetMonsterIcon().transform;
        }
        else if (this.tag == "SoulBoss") {
            icon = MiniMap.m_instance.GetBossIcon().transform;
        }
    }

    // Update is called once per frame
    void Update() {
        Vector3 offset = transform.position - player.position;
        offset *= 4;
        icon.localPosition = new Vector3(offset.x, offset.z, 0);
    }

    void OnDestroy() {
        if (icon != null) {
            Destroy(icon.gameObject);
        }
    }
}
