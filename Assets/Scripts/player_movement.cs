using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Vector3 pos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(0, 0, 0);
        speed = 5f;
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
