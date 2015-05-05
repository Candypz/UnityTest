using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 10;
    public float attack = 100;

    // Use this for initialization
    void Start() {
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "SoulBoss" || other.tag == "SoulMonster") {
            other.GetComponent<ATKAndDamage>().TakeDamage(attack);
        }
    }


}
