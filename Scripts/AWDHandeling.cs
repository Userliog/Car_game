using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWDHandeling : MonoBehaviour
{
    private float HorizontalInput;
    private float VerticalInput;

    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;

    [SerializeField] private float EngineMax;
    [SerializeField] private float BrakeMax;
    [SerializeField] private float MaxSteerAngle;

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
        FLcollider.motorTorque = VerticalInput * EngineMax;
        FRcollider.motorTorque = VerticalInput * EngineMax;
        RLcollider.motorTorque = VerticalInput * EngineMax;
        RRcollider.motorTorque = VerticalInput * EngineMax;

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
        FLcollider.brakeTorque = x * 20;
        FRcollider.brakeTorque = x * 20;
        RLcollider.brakeTorque = x;
        RRcollider.brakeTorque = x;
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
        //rot = rot * Quaternion.Euler(new Vector3(180, 90, 180));
        WT.position = pos;
        WT.rotation = rot;
    }

}
