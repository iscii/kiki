using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor_behavior : MonoBehaviour
{
    //references
    private game_controller gc;
    //inspector references
    public Texture2D cursor_pointer;
    public Texture2D cursor_hand;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        // gameObject.GetComponent<SpriteRenderer>().sprite = cursor_hand;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse_world_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //keeping own local ref bc we're constantly updating this. would rather not pull from game controller every frame update
        transform.position = new Vector3(mouse_world_pos.x, mouse_world_pos.y, -9); //camera is at z=-10, so z=-9 is the highest z-index
    }

    
}
