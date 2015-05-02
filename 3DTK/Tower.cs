using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
    public Transform target;
    public GameObject turretObject;
    public GameObject baseObject;
    public Transform thisT;
    public float attackRange = 10f;
    public float rotateSpeed = 2f;
    public GameObject shootPrefb;
    public Transform shootPos;

    public int scoreCast = 50;
    private Transform turretT;
    private int count = 0;

    void Awake() {
        thisT = this.transform;
        if (turretObject != null) {
            turretT = baseObject.transform;
        }
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        ScanForTarget();
        if (target == null) {
            return;
        }

        if (turretT != null) {
            //获取敌人坐标
            Vector3 targetPosit = target.position;
            targetPosit.y = turretT.position.y;
            //旋转
            Quaternion enemyRoation = Quaternion.LookRotation(targetPosit - turretT.position);
            turretT.rotation = Quaternion.Slerp(turretT.rotation, enemyRoation, rotateSpeed * Time.deltaTime);
        }

        if (shootPrefb == null) {
            return;
        }

        count++;
        if (count % 60 == 0) {
            GameObject go = Instantiate(shootPrefb, shootPos.position, shootPos.rotation) as GameObject;
            ShootObject so = go.GetComponent<ShootObject>();
            so.Shoot(target);
        }
    }

    void ScanForTarget() {
        if (target == null) {
            Collider[] cols = Physics.OverlapSphere(thisT.position, attackRange);
            if (cols.Length == 0) {
                return;
            }
            foreach (Collider col in cols) {
                if (col.gameObject.tag == "Enamy") {
                    GameObject go = col.gameObject;
                    Enemy e = go.GetComponent<Enemy>();
                    target = e.transform;
                }
            }
        }
        else {
            float dis = Vector3.Distance(target.position, thisT.position);
            if (dis > attackRange) {
                target = null;
            }
        }
    }
}
