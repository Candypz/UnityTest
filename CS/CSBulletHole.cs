using UnityEngine;
using System.Collections;

public class CSBulletHole : MonoBehaviour {
    public float speed = 0.5f;
    private float timer = 0;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > 2) {
            renderer.material.color = Color.Lerp(renderer.material.color, Color.clear, speed * Time.deltaTime);
        }
        if (timer > 7) {
            Destroy(this.gameObject);
        }
    }
}
