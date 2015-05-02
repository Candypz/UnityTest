using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
    public Path path;
    public float moveSpeed = 5f;
    public int HP = 100;
    public int socreVale = 40;
    private Vector3 target;
    private Transform thisT;
    private float rotateSpeed = 5f;
    private IList<Vector3> vector3Points;
    private bool dead = false;

    // Use this for initialization
    void Start() {

    }

    void Awake() {
        thisT = this.transform;
    }

    public void Init() {
        vector3Points = new List<Vector3>();
        for (int i = 0; i < path.wayPoints.Length; ++i) {
            vector3Points.Add(path.wayPoints[i].transform.position);
        }
        GetNextPoint();
    }

    void GetNextPoint() {
        if (vector3Points.Count > 0) {
            target = vector3Points[0];
            vector3Points.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update() {
        if (dead) {
            return;
        }
        bool reached = MoveToPoint(target);
        if (reached) {
            GetNextPoint();
        }

    }

    bool MoveToPoint(Vector3 point) {
        float dis = Vector3.Distance(thisT.position, point);
        if (dis < 0.15f) {
            return true;
        }
        //转向
        Quaternion enemyRoation = Quaternion.LookRotation(point - this.transform.position);
        //thisT.rotation = enemyRoation;
        thisT.rotation = Quaternion.Slerp(thisT.rotation, enemyRoation, rotateSpeed * Time.deltaTime);

        Vector3 dir = (point - thisT.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime,Space.World);

        return false;
    }

    public void OnTriggerEnter(Collider other) {
        //print(other.gameObject.tag);
        if (other.gameObject.tag == "TowerProject") {
            ShootObject so = other.GetComponent<ShootObject>();
            HP -= so.danger;

            if (HP <= 0) {
                Die();
            }
        }
        else if (other.gameObject.tag == "EndPoint") {
            GameController.remainLive--;
            print(GameController.remainLive);

            if (GameController.remainLive == 0) {
                GameController.GameOver();
            }
            StartCoroutine(endCamplate());
        }
    }

    void Die() {
        if (dead) {
            return;
        }

        GameController.score += socreVale;
        dead = true;
        animation.Play("dying");
        StartCoroutine(dieComplete());
    }

    IEnumerator dieComplete() {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);

        GameController.enamyDir();
    }

    IEnumerator endCamplate() {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);

        if (!(GameController.remainLive == 0)) {
            GameController.enamyDir();
        }
    }

}
