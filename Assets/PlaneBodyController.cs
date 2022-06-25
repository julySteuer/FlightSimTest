using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBodyController : MonoBehaviour
{
    Rigidbody body;
    public float thrust; 
    // Start is called before the first frame update
    void Start()
    {
        body = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("space")) {
            body.AddForce(transform.right * thrust);
        }
    }
}
