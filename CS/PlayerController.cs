using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public Camera camera;
    public CSBulletSpwan bulletSpwan;
    public NetworkPlayer ownerPlayer;

    private CharacterMotor motor;
    private PlayerAnimation playerAnimation;


    // Use this for initialization
    void Start() {
        motor = this.GetComponent<CharacterMotor>();
        playerAnimation = this.GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void loseControl() {
        motor = this.GetComponent<CharacterMotor>();
        playerAnimation = this.GetComponent<PlayerAnimation>();

        //禁用camera
        camera.gameObject.SetActive(false);
        //禁用移动控制
        motor.canControl = false;
        //禁用掉动画控制
        playerAnimation.enabled = false;
        //禁用设计
        bulletSpwan.enabled = false;
    }

    [RPC]
    public void setOwnerPlayer(NetworkPlayer player) {
        this.ownerPlayer = player;

        if (Network.player != player) {
            loseControl();
        }
    }
}
