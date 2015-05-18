using UnityEngine;
using System.Collections;

public class PlayerDir : MonoBehaviour {
    public GameObject effectPrefab;//点击特效
    public Vector3 targetPoint = Vector3.zero;//点击的位置传给playermove;
    private PlayerMove playerMove;
    private bool isMoving = false;

    void Start() {
        playerMove = this.gameObject.GetComponent<PlayerMove>();
        targetPoint = transform.position;
    }

    void Update() {
        PlayerMoving();
    }

    void ShowClickEffect(Vector3 point) {
        point = new Vector3(point.x, point.y + 0.2f, point.z);
        GameObject.Instantiate(effectPrefab, point, Quaternion.identity);
    }

    void LookAtTarget(Vector3 point) {
        targetPoint = point;
        targetPoint = new Vector3(targetPoint.x, transform.position.y, targetPoint.z);
        transform.LookAt(targetPoint);
    }

    void PlayerMoving() {
        if (Input.GetMouseButtonDown(0)&&!UICamera.isOverUI) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);
            if (isCollider && hit.collider.tag == Tags.ground) {
                isMoving = true;
                ShowClickEffect(hit.point);
                LookAtTarget(hit.point);
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            isMoving = false;
        }
        if (isMoving) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);
            if (isCollider && hit.collider.tag == Tags.ground) {
                LookAtTarget(hit.point);
            }
        }
        else {
            if (playerMove.isMove) {
                LookAtTarget(targetPoint);
            }
        }
    }
}
