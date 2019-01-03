using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : MonoBehaviour {
    GameObject wagon;
    GameObject point;
    Transform target;
    public float speed=0.041f;
    public float rotSpeed = 4f;
    float rs;
    int i;
    int last;
    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        i = 0;
        wagon = GameObject.Find("Wagon_rollercoaster");
        point = GameObject.Find("Point");
        target = point.transform.GetChild(i);
        last = point.transform.childCount;
        rb = GetComponent<Rigidbody>();

        rs = rotSpeed / (target.position - transform.position).magnitude;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log(target.name);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        
        Vector3 vec = target.position - transform.position;
        Vector3 look = Vector3.Slerp(transform.forward, vec.normalized,rs* Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(look,Vector3.up);
        
        if (transform.position == target.position)
        {
            i++;
            
            if (i == last)
            {
                rb.isKinematic = false;
                wagon.GetComponent<Forward>().enabled = false;
            }
            
            target = point.transform.GetChild(i);
            rs = 4/(target.position-transform.position).magnitude;
            Debug.Log("------------------------" + rs);
        }        
    }
}
