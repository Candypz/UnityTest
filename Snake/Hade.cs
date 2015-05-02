using UnityEngine;
using System.Collections;

public class Hade : MonoBehaviour {
    float m_speed;
    float m_angle;
    public GameObject body;

    void Start() {
        //food = false;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Debug.Log(mesh.bounds.size.x);
        Debug.Log(renderer.bounds.size.x);

        m_speed = 1.5f;
        m_angle = 3.1415926f / 2f;
        transform.RotateAround(transform.position, new Vector3(1, 0, 0), 90);
    }

    void Update() {
        if (Input.GetKey(KeyCode.A)) {
            transform.RotateAround(transform.position, transform.forward, m_angle);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.RotateAround(transform.position, transform.forward, -m_angle);
        }
        transform.Translate(transform.up * m_speed * Time.deltaTime, Space.World);
        Debug.DrawRay(transform.position, transform.up);


    }

    void OnCollisionEnter(Collision coll) {
        if (Food.eatFood) {
            Vector3 point = transform.position - new Vector3(0.2f, 0f, 0.2f);
            GameObject clone = Instantiate(body/*, point, Quaternion.identity*/) as GameObject;

            GameObject tGBJ = GameObject.Find("Body");
            Body tBody = tGBJ.GetComponent<Body>();
            tBody.m_target = clone;
        }
    }
}