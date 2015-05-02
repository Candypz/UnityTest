using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {
    float m_x, m_y, m_z;
    Vector3 m_foodPosition;
    public static bool eatFood = false;
    // Use this for initialization
    void Start() {
        transform.RotateAround(transform.position, new Vector3(1, 0, 0), 90);
        m_x = Random.Range(-4, 4);
        m_y = 0.4249105f;
        m_z = Random.Range(-4, 4);
        Vector3 m_foodPosition = new Vector3(m_x, m_y, m_z);
        this.transform.position = m_foodPosition;
    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter(Collision coll) {
        Debug.Log(coll.gameObject.name);
        GameObject.Instantiate(transform);
        Destroy(gameObject);
        eatFood = true;
    }
}
