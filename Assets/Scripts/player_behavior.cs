using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_behavior : MonoBehaviour
{
    [SerializeField] private Vector3 pos;
    private Quaternion rot;     // player rotation
    private GameObject game;
    private game_controller gc;    // to access game_controller values
    private BoxCollider2D hitbox;     // player hitbox
    private float object_width;
    private float object_height;
    [SerializeField]    //shows var (speed) in inspector so we can modify it but keeps it private
    private float speed = 2f;
    private GameObject waste;
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(0, 0, 0);
        game = GameObject.Find("game");
        gc = game.GetComponent<game_controller>();
        hitbox = gameObject.GetComponent<BoxCollider2D>();
        object_width = hitbox.size.x;
        object_height = hitbox.size.y;
        waste = GameObject.Find("main_camera").GetComponent<game_controller>().waste;   //reference waste object from game_controller script (there'll only be one bc it's under the camera)
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

        rot = Quaternion.Euler(0, 0, Mathf.Atan2(gc.mouse_world_pos.y-pos.y, gc.mouse_world_pos.x-pos.x)*Mathf.Rad2Deg-90);

        // clamp the player's position so they can't leave the map
        pos.x = Mathf.Clamp(pos.x, (gc.map_border.x - object_width/2)*-1, gc.map_border.x - object_width/2);
        pos.y = Mathf.Clamp(pos.y, (gc.map_border.y - object_height/2)*-1, gc.map_border.y - object_height/2);

        transform.position = pos;
        transform.rotation = rot;
    }
}
