using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWDHandelingOpponent : MonoBehaviour
{
    public Transform checkpoints;
    private List<Transform> nodes;
    private int currentNode = 0;
    private float targetSteerAngle = 0;

    private float HorizontalInput;
    private float VerticalInput;

    //private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;
    private double speed;

    [SerializeField] private float EngineMax;
    [SerializeField] private float BrakeMax;
    [SerializeField] private float MaxSteerAngle;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float turnSpeed;

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

        Transform[] pathTransforms = checkpoints.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        //0 --> 1
        for (int i = 1; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    void FixedUpdate()
    {
        Steering();
        Engine();
        CheckpointDistance();
        UpdateWheels();
        LerpSteer();
    }

    private void Engine()
    {
        //        Math.PI
        speed = 3.14 * FRcollider.radius * FRcollider.rpm * 60 / 500;

        if (speed < MaxSpeed)
        {
            FLcollider.motorTorque = ((EngineMax * 5) / 4);
            FRcollider.motorTorque = ((EngineMax * 5) / 4);
        }
        else
        {
            FLcollider.motorTorque = 0;
            FRcollider.motorTorque = 0;
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
    private void CheckpointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 2f)
        {
            ApplyBrake(5);
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    private void ApplyBrake(float x)
    {
        FLcollider.brakeTorque = x;
        FRcollider.brakeTorque = x;
        RLcollider.brakeTorque = x;
        RRcollider.brakeTorque = x;

        System.Threading.Thread.Sleep(2300);
        ApplyBrake(0);
    }

    private void Steering()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float currentSteerAngle = (relativeVector.x / relativeVector.magnitude) * MaxSteerAngle;
        targetSteerAngle = currentSteerAngle;
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
        rot = rot * Quaternion.Euler(new Vector3(180, 90, 180));
        WT.position = pos;
        WT.rotation = rot;
    }

    private void LerpSteer()
    {
        FLcollider.steerAngle = Mathf.Lerp(FLcollider.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        FRcollider.steerAngle = Mathf.Lerp(FRcollider.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }
}
