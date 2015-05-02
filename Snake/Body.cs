using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {
    public GameObject m_target;

    float m_dist = 0.8f;
    Vector3 vectDir;
    void Start() {
        //m_target = null;
    }

    void Update() {
        move();
    }

    void move() {
        vectDir = m_target.transform.position - transform.position;
        float sd = vectDir.x * vectDir.x + vectDir.y * vectDir.y + vectDir.z * vectDir.z;
        float dd = sd - (m_dist * m_dist);
        if (dd >= 0) {
            transform.Translate(vectDir.normalized * dd * 0.07f);
        }
    }
}
