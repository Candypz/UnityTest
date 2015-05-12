using UnityEngine;
using System.Collections;


/*
 *  相机跟随主角移动和转动
 */

public class FollowPlayer : MonoBehaviour {
    public bool isMouseDown = false;
    public float rotateSpeed = 1f;
    public float moveSpeed = 2f;
    private Transform player;
    private Camera mainCamer;
    private Vector3 offsetPosition;//位置偏移
    float min = 37f;
    float max = 72f;

    // Use this for initialization
    void Start() {
        mainCamer = gameObject.GetComponent<Camera>();
        player = GameObject.Find("Player").transform;
        offsetPosition = transform.position - player.position;
    }

    // Update is called once per frame
    void Update() {
        FollowPlayerMove();

        FoollowRotation();
        ChangeZoom();
    }

    void FollowPlayerMove() {
        //跟随主角的位置移动
        //Vector3 targetPos = player.position + new Vector3(0, 13f, 20f);
        //transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        //跟随主角的转动
        //Quaternion targetRotain = Quaternion.LookRotation(player.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotain, moveSpeed * Time.deltaTime);
        transform.position = offsetPosition + player.position;
    }

    //控制镜头的缩放
    void ChangeZoom() {
        //获取鼠标滚轮，滚动的值
        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScrollWheel != 0) {
            //Debug.Log("MouseButton");
            //camera的视角缩放加上鼠标滚轮滚动的值
            if (mainCamer.fieldOfView < min) {
                mainCamer.fieldOfView = min;
            }
            if (mainCamer.fieldOfView > max) {
                mainCamer.fieldOfView = max;
            }

            if (mainCamer.fieldOfView >= min && mainCamer.fieldOfView <= max) {
                if (mouseScrollWheel > 0) {
                    mainCamer.fieldOfView += (mouseScrollWheel + 2f);
                }
                else {
                    mainCamer.fieldOfView -= (mouseScrollWheel + 2f);
                }
            }
        }
    }

    //镜头根据鼠标的旋转
    void FoollowRotation() {
        //Input.GetAxis("Mouse X");//鼠标在水平方向的滑动
        //Input.GetAxis("Mouse Y");//鼠标在垂直方向的滑动
        if (Input.GetMouseButtonDown(1)) {
            isMouseDown = true;
        }
        if (Input.GetMouseButtonUp(1)) {
            isMouseDown = false;
        }

        if (isMouseDown) {//主角位置，旋转朝向，旋转速度
            transform.RotateAround(player.position, player.up, rotateSpeed * Input.GetAxis("Mouse X"));

            Vector3 originalPos = transform.position;
            Quaternion origianlRotation = transform.rotation;

            transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));

            float x = transform.eulerAngles.x;
            if (x < 10 || x > 60) {//超出范围后，将属性还原
                transform.position = originalPos;
                transform.rotation = origianlRotation;
            }
        }
        offsetPosition = transform.position - player.position;
    }
}
