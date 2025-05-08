using Unity.Collections;
using UnityEngine;

[Tooltip("This help control both wheel joint component without need to adjust some value individually.")]
public class MoveVehicle : MonoBehaviour
{
    [Header("Wheel Joints Components")]
    [SerializeField] private WheelJoint2D[] wheelJoints;

    [Header("Suspension")]
    [SerializeField] private float dampingRatio;
    [SerializeField] private float frequency;
    [SerializeField] private float angle;

    [Header("Motor")]
    [SerializeField, Tooltip("Max motor force")] private float targetMotorForce;
    [SerializeField, Tooltip("How much the force go up in 1 sec")] private float motorAccelerate;
    [SerializeField, Tooltip("How much the force go down in 1 sec")] private float motordeccelerate;

    private float currentForce = 0f;

    private bool isAccel = false;

    void Start()
    {
        foreach(var joint in wheelJoints)
        {
            var suspension = joint.suspension;
            suspension.dampingRatio = dampingRatio;
            suspension.frequency = frequency;
            suspension.angle = angle;
            joint.suspension = suspension;
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            currentForce -= motorAccelerate * Time.deltaTime;
            isAccel = true;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            currentForce += motorAccelerate * Time.deltaTime;
            isAccel = true;
        }

        if(!isAccel)
        {
            if(currentForce > 0)
            {
                currentForce -= motordeccelerate * Time.deltaTime;
                currentForce = Mathf.Clamp(currentForce, 0, currentForce);
            }
            else if(currentForce < 0)
            {
                currentForce += motordeccelerate * Time.deltaTime;
                currentForce = Mathf.Clamp(currentForce, currentForce, 0);
            }
        }

        currentForce = Mathf.Clamp(currentForce, -targetMotorForce, targetMotorForce);

        foreach(var wheel in wheelJoints)
        {
            JointMotor2D motor = wheel.motor;
            motor.motorSpeed = currentForce;
            wheel.motor = motor;
        }

        isAccel = false;
    }
}
