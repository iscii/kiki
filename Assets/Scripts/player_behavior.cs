using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_behavior : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField]    //shows var (speed) in inspector so we can modify it but keeps it private
    private float speed;
    private GameObject waste;
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(0, 0, 0);
        speed = 2f;
        waste = GameObject.Find("Main Camera").GetComponent<game_controller>().waste;   //reference waste object from game_controller script (there'll only be one bc it's under the camera)
    }

    // Update is called once per frame
    void Update()
    {
        float update_constant = speed*Time.deltaTime;
        if (Input.GetKey("w")){
            pos.y += update_constant;
        } 
        if (Input.GetKey("s")){
            pos.y -= update_constant;
        } 
        if (Input.GetKey("a")){
            pos.x -= update_constant;
        }
        if(Input.GetKey("d")){  
            pos.x += update_constant;
        }
        transform.position = pos;
    }
}
