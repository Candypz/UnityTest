using UnityEngine;
using System.Collections;

public class MonstterMove : MonoBehaviour {
    private NavMeshAgent monster;
    public Transform target;

    // Use this for initialization
    void Start() {
        monster = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        monsterMove();
    }

    void monsterAnimation(string animName) {
        animation.CrossFade(animName, 0.2f);
    }

    void monsterMove() {
        monster.SetDestination(target.position);
        monsterAnimation("ghost_block_start");
    }

    void OnCollisionEnter(Collision coll) {
        Debug.Log("碰撞");
        if (coll.transform.name == gameObject.transform.name) {
            Destroy(gameObject);
        }
    }
}
