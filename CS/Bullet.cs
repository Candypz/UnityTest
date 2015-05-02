using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 800;
    public GameObject[] bulletHoles;


    // Use this for initialization
    void Start() {
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update() {
        Vector3 orgPos = transform.position;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Vector3 direction = transform.position - orgPos;
        float length = (transform.position - orgPos).magnitude;

        RaycastHit hitinfo;
        bool isCollier = Physics.Raycast(orgPos, direction, out hitinfo, length);
        if (isCollier) {
            //射线检测
            int index = Random.Range(0, 2);
            GameObject bulletHolePrefab = bulletHoles[index];
            Vector3 pos = hitinfo.point;//得到碰撞的位置
            GameObject go = GameObject.Instantiate(bulletHolePrefab, pos, Quaternion.identity) as GameObject;
            go.transform.LookAt(hitinfo.point - hitinfo.normal);
            go.transform.Translate(Vector3.back * 0.01f);
            //hitinfo.normal;可以得到碰撞点的垂线向量 法线
        }
    }
}
