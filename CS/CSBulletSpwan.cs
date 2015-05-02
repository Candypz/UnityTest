using UnityEngine;
using System.Collections;

public class CSBulletSpwan : MonoBehaviour {

    public int shootRate = 10;//每秒可以射击多少子弹
    public Flash flash;
    public GameObject bulletPrefab;
    public Camera soldierCamera;

    private float timer = 0;
    private bool isFiring = false;


	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            isFiring = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            isFiring = false;
        }

        if (isFiring) {
            timer += Time.deltaTime;
            if (timer > 1f / shootRate) {
                shoot();
                timer -= 1f/shootRate;
            }
        }
	}

    void shoot() {//射击
        flash.flash();
        GameObject go = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation)as GameObject;
        //得到当前视野的一个目标
        Vector3 point = soldierCamera.ScreenToWorldPoint(new Vector3(Screen.width/2,Screen.height/2,0));
        RaycastHit hitinfo;
        bool isCollider = Physics.Raycast(point, soldierCamera.transform.forward,out hitinfo);
        if (isCollider) {
            go.transform.LookAt(hitinfo.point);
        }
        else {
            point += soldierCamera.transform.forward * 1000;
            go.transform.LookAt(point);
        }
    }
}
