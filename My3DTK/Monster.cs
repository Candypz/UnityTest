using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
    public int socreVale = 30;
    private NavMeshAgent monster;
    private Transform target;
    public int monsterHP = 200;
    bool isDie = false;

    // Use this for initialization
    void Awake() {
        monster = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.Find("PlayerPoint").transform;
    }

    // Update is called once per frame
    void Update() {
        monsterMove();
    }

    void monsterAnimation(string animName) {
        animation.CrossFade(animName, 0.2f);
    }

    public void monsterMove() {
        if (!isDie) {
            monster.SetDestination(target.position);
            monsterAnimation("dragon_idle");
        }
    }

    IEnumerator dieComplete() {
        yield return new WaitForSeconds(1.3f);
        Destroy(this.gameObject);
        GameController.enamyDir();
    }

    IEnumerator endComplete() {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        if (!(GameController.remainLive == 0)) {
            GameController.enamyDir();
        }
    }

    void isDir() {
        if (isDie) {
            return;
        }
        monsterAnimation("dragon_die");
        GameController.score += socreVale;
        monster.SetDestination(this.transform.position);
        isDie = true;
        StartCoroutine(dieComplete());
    }

    public void OnTriggerEnter(Collider other) {
        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Cannonball") {
            ShootObject so = other.GetComponent<ShootObject>();
            monsterHP -= so.danger;
            if (monsterHP <= 0) {
                isDir();
            }
        }
        if (other.gameObject.tag == "EndPoint") {
            GameController.remainLive--;
            if (GameController.remainLive == 0) {
                GameController.GameOver();
            }
            StartCoroutine(endComplete());
        }
    }
}
