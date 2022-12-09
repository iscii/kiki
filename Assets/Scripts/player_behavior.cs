using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_behavior : MonoBehaviour
{
    public Animator animator;
    // [SerializeField] private Vector3 pos;
    private Quaternion rot;     // player rotation
    private game_controller gc;    // to access game_controller values
    private CapsuleCollider2D hitbox;     // player hitbox
    private float player_width;
    private float player_height;
    private CircleCollider2D interact_box;      //interaction box
    [SerializeField] private Vector3 velocity;       //shows var (speed) in inspector so we can modify it but keeps it private
    private float speed;
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
        speed = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        rot = Quaternion.Euler(0, 0, Mathf.Atan2(gc.mouse_world_pos.y-transform.position.y, gc.mouse_world_pos.x-transform.position.x)*Mathf.Rad2Deg-90);

        // transform.position = pos;
        transform.rotation = rot;
    }

    void FixedUpdate() {
        Vector2 velocity = new Vector2(0, 0);
        if(gc.ui_active) {
            GetComponent<Rigidbody2D>().velocity = velocity*Time.deltaTime;
            return;
        }
        if (Input.GetKey("w")){
            velocity.y += speed;
        }
        if (Input.GetKey("s")){
            velocity.y += -speed;
        }
        if (Input.GetKey("d")){
            velocity.x += speed;
        }
        if (Input.GetKey("a")){
            velocity.x += -speed;
        }
        GetComponent<Rigidbody2D>().velocity = velocity*Time.deltaTime;
    }

    public void pickup_state(bool holding){
        animator.SetBool("is_holding", holding);
    }
}
