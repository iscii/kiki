using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_behavior : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private Vector3 pos;
    private Quaternion rot;     // player rotation
    private game_controller gc;    // to access game_controller values
    private CapsuleCollider2D hitbox;     // player hitbox
    private float player_width;
    private float player_height;
    private CircleCollider2D interact_box;      //interaction box
    [SerializeField] private Vector3 velocity;       //shows var (speed) in inspector so we can modify it but keeps it private
    private float max_speed;
    private float accel;
    private GameObject waste;
    // [SerializeField] private Sprite kiki_full;
    // [SerializeField] private Sprite kiki_empty;

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
        velocity = new Vector3(0, 0, 0);
        max_speed = 3f;
        accel = 12f;
        pos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: make this movement physics based and put it into FixedUpdate() instead of Update(). This is necessary for wall collision
        
        float update_constant = accel*Time.deltaTime;
        if (Input.GetKey("w") && velocity.y >= 0){
            if (velocity.x == 0){
                velocity.y += update_constant;
            } else {
                velocity.y = Mathf.Abs(velocity.x) + update_constant;
            }
        } 
        else if (Input.GetKey("s") && velocity.y <= 0){
            if (velocity.x == 0){
                velocity.y -= update_constant;
            } else {
                velocity.y = -1*(Mathf.Abs(velocity.x) + update_constant);
            }
        } else {
            velocity.y = 0;
        }
        if (Input.GetKey("a") && velocity.x <= 0){
            if (velocity.y == 0){
                velocity.x -= update_constant;
            } else {
                velocity.x = -1*Mathf.Abs(velocity.y);
            }
        }
        else if(Input.GetKey("d")&& velocity.x >= 0){  
            if (velocity.y == 0) {
                velocity.x += update_constant;
            } else {
                velocity.x = Mathf.Abs(velocity.y);
            }
        } else {
            velocity.x = 0;
        }

        velocity = Vector3.ClampMagnitude(velocity, max_speed);

        pos += velocity*Time.deltaTime;
    
        rot = Quaternion.Euler(0, 0, Mathf.Atan2(gc.mouse_world_pos.y-pos.y, gc.mouse_world_pos.x-pos.x)*Mathf.Rad2Deg-90);

        // clamp the player's position so they can't leave the map
        pos.x = Mathf.Clamp(pos.x, (gc.map_border.x - player_width/2)*-1, gc.map_border.x - player_width/2);
        pos.y = Mathf.Clamp(pos.y, (gc.map_border.y - player_height/2)*-1, gc.map_border.y - player_height/2);

        transform.position = pos;
        transform.rotation = rot;
    }

    void FixedUpdate() {
        // if (Input.GetKey("w")){
        //     GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2);
        // }
    }

    public void pickup_state(bool holding){
        animator.SetBool("is_holding", holding);
    }
}
