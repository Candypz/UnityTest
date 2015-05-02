using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour {

    public Material[] mats;
    public float flashTime = 0.05f;//闪光时间
    private float flashTimer = 0;
    private int index = 0;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetMouseButtonDown(0)) {
        //    flash();
        //}

        if (renderer.enabled) {
            flashTimer += Time.deltaTime;
            if (flashTimer > flashTime) {
                renderer.enabled = false;
            }
        }
    }

    public void flash() {
        index++;
        index %= 4;
        //renderer.enabled物体是否隐藏
        renderer.enabled = true;
        renderer.material = mats[index];
        flashTimer = 0;
    }
}
