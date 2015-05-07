using UnityEngine;
using System.Collections;

//相机跟随角色

public class FollowPlayer : MonoBehaviour {
    public Transform player;
    public float speed = 2;

    // Use this for initialization
//     void Awake() {
//         player = GameObject.FindGameObjectWithTag(Tags.player).transform;
//     }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        //跟随移动
        Vector3 targetPos = player.position + new Vector3(0, 2.39f, -2.07f);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
        //跟随旋转
        Quaternion targetRotain = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotain, speed * Time.deltaTime);
    }
}
