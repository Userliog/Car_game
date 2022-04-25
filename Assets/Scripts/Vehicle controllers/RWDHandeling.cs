using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private float HorizontalInput;
    private float VerticalInput;

    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;
    private double speed;

    [SerializeField] private float EngineMax;
    [SerializeField] private float BrakeMax;
    [SerializeField] private float MaxSteerAngle;
    [SerializeField] private float MaxSpeed;

    [SerializeField] private WheelCollider FLcollider;
    [SerializeField] private WheelCollider FRcollider;
    [SerializeField] private WheelCollider RLcollider;
    [SerializeField] private WheelCollider RRcollider;

    [SerializeField] private Transform FL;
    [SerializeField] private Transform FR;
    [SerializeField] private Transform RL;
    [SerializeField] private Transform RR;

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

    private void GetInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void Engine()
    {
        speed = 3.14 * FRcollider.radius * FRcollider.rpm * 60 / 500;

        if (speed < MaxSpeed)
        {
            RLcollider.motorTorque = VerticalInput * ((EngineMax * 5) / 4);
            RRcollider.motorTorque = VerticalInput * ((EngineMax * 5) / 4);
        }
        else
        {
            FLcollider.motorTorque = 0;
            FRcollider.motorTorque = 0;
        }
        //Debug.Log(speed);

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

    private void ApplyBrake(float x)
    {
        FLcollider.brakeTorque = x;
        FRcollider.brakeTorque = x;
        RLcollider.brakeTorque = x * 10;
        RRcollider.brakeTorque = x * 10;
    }

    private void Steering()
    {
        currentSteerAngle = MaxSteerAngle * HorizontalInput;
        FLcollider.steerAngle = currentSteerAngle;
        FRcollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingelWheel(FLcollider, FL);
        UpdateSingelWheel(FRcollider, FR);
        UpdateSingelWheel(RLcollider, RL);
        UpdateSingelWheel(RRcollider, RR);
    }

    private void UpdateSingelWheel(WheelCollider WC, Transform WT)
    {
        Vector3 pos = WT.position;
        Quaternion rot = WT.rotation;
        WC.GetWorldPose(out pos, out rot);
        rot = rot * Quaternion.Euler(new Vector3(0, 0, 0));
        WT.position = pos;
        WT.rotation = rot;
    }

}
