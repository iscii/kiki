using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_behavior : MonoBehaviour
{
    [SerializeField] private Vector3 pos;
    private Quaternion rot;     // player rotation
    private game_controller gc;    // to access game_controller values
    private CapsuleCollider2D hitbox;     // player hitbox
    private float player_width;
    private float player_height;
    private CircleCollider2D interact_box;      //interaction box
    [SerializeField] private float speed;       //shows var (speed) in inspector so we can modify it but keeps it private
    private GameObject waste;
    // Start is called before the first frame update
    void Start()
    {
        //references
        gc = GameObject.Find("game").GetComponent<game_controller>();
        waste = gc.waste;   //reference waste object from game_controller script (there'll only be one bc it's under the camera)
        hitbox = gameObject.GetComponent<CapsuleCollider2D>();
        player_width = hitbox.size.x;
        player_height = hitbox.size.y;
        interact_box = gameObject.GetComponent<CircleCollider2D>();     

        //gameObject variables
        speed = 6f;
        pos = new Vector3(0, 0, 0);
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
        if (Input.GetMouseButtonDown(0)){
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit)){
                if (raycastHit.transform == waste){
                    Destroy(waste);
                }
            }
        }

        rot = Quaternion.Euler(0, 0, Mathf.Atan2(gc.mouse_world_pos.y-pos.y, gc.mouse_world_pos.x-pos.x)*Mathf.Rad2Deg-90);

        // clamp the player's position so they can't leave the map
        pos.x = Mathf.Clamp(pos.x, (gc.map_border.x - player_width/2)*-1, gc.map_border.x - player_width/2);
        pos.y = Mathf.Clamp(pos.y, (gc.map_border.y - player_height/2)*-1, gc.map_border.y - player_height/2);

        transform.position = pos;
        transform.rotation = rot;
    }
    // void OnMouseDown() {
    //     Destroy(waste);
    // }
}
