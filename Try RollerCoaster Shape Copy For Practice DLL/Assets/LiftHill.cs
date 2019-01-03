using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftHill : MonoBehaviour {

    GameObject wagon;
    GameObject front;
    GameObject target;
    private Rigidbody rb;
    private Rigidbody rigidBody;
    public float Speed = 3;  // 속도
    public float TitleTime = 0;//영상의 초반 인트로(단위: 초)
    float limit_min;
    bool doUpdate = false;
    Vector3 add = new Vector3(1, 1, 1);

    // Use this for initialization
    void Start () {
        wagon = GameObject.Find("Wagon_rollercoaster");
        front = wagon.transform.Find("Front").gameObject;
        target = GameObject.Find("targetPoint");
        rb = wagon.GetComponent<Rigidbody>();
        limit_min = wagon.GetComponent<Force>().limit_min;
        rigidBody = GetComponent<Rigidbody>();
        StartCoroutine(DelayTime(TitleTime));
    }
    IEnumerator DelayTime(float delayTime)
    {
        
        yield return new WaitForSeconds(delayTime);
        doUpdate = true;
    }
    
	
	// Update is called once per frame
	void Update () {
        
        if (!doUpdate) return;
        
        //방법1
        transform.position = Vector3.Lerp(transform.position,wagon.transform.position,Time.deltaTime * Speed);
        transform.LookAt(wagon.transform);
        //방법2
        //var heading = wagon.transform.position - transform.position;
        //rigidBody.AddForce(heading);
        //방법3 
        //transform.position = Vector3.MoveTowards(transform.position, wagon.transform.position, Time.deltaTime * Speed);
        //transform.LookAt(wagon.transform);
        if (rb.velocity.sqrMagnitude > limit_min)
        {
            Destroy(gameObject);
            wagon.GetComponent<Force>().enabled = true;
        }
    }
}


   
