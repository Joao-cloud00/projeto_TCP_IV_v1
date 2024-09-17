using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperControler : MonoBehaviour
{
    private Vector3 initialPosition;


    private void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.A)))
        {
            GetComponent<HingeJoint2D>().useMotor = true;
            print("Flippers ON");
        }
        else
        {
            GetComponent<HingeJoint2D>().useMotor = false;
            print("Flippers OFF");
            transform.position = initialPosition;
        }


    }
}
