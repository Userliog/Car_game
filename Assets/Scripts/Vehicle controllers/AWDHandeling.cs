using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWDHandeling : MonoBehaviour
{
    private float HorizontalInput;
    private float VerticalInput;

    private float currentSteerAngle;
    private float currentBrakForce;
    private bool isBraking;
    private double speed;

    [Header("Constant values")]
    [SerializeField] private float EngineMax;
    [SerializeField] private float BrakeMax;
    [SerializeField] private float MaxSteerAngle;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float turnSpeed;

    [Header("Wheel colliders")]
    [SerializeField] private WheelCollider FLcollider;
    [SerializeField] private WheelCollider FRcollider;
    [SerializeField] private WheelCollider RLcollider;
    [SerializeField] private WheelCollider RRcollider;

    [SerializeField] private Transform FL;
    [SerializeField] private Transform FR;
    [SerializeField] private Transform RL;
    [SerializeField] private Transform RR;


    [Header("Center of mass")]
    private Rigidbody RB;
    [SerializeField] public Transform com;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.centerOfMass = com.transform.localPosition;
    }

    void FixedUpdate()
    {
        GetInput();
        Engine();
        Steering();
        UpdateWheels();
    }

    /// <summary>
    ///  This class gets inputs from the player and saves them as variables.
    /// </summary>
    private void GetInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    /// <summary>
    ///  This class applies acceleration or braking depending on the inputs from the player. 
    /// </summary>
    private void Engine()
    {
        speed = (3.14 * FRcollider.radius * FRcollider.rpm * 60) / 500;
        //Here is the wheelspeed of the car is calculated.
        if (speed < MaxSpeed)
        {
            ApplyThrottle(VerticalInput * ((EngineMax * 5) / 4));
        }
        else
        {
            ApplyThrottle(0);
        }

        currentBrakForce = isBraking ? BrakeMax : 0f;
        if (isBraking)
        {
            ApplyBrake(currentBrakForce);
        }
        else
        {
            ApplyBrake(0);
        }
    }

    /// <summary>
    ///  This class applies the acceleration to each of the wheels. 
    /// </summary>
    private void ApplyThrottle(float throttle)
    {
        FLcollider.motorTorque = throttle;
        FRcollider.motorTorque = throttle;
        RLcollider.motorTorque = throttle;
        RRcollider.motorTorque = throttle;
    }

    /// <summary>
    ///  This class applies the braking to the wheels. 
    /// </summary>
    private void ApplyBrake(float brake)
    {
        FLcollider.brakeTorque = brake;
        FRcollider.brakeTorque = brake;
        RLcollider.brakeTorque = brake * 100;
        RRcollider.brakeTorque = brake * 100;
    }

    /// <summary>
    ///  This class steers the wheels 
    /// </summary>
    private void Steering()
    {
        currentSteerAngle = MaxSteerAngle * HorizontalInput;
        FLcollider.steerAngle = Mathf.Lerp(FLcollider.steerAngle, currentSteerAngle, Time.deltaTime * turnSpeed);
        FRcollider.steerAngle = Mathf.Lerp(FRcollider.steerAngle, currentSteerAngle, Time.deltaTime * turnSpeed);
        //Lerp is used to slowdown the turning, witch improvies the controlability of the car.
    }

    /// <summary>
    ///  This class calls another class to update each wheel, I chose to do it this way to reduce the amout of repedetive code.
    /// </summary>
    private void UpdateWheels()
    {
        UpdateSingelWheel(FLcollider, FL);
        UpdateSingelWheel(FRcollider, FR);
        UpdateSingelWheel(RLcollider, RL);
        UpdateSingelWheel(RRcollider, RR);
    }

    /// <summary>
    ///  This class uppdates the look of the wheels, otherwise the wheels would stay in the same position relative to the car as from the begining.
    /// </summary>
    private void UpdateSingelWheel(WheelCollider WheelCollider, Transform WheelTransform)
    {
        Vector3 pos = WheelTransform.position;
        Quaternion rot = WheelTransform.rotation;
        WheelCollider.GetWorldPose(out pos, out rot);
        rot = rot * Quaternion.Euler(new Vector3());
        WheelTransform.position = pos;
        WheelTransform.rotation = rot;
    }
}
