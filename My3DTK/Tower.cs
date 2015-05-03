using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
    public Transform target;
    public GameObject baseObject;//底座
    public GameObject towerTopObject;//塔顶
    public float attackRange = 10f;//攻击范围
    public float rotateSpeed = 2f;//转动速度
    public GameObject shootPrefb;//箭
    public Transform shootPoint;//发射点
    private int count = 0;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        ScanForTarget();
        if (target == null) {
            return;
        }

        if (towerTopObject != null) {
            Vector3 targetPosit = target.position;
            targetPosit.y = towerTopObject.transform.position.y;

            Quaternion monsterRoation = Quaternion.LookRotation(targetPosit - towerTopObject.transform.position);
            towerTopObject.transform.rotation = Quaternion.Slerp(towerTopObject.transform.rotation, monsterRoation, rotateSpeed * Time.deltaTime);
        }
        if (shootPrefb==null) {
            return;
        }
        count++;
        if (count % 60 == 0) {
            GameObject go = Instantiate(shootPrefb, shootPoint.position, shootPoint.rotation) as GameObject;
            ShootObject so = go.GetComponent<ShootObject>();
            so.Shoot(target);
        }
    }

    void ScanForTarget() {
        if (target == null) {
            Collider[] cols = Physics.OverlapSphere(this.transform.position, attackRange);
            if (cols.Length == 0) {
                return;
            }
            foreach (Collider col in cols) {
                if (col.gameObject.tag == "Monster") {
                    GameObject go = col.gameObject;
                    Monster monster = go.GetComponent<Monster>();
                    target = monster.transform;
                }
            }
        }
        else {
            float dis = Vector3.Distance(target.transform.position, this.transform.position);
            if (dis > attackRange) {
                target = null;
            }
        }
    }
}
