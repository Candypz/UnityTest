using UnityEngine;
using System.Collections;

public class Eyes : MonoBehaviour {
    private Transform player;
    public float attackDirstance = 15;
    public float speed = 5;
    private Animator anim;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float dirstance = Vector3.Distance(player.position, transform.position);

        if (dirstance < attackDirstance) {
            anim.SetBool("Run", true);
            if (player.position.x < transform.position.x) {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else {
                transform.localScale = new Vector3(1, 1, 1);
            }
            Vector3 dir = player.position - transform.position;
            transform.position = dir.normalized * speed * Time.deltaTime + transform.position;
        }
        else {
            anim.SetBool("Run", false);
        }
    }
}
