using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationClamp : MonoBehaviour
{

    [Range(0,360)]
    public float XMax;
    
    [Range(0,360)]
    public float XMin; 
    
    [Range(0,360)]
    public float YMax;
    [Range(0,360)]
    public float YMin;
    [Range(0,360)]
    public float ZMax;
    [Range(0,360)]
    public float ZMin; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, XMin, XMax),Mathf.Clamp(transform.eulerAngles.y, YMin, YMax),Mathf.Clamp(transform.eulerAngles.z, ZMin, ZMax));
    }
}
