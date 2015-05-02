using UnityEngine;
using System.Collections;

public class Award : MonoBehaviour {
    public float speed = 100;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
	}

    public void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag == "Player") {
            //加分
            AudioMarager.m_instance.PlayCollectible();
            Destroy(gameObject);
        }
    }
}
