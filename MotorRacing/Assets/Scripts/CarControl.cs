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


        foreach (WheelElements collider in wheelData)
        {
            if (collider.shouldSteer)
            {
                collider.leftWheel.steerAngle = steer;
                collider.rightWheel.steerAngle = steer;
            }

            if (collider.addWheelTorque)
            {
                collider.leftWheel.motorTorque = speed;
                collider.rightWheel.motorTorque = speed;
            }

            doTires(collider.leftWheel);
            doTires(collider.rightWheel);
        }
    }

    void doTires(WheelCollider collider)
    {
        Transform tire = collider.transform.parent.transform.GetChild(1);

        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation); 

        rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z+90); 

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
