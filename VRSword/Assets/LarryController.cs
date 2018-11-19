using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarryController : MonoBehaviour
{

    public bool playHitAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playHitAnim){
            if(GetComponent<Animation>().isPlaying){
                return;
            } 
            GetComponent<Animation>().Play();
            playHitAnim = false;
        }
    }

    void OnCollisionEnter(Collision collision){
        Debug.Log(collision.gameObject.name);
        playHitAnim = true;
    }
}
