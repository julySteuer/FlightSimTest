using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public float sensitivity;
    Rigidbody rb;
    public GameObject leftWing;
    public GameObject rightWing;
    public float aileronStrength;
    public float maxRollRate;// More Roll rate
    public AnimationCurve aoaCurve;
    public AnimationCurve rollCurve;

    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 worldPosition = Input.mousePosition;
        float normX = (worldPosition.x / Screen.width)-0.5f;
        float normY = (worldPosition.y / Screen.height)-0.5f;
        float aileronInPercent = normY * sensitivity;
        // Angular Drag
        // Revrite aileron control 
        // Angular velocity twiches
        Vector3 controlInput = Vector3.zero;
        if (Input.GetKey("d")) {
            controlInput.x = aileronStrength;
        }
        if (Input.GetKey("a")) {
            controlInput.x = -aileronStrength;
        }
        if (Input.GetKey("w"))
        {
            controlInput.z = aileronStrength;
        }
        if (Input.GetKey("s"))
        {
            controlInput.z = -aileronStrength;
        }
        // Debug each velocity
        Debug.Log(new Vector3(
            calcAngularVelocity(rb.angularVelocity.x, controlInput.x, maxRollRate),
            calcAngularVelocity(rb.angularVelocity.y, controlInput.y, maxRollRate),
            calcAngularVelocity(rb.angularVelocity.z, controlInput.z, maxRollRate)
            ));
        rb.AddRelativeTorque(new Vector3(
            calcAngularVelocity(rb.angularVelocity.x, controlInput.x, maxRollRate),
            calcAngularVelocity(rb.angularVelocity.y, controlInput.y, maxRollRate),
            calcAngularVelocity(rb.angularVelocity.z, controlInput.z, maxRollRate)
            ), ForceMode.VelocityChange);
        //Change aileron stregth 
        /*
        var dt = Time.fixedDeltaTime;
        var speed = Mathf.Max(0, rb.velocity.z);
        var targetAV = Vector3.Scale(controlInput, new Vector3(aileronStrength, aileronStrength, aileronStrength));
        var av = rb.angularVelocity * Mathf.Rad2Deg;
        var correction = new Vector3(
            CalculateSteering(dt, av.x, targetAV.x, maxRollRate),
            CalculateSteering(dt, av.y, targetAV.y, maxRollRate),
            CalculateSteering(dt, av.z, targetAV.z, maxRollRate)
        );
        rb.AddRelativeTorque(correction * Mathf.Deg2Rad, ForceMode.VelocityChange);
        */
     }

    float calcAngularVelocity(float currAcceleration, float desiredVelocity, float maxVelocity) { 
        float difference = desiredVelocity - currAcceleration;
        float accel = maxVelocity * Time.fixedDeltaTime;
        return Mathf.Clamp(difference, -accel, accel);
    }

    float CalculateSteering(float dt, float angularVelocity, float targetVelocity, float acceleration)
    {
        var error = targetVelocity - angularVelocity;
        var accel = acceleration * dt;
        return Mathf.Clamp(error, -accel, accel);
    }
}

