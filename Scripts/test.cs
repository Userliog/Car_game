using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Destroy(gameObject);
            rb.velocity = Vector3.forward * 30f;
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 200);
        }
    }


    private void OnMouseDown()
    {
        Destroy(gameObject);
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
