using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour {
    private Rigidbody rb;
    int i = 0;
    public int limit_max = 1000;
    public int limit_min = 50;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 temp = rb.velocity;
        float mag = temp.sqrMagnitude;
       
        if (Input.GetKey("a") && mag<limit_max || mag<limit_min)
        {
            rb.velocity *= 1.1f;
            Debug.Log(i + "  Accel");
        }
       
        if (Input.GetKey("s") && mag>limit_min || mag>limit_max)
        {
            rb.velocity *= 0.9f;
            Debug.Log(i + "  slow");
        }
        
        Debug.Log(i + " : " + mag);
        i++;
    }
}
