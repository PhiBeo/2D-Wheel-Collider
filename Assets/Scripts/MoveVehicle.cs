using UnityEngine;

public class MoveVehicle : MonoBehaviour
{
    [SerializeField] private WheelJoint2D[] wheelJoint2Ds;

    [SerializeField] private float targetMotorForce;
    [SerializeField] private float motorAccelerate;
    [SerializeField] private float motordeccelerate;

    [SerializeField] private float currentForce = 0f;

    private bool isAccel = false;

    void Start()
    {
        
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

        foreach(var wheel in wheelJoint2Ds)
        {
            JointMotor2D motor = wheel.motor;
            motor.motorSpeed = currentForce;
            wheel.motor = motor;
        }

        isAccel = false;
    }
}
