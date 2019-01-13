
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
    public GameObject hand;
    public GameObject gripPoint;

    public GameObject equippedWeapon;
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
        if(Application.isEditor){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            transform.Translate(0,2,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.isEditor){
            if(Input.GetKey(KeyCode.Escape)){
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; 
            }
            transform.Rotate(0,Input.GetAxis("Horizontal"),0);
            transform.Translate(0,0,Input.GetAxis("Vertical")/50);
            controller.transform.Rotate(-Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0);
            if(Input.GetMouseButton(0)){
                if(hand.transform.parent.GetComponent<Animation>().clip == hand.transform.parent.GetComponent<Animation>().GetClip("HandOpen")){
                    hand.transform.parent.GetComponent<Animation>().Stop();
                    hand.transform.parent.GetComponent<Animation>().clip = hand.transform.parent.GetComponent<Animation>().GetClip("HandClose");
                    hand.transform.parent.GetComponent<Animation>().Play(); 
                }
                if(hand.GetComponent<CollisionDetector>().colliding&&this.equippedWeapon==null){
                    if(hand.GetComponent<CollisionDetector>().GetCollidedObject().transform.gameObject!=equippedWeapon&&hand.GetComponent<CollisionDetector>().GetCollidedObject().transform.tag=="Weapon"){
                        hand.GetComponent<CollisionDetector>().GetCollidedObject().GetComponent<Rigidbody>().isKinematic = true;
                        this.equippedWeapon = hand.GetComponent<CollisionDetector>().GetCollidedObject().transform.gameObject;
                        
                        this.equippedWeapon.transform.parent = gripPoint.transform;
                        this.equippedWeapon.transform.localPosition = Vector3.zero;
                        this.equippedWeapon.transform.localRotation = Quaternion.Euler(0,0,90);
                    }
                }
            }else{
                if(hand.transform.parent.GetComponent<Animation>().clip == hand.transform.parent.GetComponent<Animation>().GetClip("HandClose")){
                    hand.transform.parent.GetComponent<Animation>().Stop();
                    hand.transform.parent.GetComponent<Animation>().clip = hand.transform.parent.GetComponent<Animation>().GetClip("HandOpen");
                    hand.transform.parent.GetComponent<Animation>().Play();
                    if(this.equippedWeapon!=null){
                        this.equippedWeapon.transform.GetComponent<Rigidbody>().isKinematic = false;
                        this.equippedWeapon.transform.parent = null;
                        this.equippedWeapon = null;
                    }
                }
            }
        }else{
            
            trackingSpaceRot = trackingSpace.eulerAngles;

                //sword movement
            //sword.transform.eulerAngles = new Vector3(controller.eulerAngles.x,head.eulerAngles.y+controller.eulerAngles.y,sword.transform.eulerAngles.z);
            //handle.transform.eulerAngles = new Vector3(handle.transform.eulerAngles.x,handle.transform.eulerAngles.y,controller.rotation.eulerAngles.z);
                //sword.transform.rotation = new Quaternion(0,0,emulatedControllerRotZ,0);
            // sword.transform.rotation = Quaternion.Euler(emulatedControllerRotX,emulatedControllerRotY,0); 
                //sword2.transform.localRotation = Quaternion.Euler(sword2.transform.localRotation.x,0,emulatedControllerRotZ);
                //sword.transform.Rotate(emulatedControllerRotX,emulatedControllerRotY,emulatedControllerRotZ);
            if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)){
                if(hand.transform.parent.GetComponent<Animation>().clip == hand.transform.parent.GetComponent<Animation>().GetClip("HandOpen")){
                    hand.transform.parent.GetComponent<Animation>().Stop();
                    hand.transform.parent.GetComponent<Animation>().clip = hand.transform.parent.GetComponent<Animation>().GetClip("HandClose");
                    hand.transform.parent.GetComponent<Animation>().Play(); 
                }
                if(hand.GetComponent<CollisionDetector>().colliding&&this.equippedWeapon==null){
                    if(hand.GetComponent<CollisionDetector>().GetCollidedObject().transform.gameObject!=equippedWeapon&&hand.GetComponent<CollisionDetector>().GetCollidedObject().transform.tag=="Weapon"){
                        hand.GetComponent<CollisionDetector>().GetCollidedObject().GetComponent<Rigidbody>().isKinematic = true;
                        this.equippedWeapon = hand.GetComponent<CollisionDetector>().GetCollidedObject().transform.gameObject;
                        
                        this.equippedWeapon.transform.parent = gripPoint.transform;
                        this.equippedWeapon.transform.localPosition = Vector3.zero;
                        this.equippedWeapon.transform.localRotation = Quaternion.Euler(0,0,90); 
                    }
                }
            }else{
                if(hand.transform.parent.GetComponent<Animation>().clip == hand.transform.parent.GetComponent<Animation>().GetClip("HandClose")){
                    hand.transform.parent.GetComponent<Animation>().Stop();
                    hand.transform.parent.GetComponent<Animation>().clip = hand.transform.parent.GetComponent<Animation>().GetClip("HandOpen");
                    hand.transform.parent.GetComponent<Animation>().Play();
                    if(this.equippedWeapon!=null){
                        this.equippedWeapon.transform.GetComponent<Rigidbody>().isKinematic = false;
                        this.equippedWeapon.transform.parent = null; 
                        this.equippedWeapon = null;
                    }
                }
            }
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
