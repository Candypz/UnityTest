using UnityEngine;
using System.Collections;

/*
 * 攻击特效的自动删除
 */

public class AutoDestroy : MonoBehaviour {

    // Use this for initialization
    void Start() {
        Destroy(this.gameObject, 1);
    }
}
