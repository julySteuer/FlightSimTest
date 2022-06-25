using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneWingController : MonoBehaviour
{
    Rigidbody rb;
    public float lift;
    AnimationCurve aoaCurve;
    AnimationCurve rollCurve;
    // One curve for rourqe around longitudanal axis and one for pitch AoA
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        aoaCurve = GetComponentInParent<MainController>().aoaCurve;
        rollCurve = GetComponentInParent<MainController>().rollCurve;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float aoa = Vector3.Angle(rb.velocity, transform.right);
        float scaledVector = rb.velocity.x/50 * lift;
        Vector3 force = scaledVector * rb.velocity.normalized;
        rb.AddForce(force);
        //Change lift to rotate 
    }
}
