using System.Collections;
using System.Timers;
using System.Collections.Generic;
using UnityEngine;

public class FWDHandelingOpponent : MonoBehaviour
{
    [Header("Checkpoints")]
    public Transform checkpoints;
    private List<Transform> nodes;
    private int currentNode = 0;

    [Header("Constant values")]
    [SerializeField] private float EngineMax;
    [SerializeField] private float BrakeMax;
    [SerializeField] private float MaxSteerAngle;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float turnSpeed;
    private float currentEnginePower = 0;

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

    /// <summary>
    ///  This function setsup everything, and gets all requierd checkpoints and puts them in a list.
    /// </summary>
    void Start()
    {
        //This changes the center of mass of the car
        RB = GetComponent<Rigidbody>();
        RB.centerOfMass = com.transform.localPosition;

        //This gets all pathTransforms and puts them in a list
        Transform[] pathTransforms = checkpoints.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 1; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    /// <summary>
    ///  This function calls all other functions.
    /// </summary>
    void FixedUpdate()
    {
        Steering();
        Engine();
        CheckpointDistance();
        UpdateWheels();
    }

    /// <summary>
    ///  This function acceleraits the car to top speed.
    /// </summary>
    private void Engine()
    {
        double speed = 3.14 * FRcollider.radius * FRcollider.rpm * 60 / 500;
        //this calculates the current wheelspeed of the car

        //When the car reaches top speed you cant accelerate any more
        if (speed < MaxSpeed)
        {
            ApplyThrottle((currentEnginePower * 5) / 4);
        }
        else
        {
            ApplyThrottle(0);
        }
    }

    /// <param 
    /// name="throttle"> Float: The amount of throttle.
    /// </param>
    /// <summary>
    ///  This function applies the motortorque to the front wheels.
    /// </summary>
    private void ApplyThrottle(float throttle)
    {
        FLcollider.motorTorque = throttle;
        FRcollider.motorTorque = throttle;
    }

    /// <summary>
    ///  This function calculates the distance from the car to the next checkpoint in the list, and if the car is close enoth to the checkpoint, it switches to the next one in the list.
    /// </summary>
    private void CheckpointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 2f)
        {
            if (currentNode == nodes.Count - 1)
            {
                //if the car has reached the last checkpoint, the list will start over, so the car can drive multible laps.
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    /// <summary>
    ///  This function calculates how much the car needs to steer to reach the checkpoint.
    /// </summary>
    private void Steering()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        //The checkpoints postion is saved as a Vector3 variable.
        float currentSteerAngle = (relativeVector.x / relativeVector.magnitude) * MaxSteerAngle;
        //the steering angle of the car is calculated by taking the checkoints position on the x-axis and deviding it by the distance to the world center, and then multiplying it by the maximum steering angel of the car.
        FLcollider.steerAngle = Mathf.Lerp(FLcollider.steerAngle, currentSteerAngle, Time.deltaTime * turnSpeed);
        FRcollider.steerAngle = Mathf.Lerp(FRcollider.steerAngle, currentSteerAngle, Time.deltaTime * turnSpeed);
        //The final steering angel is calculated by using lerp to smoothen out the steering towards the checkpoint.
    }

    /// <summary>
    ///  This function calls another function to update each wheel, I chose to do it this way to reduce the amout of repedetive code.
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
        //First it gets the position and rotation of the wheel collider, and converts the values to be in the worldspace
        Vector3 pos = WheelTransform.position;
        Quaternion rot = WheelTransform.rotation;
        WheelCollider.GetWorldPose(out pos, out rot);
        rot = rot * Quaternion.Euler(new Vector3());

        //Second it applies these new values to the wheel transform to update the rotation and postion.
        WheelTransform.position = pos;
        WheelTransform.rotation = rot;
    }

    /// <summary>
    ///  This function makes it possible to start the car on demand by changing the power to the wheels from 0 to engine max.
    /// </summary>
    public void StartCar()
    {
        currentEnginePower = EngineMax;
    }
}
