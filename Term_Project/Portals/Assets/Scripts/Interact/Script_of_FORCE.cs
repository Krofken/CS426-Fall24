using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_of_FORCE : MonoBehaviour {
    public Rigidbody rb;
    public float force_Horizontal = 0f;
    public float force_Vertical = 0f;
    public float force_Lateral = 0f;

    void Start() {
        if (rb == null) {
            rb = GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate() {


        
        if (rb != null) {
            rb.AddForce(Vector3.forward * force_Horizontal);
            rb.AddForce(Vector3.up * force_Vertical);
            rb.AddForce(Vector3.right * force_Lateral);
        }
    }
}