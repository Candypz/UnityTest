using UnityEngine;
using System.Collections;

public class ShootObject : MonoBehaviour {
    public float rotateSpeed = 5f;
    public int danger = 50;
    public float moveSpeed = 20f;
    private Vector3 targetPoint;

    public void Shoot(Transform e) {
        targetPoint = e.position;

        targetPoint.x += Random.Range(-0.5f, 0.5f);
        targetPoint.y += Random.Range(-0.5f, 0.5f);
        targetPoint.z += Random.Range(0f, 2f);
    }

	void Update () {
        if (targetPoint != null) {
            bool reached = MoveToPoint(targetPoint);
            if (reached) {
                Destroy(this.gameObject);
            }
        }
	}

    bool MoveToPoint(Vector3 point) {
        float dis = Vector3.Distance(this.transform.position, point);
        if (dis < 0.15f) {
            return true;
        }

        Quaternion mouseRoation = Quaternion.LookRotation(point - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, mouseRoation, rotateSpeed * Time.deltaTime);

        Vector3 dir = (point - this.transform.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);

        return false;
    }
}
