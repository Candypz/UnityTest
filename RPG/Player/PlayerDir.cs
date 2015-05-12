using UnityEngine;
using System.Collections;

public class PlayerDir : MonoBehaviour {
    public GameObject effect_prefab;
    private bool isMoving = false;
    private PlayerMove playerMove;
    public Vector3 targetPosint = Vector3.zero;//点击的目标位置

    // Use this for initialization
    void Start() {
        targetPosint = transform.position;
        playerMove = gameObject.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update() {//UICamera.hoveredObject是否有NGUI控件
        if (Input.GetMouseButtonDown(0) && UICamera.hoveredObject == null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);
            if (isCollider && hit.collider.tag == Tags.ground) {
                isMoving = true;
                //实例化点击的效果
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
            if (playerMove.isMoving) {
                LookAtTarget(targetPosint);
            }
        }
    }

    void ShowClickEffect(Vector3 point) {
        point = new Vector3(point.x, point.y + 0.1f, point.z);
        GameObject.Instantiate(effect_prefab, point, Quaternion.identity);
    }

    //让主角朝向目标位置
    void LookAtTarget(Vector3 point) {
        targetPosint = point;
        targetPosint = new Vector3(targetPosint.x, transform.position.y, targetPosint.z);
        this.transform.LookAt(targetPosint);
    }
}
