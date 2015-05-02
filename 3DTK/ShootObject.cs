using UnityEngine;
using System.Collections;

public class ShootObject : MonoBehaviour {
    public Transform thisT;
    public float rotateSpeed = 5f;
    public int danger = 50;
    public float moveSpeed = 20f;
    private Vector3 targetPoint;

    public void Shoot(Transform e) {
        targetPoint = e.position;

        targetPoint.x += Random.Range(-1, 1);
        targetPoint.z += Random.Range(-1, 1);
        targetPoint.y += Random.Range(0f, 4f);
    }

    void Awake() {
        thisT = this.transform;
    }

    // Update is called once per frame
    void Update() {
        if (targetPoint != null) {
            bool reached = MoveToPoint(targetPoint);
            if (reached) {
                Destroy(this.gameObject);
            }
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
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);

        return false;
    }
}
