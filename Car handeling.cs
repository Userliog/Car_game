using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carhandeling : MonoBehaviour
{
    public WheelCollider Frontleftcollider;
    public WheelCollider Frontrightcollider;
    public WheelCollider Rearleftcollider;
    public WheelCollider Rearrightcollider;
    public Transform Frontleft;
    public Transform Frontright;
    public Transform Rearleft;
    public Transform Rearright;
    public Vector3 Centerofmass;
    float maxFSpeed = -3000;
    float maxRSpeed = 1000;
    double gravity = 9.8;
    private bool braked = false;
    private float maxBrakeTourqe = 500;
    private Rigidbody rb;
    private float maxTorque = 1000;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Centerofmass.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
