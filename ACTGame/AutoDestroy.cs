﻿using UnityEngine;
using System.Collections;

//特效删除

public class AutoDestroy : MonoBehaviour {

    // Use this for initialization
    void Start() {
        Destroy(this.gameObject, 1);
    }

    // Update is called once per frame
    void Update() {

    }
}
