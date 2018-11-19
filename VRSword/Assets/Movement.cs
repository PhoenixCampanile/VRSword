
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform trackingSpace;
    public Vector3 trackingSpaceRot;

    public Transform head;
    public Transform controller;

    public GameObject sword;
    public GameObject handle;
    public Text uiText;
    public float speed;
    public float rads,degs;
    [Range(-1,1)]
    public float touchX;
    [Range(-1,1)]
    public float touchY;
    public Vector2 emulatedtouchpos;
    public Vector3 velocity;

    [Range(0,360)]
    public float emulatedControllerRotX;
    [Range(0,360)]
    public float emulatedControllerRotY;
    [Range(0,360)]
    public float emulatedControllerRotZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trackingSpaceRot = trackingSpace.eulerAngles;
        //Test to see if program is in editor
        if(Application.isEditor){
            emulatedtouchpos = new Vector2(touchX,touchY);
            if(emulatedtouchpos!=Vector2.zero){
                rads = Mathf.Atan2(emulatedtouchpos.y,emulatedtouchpos.x)-trackingSpaceRot.y*Mathf.Deg2Rad;
                degs = rads*Mathf.Rad2Deg;
                velocity = new Vector3(Mathf.Cos(rads),0,Mathf.Sin(rads));
                Vector3 translation = (velocity/100)*Vector2.Distance(Vector2.zero,emulatedtouchpos)*speed;
                transform.Translate(translation);
            }

            //sword movement
           //sword.transform.eulerAngles = new Vector3(controller.eulerAngles.x,head.eulerAngles.y+controller.eulerAngles.y,sword.transform.eulerAngles.z);
           //handle.transform.eulerAngles = new Vector3(handle.transform.eulerAngles.x,handle.transform.eulerAngles.y,controller.rotation.eulerAngles.z);
            //sword.transform.rotation = new Quaternion(0,0,emulatedControllerRotZ,0);
           // sword.transform.rotation = Quaternion.Euler(emulatedControllerRotX,emulatedControllerRotY,0); 
            //sword2.transform.localRotation = Quaternion.Euler(sword2.transform.localRotation.x,0,emulatedControllerRotZ);
            //sword.transform.Rotate(emulatedControllerRotX,emulatedControllerRotY,emulatedControllerRotZ);
        }else{
            //gets the finger position on the gearvr controller touchpad
            Vector2 touches = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote);
            //if not touching the pad we dont want to move
            if(touches!=Vector2.zero){
                //atan2 gets the angle in radians from (0,0) to (x,y) in a circle
                rads = Mathf.Atan2(touches.y,touches.x)-trackingSpace.eulerAngles.y*Mathf.Deg2Rad;;
                degs = rads*Mathf.Rad2Deg;
                velocity = new Vector3(Mathf.Cos(rads),0,Mathf.Sin(rads));
                transform.Translate((velocity/100)*Vector2.Distance(Vector2.zero,touches)*speed*5);
            }
            
            //sword movement
           //sword.transform.eulerAngles = new Vector3(controller.eulerAngles.x,head.eulerAngles.y+controller.eulerAngles.y,sword.transform.eulerAngles.z);
           //handle.transform.eulerAngles = new Vector3(handle.transform.eulerAngles.x,handle.transform.eulerAngles.y,controller.rotation.eulerAngles.z);
            //sword.transform.rotation = new Quaternion(sword.transform.rotation.x,sword.transform.rotation.y,OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote).z,0);
            //sword2.transform.localRotation = new Quaternion(sword2.transform.localRotation.x,0,OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote).z,0);
            //OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        }
    }
}
