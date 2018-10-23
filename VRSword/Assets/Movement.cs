
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Text uiText;
    public float speed;
    public float rads,degs;
    [Range(-1,1)]
    public float touchX;
    [Range(-1,1)]
    public float touchY;
    public Vector2 emulatedtouchpos;
    public Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Test to see if program is in editor
        if(Application.isEditor){
            emulatedtouchpos = new Vector2(touchX,touchY);
            if(emulatedtouchpos!=Vector2.zero){
                rads = Mathf.Atan2(emulatedtouchpos.y,emulatedtouchpos.x);
                degs = rads*Mathf.Rad2Deg;
                velocity = new Vector3(Mathf.Cos(rads),0,Mathf.Sin(rads));
                transform.Translate((velocity/100)*Vector2.Distance(Vector2.zero,emulatedtouchpos)*speed);
            }
        }else{
            //gets the finger position on the gearvr controller touchpad
            Vector2 touches = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote);
            //if not touching the pad we dont want to move
            if(touches!=Vector2.zero){
                //atan2 gets the angle in radians from (0,0) to (x,y) in a circle
                rads = Mathf.Atan2(touches.y,touches.x);
                degs = rads*Mathf.Rad2Deg;
                velocity = new Vector3(Mathf.Cos(rads),0,Mathf.Sin(rads));
                transform.Translate((velocity/100)*Vector2.Distance(Vector2.zero,touches)*speed*5);
            }
        }
    }
}
