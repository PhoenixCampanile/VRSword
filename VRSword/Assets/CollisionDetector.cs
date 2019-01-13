using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{


    public bool colliding = false;
    public GameObject colobj; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision){
        this.colliding = true;
        this.colobj = collision.gameObject;
    }

    void OnCollisionExit(Collision collision){
        this.colliding = false;
        this.colobj = null;
    }

    public GameObject GetCollidedObject(){
        return this.colobj;
    }
}
