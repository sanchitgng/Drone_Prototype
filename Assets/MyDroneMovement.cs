using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDroneMovement : MonoBehaviour
{
    Rigidbody myDrone;
    public float upForce;
    public float movementForwardSpeed = 500f;
    public float tiltAmountForward = 0f;
    public float tiltVelocityForward;

    // Start is called before the first frame update
    void Start()
    {
        myDrone = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovementUpDown();
        MovementForward();
        myDrone.AddRelativeForce(Vector3.up * upForce);
        myDrone.rotation = Quaternion.Euler(
            new Vector3(tiltAmountForward, myDrone.rotation.y , myDrone.rotation.z)
            );
    }

    private void MovementForward()
    {
      if(Input.GetAxis("Vertical") != 0)
        {
            myDrone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.1f);
        }   
    }

    private void MovementUpDown()
    {
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            if(Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.K))
            {
                myDrone.velocity = myDrone.velocity;
            }
            if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L))
            {
                myDrone.velocity = new Vector3(myDrone.velocity.x, Mathf.Lerp(myDrone.velocity.y, 0, Time.deltaTime * 5), myDrone.velocity.z);
                upForce = 281;
            }
            if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)))
            {
                myDrone.velocity = new Vector3(myDrone.velocity.x, Mathf.Lerp(myDrone.velocity.y, 0, Time.deltaTime * 5), myDrone.velocity.z);
                upForce = 110;
            }
            if(Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
            {
                upForce = 410;
            }
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            upForce = 135;
        }
        if (Input.GetKey(KeyCode.I))
        {
            upForce = 450;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
            {
                upForce = 500;
            }
        }
        else if (Input.GetKey(KeyCode.K))
        {
            upForce = -200;
        }
        else if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f))
        {
            upForce = 98.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
