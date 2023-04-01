using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{

    public List<WheelElements> wheelData;

    public float maxTorque;
    public float maxSteerAngle = 30;

    private void FixedUpdate()
    {
        float speed = Input.GetAxis("Vertical") * maxTorque;
        float steer = Input.GetAxis("Horizontal") * maxSteerAngle;

        print(speed);
        print(steer);

        foreach (WheelElements wheel in wheelData)
        {
            if (wheel.shouldSteer)
            {
                wheel.leftWheel.steerAngle = steer;
                wheel.rightWheel.steerAngle = steer;
            }

            if (wheel.addWheelTorque)
            {
                wheel.leftWheel.motorTorque = speed;
                wheel.rightWheel.motorTorque = speed;
            }

            doTires(wheel.leftWheel);
            doTires(wheel.rightWheel);
        }
    }

    void doTires(WheelCollider wheel)
    {
        if (wheel.transform.childCount == 0)
        {
            return;
        }

        print("test");
        Transform tire = wheel.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;

        wheel.GetWorldPose(out position, out rotation); 

        tire.transform.position = position;
        tire.transform.rotation = rotation;
    }
}

[System.Serializable]
public class WheelElements
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;

    public bool addWheelTorque;
    public bool shouldSteer;
}
