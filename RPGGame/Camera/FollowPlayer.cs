using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    private Camera mainCamera;
    private Vector3 offsetPosition;
    private Transform player;
    private float moveSpeed = 3f;
    private float min = 50f;
    private float max = 102f;
    private bool isMouseDown = false;
    private float rotateSpeed = 1f;
    private Vector3 oldTransform;
    private Vector3 targetPos;

    void Start() {
        mainCamera = this.gameObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        offsetPosition = transform.position - player.position;
        //oldTransform = transform.position;

    }

    void Update() {
        FollowPlayerMove();
        ChangeZoom();
        FollowRotation();
    }

    void FollowPlayerMove() {
        //Vector3 targetPos = player.position + new Vector3(-0.1240393f, 4.104241f, -4.997493f);
        //targetPos = player.position + oldTransform;
        //transform.position = Vector3.Lerp(transform.position, targetPos, 3f * Time.deltaTime);
        //
        //Quaternion targetRotain = Quaternion.LookRotation(player.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotain, 3f * Time.deltaTime);

        transform.position = offsetPosition + player.position;
    }

    void ChangeZoom() {
        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScrollWheel != 0) {

            if (mainCamera.fieldOfView < min) {
                mainCamera.fieldOfView = min;
            }
            if (mainCamera.fieldOfView > max) {
                mainCamera.fieldOfView = max;
            }

            if (mainCamera.fieldOfView >= min && mainCamera.fieldOfView <= max) {
                if (mouseScrollWheel > 0) {
                    mainCamera.fieldOfView += (mouseScrollWheel + 2f);
                }
                else {
                    mainCamera.fieldOfView -= (mouseScrollWheel + 2f);
                }
            }
        }
    }

    void FollowRotation() {
        if (Input.GetMouseButtonDown(1)) {
            isMouseDown = true;
        }
        if (Input.GetMouseButtonUp(1)) {
            isMouseDown = false;
        }

        if (isMouseDown) {
            transform.RotateAround(player.position, player.up, rotateSpeed * Input.GetAxis("Mouse X"));
            Vector3 origialPos = transform.position;
            Quaternion origialRotation = transform.rotation;

            transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));

            float x = transform.eulerAngles.x;
            if (x < 10 || x > 60) {
                transform.position = origialPos;
                transform.rotation = origialRotation;
            }
        }
        //targetPos = transform.position - player.position;
        //oldTransform = transform.position-player.position;
        offsetPosition = transform.position - player.position;
    }
}
