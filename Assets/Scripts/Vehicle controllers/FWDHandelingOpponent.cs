using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;

public class FWDHandelingOpponent : MonoBehaviour
{
    public Transform checkpoints;
    private List<Transform> nodes;
    private int currentNode = 0;
    private float targetSteerAngle = 0;

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
        speed = 3.14 * FRcollider.radius * FRcollider.rpm * 60 / 500;

        if (speed < MaxSpeed)
        {
            ApplyThrottle((EngineMax * 5) / 4);
        }
        else
        {
            ApplyThrottle(0);
        }
    }

    private void ApplyThrottle(float throttle)
    {
        FLcollider.motorTorque = throttle;
        FRcollider.motorTorque = throttle;
    }

    private void CheckpointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 2f)
        {
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

    private void UpdateSingelWheel(WheelCollider WheelCollider, Transform WheelTransform)
    {
        Vector3 pos = WheelTransform.position;
        Quaternion rot = WheelTransform.rotation;
        WheelCollider.GetWorldPose(out pos, out rot);
        rot = rot * Quaternion.Euler(new Vector3());
        WheelTransform.position = pos;
        WheelTransform.rotation = rot;
    }

    private void LerpSteer()
    {
        FLcollider.steerAngle = Mathf.Lerp(FLcollider.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        FRcollider.steerAngle = Mathf.Lerp(FRcollider.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }
}
