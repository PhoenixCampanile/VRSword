using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Vector3 rot;
    public Vector3 locrot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //locrot = transform.eulerAngles;//new Quaternion(transform.localRotation.x*Mathf.Rad2Deg,transform.localRotation.y*Mathf.Rad2Deg,transform.localRotation.z*Mathf.Rad2Deg,transform.localRotation.w*Mathf.Rad2Deg);
          transform.rotation = Quaternion.Euler(locrot.x,locrot.y,locrot.z);
          rot = transform.eulerAngles;
    }
}
