using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityDLL;


public class GetWagonValue : MonoBehaviour {

    ApproachUnity AU = new ApproachUnity();

    public Vector3 pos;
    float posX;
    float posY;
    float posZ;

    float timer_for_record;
    float time;

    public float rotY;
    public float rotP;
    public float rotR;

    float preYaw;
    float Yaw;
    float roundYaw;

    int round;

    private Vector3 acceleration = new Vector3();
    private Vector3 preVel = new Vector3();
    //private Rigidbody obj;
    string m_strPath = "Assets/Resource/WagonData.txt";
    // Use this for initialization
    void Start()
    {
        AU.Init();
        //obj = this.gameObject.GetComponent<Rigidbody>();
        timer_for_record = 0.0f;
        preYaw = 0;
        round = 0;
        Debug.Log("Wegan Start!!");
        System.IO.File.WriteAllText(m_strPath, "", Encoding.Default);
        //InvokeRepeating("PeriodicUpdate",0f,0.01f);
    }


    public void WriteData(string strData)
    {
        //Debug.Log(strData);

        StreamWriter writer = new StreamWriter(m_strPath, true);
        writer.WriteLine(strData);

        writer.Close();
    }

    
    public float GetPitch()
    {
        var right = transform.right;
        right.y = 0;
        var fwd = Vector3.Cross(right, Vector3.up).normalized;
        //Debug.Log("transform.right = "+transform.right+"right = "+right+"transform.forward = "+transform.forward+"fwd = "+fwd);
        return Vector3.Angle(fwd, transform.forward) * Mathf.Sign(transform.forward.y);//need to thinkabout roll
    }

    public float GetRoll()
    {
        var fwd = transform.forward;
        fwd.y = 0;
        var right = Vector3.Cross(Vector3.up, fwd).normalized;
        //Debug.Log("transform.right = "+transform.right+"right = "+right+"transform.forward = "+transform.forward+"fwd = "+fwd);
        return Vector3.Angle(right, transform.right) * Mathf.Sign(-transform.right.y);
    }

    public float GetYaw()
    {
        var fwd = transform.forward;
        fwd.y = 0;
        fwd *= Mathf.Sign(transform.up.y);
        Yaw = Vector3.Angle(Vector3.forward, fwd) * Mathf.Sign(-fwd.x);

        if (Yaw - preYaw < -300) round++;
        else if (Yaw - preYaw > 300) round--;
        
        roundYaw = round * 360 + Yaw;        
        preYaw = Yaw;
        //Debug.Log("Global forward = " + Vector3.forward + "fwd = " + fwd + "Yaw = "+Yaw);
        return roundYaw;
     }


    public float MakeRad(float deg)
    {
        return deg * Mathf.PI/180;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer_for_record += Time.deltaTime;
        time = Mathf.Round(timer_for_record * 100) * 10;
        timer_for_record = time *0.001f;

        //acceleration = (Vector3)((obj.velocity - preVel) / Time.deltaTime);
        //preVel = obj.velocity;
        //Debug.Log(acceleration);

        pos = gameObject.transform.position;
        posX = pos.z; 
        posY = pos.x;
        posZ = -pos.y;
       
        rotP = MakeRad(GetPitch()*0.33f);
        rotR = MakeRad(GetRoll()*0.22f);
        rotY = MakeRad(GetYaw());
        

        AU.Get(time, posX, posY, posZ, rotY, rotP, rotR);
        WriteData(time+","+posX+","+posY+","+posZ+","+rotY+","+rotP+","+rotR);
    }

    void OnDisable()
    {
        AU.DeInit();
    }
}
