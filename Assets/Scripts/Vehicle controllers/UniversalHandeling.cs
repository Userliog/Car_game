using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalHandeling : MonoBehaviour
{
    private float HorizontalInput;
    private float VerticalInput;

    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;
    private float speed;

    [Header("Constant values")]
    [SerializeField] private float EngineMax;
    [SerializeField] private float BrakeMax;
    [SerializeField] private float MaxSteerAngle;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float MaxSpeedSteer;

    [Header("Wheel colliders")]
    [SerializeField] private WheelCollider FLcollider;
    [SerializeField] private WheelCollider FRcollider;
    [SerializeField] private WheelCollider RLcollider;
    [SerializeField] private WheelCollider RRcollider;

    [SerializeField] private Transform FL;
    [SerializeField] private Transform FR;
    [SerializeField] private Transform RL;
    [SerializeField] private Transform RR;

    [Header("Driving wheels")]

    public bool RWD;
    public bool FWD;
    public bool AWD;
    
    [Header("Center of mass")]
    [SerializeField] public Transform com;
    private Rigidbody RB;

    void Start()
    {
        //This changes the center of mass of the car
        RB = GetComponent<Rigidbody>();
        RB.centerOfMass = com.transform.localPosition;
    }

    /// <summary>
    ///  This class calls all other classes.
    /// </summary>
    void FixedUpdate()
    {
        GetInput();
        Engine();
        Steering();
        UpdateWheels();
    }

    /// <summary>
    ///  This class gets all inputs from the player and saves them as variables.
    /// </summary>
    private void GetInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    /// <summary>
    ///  This class acceraits the car to top speed.
    /// </summary>
    private void Engine()
    {
        //this calculates the current wheelspeed of the car
        speed = (3.14f * FRcollider.radius * FRcollider.rpm * 60f) / 500f;

        //When the car reaches top speed you cant accelerate any more
        if (speed < MaxSpeed)
        {
            ApplyThrottle(VerticalInput * ((EngineMax * 5) / 4));
        }
        else
        {
            ApplyThrottle(0);
        }

        //The force for the brakes is calculated by the braking pressure times maximum braking power.
        currentBreakForce = isBreaking ? BrakeMax : 0f;
        if (isBreaking)
        {
            ApplyBrake(currentBreakForce);
        }
        else
        {
            ApplyBrake(0);
        }
    }

    /// <param 
    /// name="throttle"> Float: The amount to accelerate the car.
    /// </param>
    /// <summary>
    ///  This function applies throttle to the driving wheels, depending on witch are selected
    /// </summary>
    private void ApplyThrottle(float throttle)
    {
        if (FWD == true)
        {
            FLcollider.motorTorque = throttle;
            FRcollider.motorTorque = throttle;
        }
        else if (RWD == true)
        {
            RLcollider.motorTorque = throttle;
            RRcollider.motorTorque = throttle;
        }
        else if (AWD == true)
        {
            FLcollider.motorTorque = throttle;
            FRcollider.motorTorque = throttle;
            RLcollider.motorTorque = throttle;
            RRcollider.motorTorque = throttle;
        }

    }

    /// <param 
    /// name="brake"> Float: The amount of braking force.
    /// </param>
    /// <summary>
    ///  This function applies braking to all the wheels, but has a rear bias
    /// </summary>
    private void ApplyBrake(float brake)
    {
        //The rear wheels have a higher force to act as a handbrake, but all wheels are applied to increese controllablity
        FLcollider.brakeTorque = brake;
        FRcollider.brakeTorque = brake;
        RLcollider.brakeTorque = brake * 100;
        RRcollider.brakeTorque = brake * 100;
    }

    /// <summary>
    ///  This function calculates and applies sterring to the front wheels
    /// </summary>
    private void Steering()
    {
        //The current steering angel is linear to the speed of the car, and has a lower steering angle at higher speeds, witch increases coner ability
        currentSteerAngle = (MaxSteerAngle * HorizontalInput) * ((((MaxSpeedSteer / MaxSteerAngle) - 1f) / MaxSpeed) * speed + 1f);

        //The sterring angle is applied to the wheels, and the time it takes to reach full lock can be adjusted
        FLcollider.steerAngle = Mathf.Lerp(FLcollider.steerAngle, currentSteerAngle, Time.deltaTime * turnSpeed);
        FRcollider.steerAngle = Mathf.Lerp(FRcollider.steerAngle, currentSteerAngle, Time.deltaTime * turnSpeed);
    }

    /// <summary>
    ///  This function calls another function to update each wheel, I choose to do it this way to reduce the amout of repedetive code.
    /// </summary>
    private void UpdateWheels()
    {
        UpdateSingelWheel(FLcollider, FL);
        UpdateSingelWheel(FRcollider, FR);
        UpdateSingelWheel(RLcollider, RL);
        UpdateSingelWheel(RRcollider, RR);
    }

    /// <param 
    /// name="WheelCollider"> WheelCollider: The wheel collider to base the new transform position of.
    /// </param>
    /// <param 
    /// name="WheelTransform"> Transform: The wheel transform to uppdate its position.
    /// </param>
    /// <summary>
    ///  This function updates the look of the wheels, otherwise the wheels would stay in the same position relative to the car as from the begining.
    /// </summary>
    private void UpdateSingelWheel(WheelCollider WheelCollider, Transform WheelTransform)
    {
        //The position of the wheel colliders is saved
        Vector3 pos = WheelTransform.position;
        Quaternion rot = WheelTransform.rotation;
        WheelCollider.GetWorldPose(out pos, out rot);
        rot = rot * Quaternion.Euler(new Vector3());

        //And applied to the wheel transform
        WheelTransform.position = pos;
        WheelTransform.rotation = rot;
    }
}
