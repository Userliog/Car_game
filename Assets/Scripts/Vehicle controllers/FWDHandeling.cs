using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWDHandeling : MonoBehaviour
{
    private float HorizontalInput;
    private float VerticalInput;
    
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;
    private double speed;

    [Header("Constant values")]
    [SerializeField] private float EngineMax;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float BrakeMax;
    [SerializeField] private float MaxSteerAngle;
    [SerializeField] private float turnSpeed;

    [Header("Wheel colliders")]
    [SerializeField] private WheelCollider FLcollider;
    [SerializeField] private WheelCollider FRcollider;
    [SerializeField] private WheelCollider RLcollider;
    [SerializeField] private WheelCollider RRcollider;

    [Header("Wheel transforms")]
    [SerializeField] private Transform FL;
    [SerializeField] private Transform FR;
    [SerializeField] private Transform RL;
    [SerializeField] private Transform RR;

    private Rigidbody RB;
    [Header("Center of mass")]
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

    private void GetInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void Engine()
    {
        speed = 3.14 * FRcollider.radius * FRcollider.rpm * 60/500;

        if (speed < MaxSpeed)
        {
            ApplyThrottle(VerticalInput * ((EngineMax * 5) / 4));
        }
        else
        {
            ApplyThrottle(0);
        }

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
    
    private void ApplyThrottle(float throttle)
    {
        FLcollider.motorTorque = throttle;
        FRcollider.motorTorque = throttle;
    }

    private void ApplyBrake(float brake)
    {
        FLcollider.brakeTorque = brake;
        FRcollider.brakeTorque = brake;
        RLcollider.brakeTorque = brake * 100;
        RRcollider.brakeTorque = brake * 100;
    }

    private void Steering()
    {
        currentSteerAngle = MaxSteerAngle * HorizontalInput;
        FLcollider.steerAngle = Mathf.Lerp(FLcollider.steerAngle, currentSteerAngle, Time.deltaTime * turnSpeed);
        FRcollider.steerAngle = Mathf.Lerp(FRcollider.steerAngle, currentSteerAngle, Time.deltaTime * turnSpeed);
    }

    private void UpdateWheels()
    {
        UpdateSingelWheel(FLcollider, FL);
        UpdateSingelWheel(FRcollider, FR);
        UpdateSingelWheel(RLcollider, RL);
        UpdateSingelWheel(RRcollider, RR);
    }

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
