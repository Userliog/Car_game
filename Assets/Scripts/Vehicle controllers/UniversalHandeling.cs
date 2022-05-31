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
        speed = (3.14f * FRcollider.radius * FRcollider.rpm * 60f) / 500f;
        //print(speed);
        WheelHit FLhit = new WheelHit();
        WheelHit FRhit = new WheelHit();
        WheelHit RLhit = new WheelHit();
        WheelHit RRhit = new WheelHit();
        FLcollider.GetGroundHit(out FLhit);
        FLcollider.GetGroundHit(out FRhit);
        FLcollider.GetGroundHit(out RLhit);
        FLcollider.GetGroundHit(out RRhit);

        //current Extremum slip
        float FrontExs = 1f;
        float RearExs = 0.4f;

        //current sideways slip divided by Extremum slip



        print("-------------------------------------");
        //print("FL frowardSlip: " + FLhit.forwardSlip);
        print("FL: " + FLhit.sidewaysSlip / FrontExs);
        //print("FR frowardSlip: " + FRhit.forwardSlip);
        print("FR: " + FRhit.sidewaysSlip / FrontExs);
        //print("RL frowardSlip: " + RLhit.forwardSlip);
        print("RL: " + RLhit.sidewaysSlip / RearExs);
        //print("RR frowardSlip: " + RRhit.forwardSlip);
        print("RR: " + RRhit.sidewaysSlip / RearExs);

        print("Speed: " + speed);
        //print(speed / MaxSpeed);
        print("-------------------------------------");

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

    private void ApplyBrake(float brake)
    {
        FLcollider.brakeTorque = brake;
        FRcollider.brakeTorque = brake;
        RLcollider.brakeTorque = brake * 100;
        RRcollider.brakeTorque = brake * 100;
    }

    private void Steering()
    {

        //float k = (((MaxSpeedSteer / MaxSteerAngle) - 1f) / MaxSpeed);

        currentSteerAngle = (MaxSteerAngle * HorizontalInput) * ((((MaxSpeedSteer / MaxSteerAngle) - 1f) / MaxSpeed) * speed + 1);


        FLcollider.steerAngle = Mathf.Lerp(FLcollider.steerAngle, currentSteerAngle, Time.deltaTime * turnSpeed);
        FRcollider.steerAngle = Mathf.Lerp(FRcollider.steerAngle, currentSteerAngle, Time.deltaTime * turnSpeed);
        //print(currentSteerAngle);
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
