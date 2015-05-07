using UnityEngine;
using System.Collections;

//奖励物品

public enum AwardType {
    Gun,
    DualSword
}

public class AwardItem : MonoBehaviour {
    public AwardType type;
    public bool startMove = false;
    public float speed = 8;
    public AudioClip pickup;
    private Transform player;

    void Start() {
        rigidbody.velocity = new Vector3(Random.Range(-5,5),Random.Range(-5,5));//速度
    }

    void Update() {
        if (startMove) {
            transform.position = Vector3.Lerp(transform.position, player.position+Vector3.up, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.position+Vector3.up) < 0.5f) {
                player.GetComponent<PlayerAward>().GetAward(type);
                Destroy(this.gameObject);
                AudioSource.PlayClipAtPoint(pickup, transform.position, 0.3f);
            }
        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Ground") {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            SphereCollider col = this.GetComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 1.5f;
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            startMove = true;
            player = other.transform;
        }
    }





}
