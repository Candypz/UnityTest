using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
    public float force_move = 50;
    public float jumpVelocity = 70;
    private Animator anim;
    private bool isGround = false;
    private bool isWall = false;
    private Transform wallTransforms;
    private bool isSilde = false;

    void Awake() {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float h = Input.GetAxis("Horizontal");
        if (isSilde == false) {
            Vector2 velocity = rigidbody2D.velocity;

            if (h > 0.05f) {
                transform.localScale = new Vector3(1, 1, 1);
                rigidbody2D.AddForce(Vector2.right * force_move);
            }
            else if (h < -0.05f) {
                transform.localScale = new Vector3(-1, 1, 1);
                rigidbody2D.AddForce(-Vector2.right * force_move);
            }
            //Vector2 velocity = rigidbody2D.velocity;
            //anim.SetFloat("Horizontal", Mathf.Abs(velocity.x));
            anim.SetFloat("Horizontal", Mathf.Abs(h));

            if (isGround && Input.GetKeyDown(KeyCode.Space)) {
                velocity.y = jumpVelocity;
                rigidbody2D.velocity = velocity;
                if (isWall) {
                    rigidbody2D.gravityScale = 5;
                }
            }

            anim.SetFloat("Vertical", rigidbody2D.velocity.y);
        }
        else {

        }


        if (isWall == false || isGround == true) {
            isSilde = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "Ground") {
            isGround = true;
            rigidbody2D.gravityScale = 15;
        }
        if (coll.collider.tag == "Wall") {
            isWall = true;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.gravityScale = 5;
            wallTransforms = coll.collider.transform;
        }
        anim.SetBool("IsGround", isGround);
        anim.SetBool("IsWall", isWall);
    }

    public void OnCollisionExit2D(Collision2D coll) {
        if (coll.collider.tag == "Ground") {
            isGround = false;
        }
        if (coll.collider.tag == "Wall") {
            isWall = false;
            rigidbody2D.gravityScale = 15;
        }
        anim.SetBool("IsGround", isGround);
        anim.SetBool("IsWall", isWall);
    }

    //更改朝向
    public void changDir() {
        isSilde = true;
        if (wallTransforms.position.x < transform.position.x) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
