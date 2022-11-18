using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot_behavior : MonoBehaviour
{
    private game_controller gc;
    private CircleCollider2D player_collider;
    private GameObject my_waste;
    public string size;
    [SerializeField] private bool in_range;
    private Color startcolor;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        player_collider = GameObject.Find("player").GetComponent<CircleCollider2D>();
        my_waste = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }   


    // Fires once when mouse is over collider
    private void OnMouseEnter() {
        Cursor.SetCursor(gc.cursor_hand, new Vector2(gc.cursor_hand.width/2, gc.cursor_hand.height/10), CursorMode.Auto);
        if(in_range)
            my_waste.GetComponent<Renderer>().material.color = Color.yellow;
    }

    // Fires once when mouse exits collider
    private void OnMouseExit() {
        Cursor.SetCursor(gc.cursor_pointer, new Vector2(gc.cursor_pointer.width/2, gc.cursor_pointer.height/10), CursorMode.Auto);
        if(in_range)
            my_waste.GetComponent<Renderer>().material.color = startcolor; 
    }

    // Fires once when mouse clicks on collider
    private void OnMouseDown(){
        if (in_range){  
            //TODO: instead, just remove the renderer sprite and boxcollider. keep the renderer component
            //TODO: make parent player
            Destroy(my_waste.GetComponent<Renderer>());
            Destroy(my_waste.GetComponent<BoxCollider2D>()); 
        }
    }

    // Fires once when a collider enters this object's collider
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name.Equals("player")){
            in_range = true;
            //FIXME: instead, check if mouse is over
            if(my_waste.GetComponent<Renderer>().material.color == startcolor){
                my_waste.GetComponent<Renderer>().material.color = Color.yellow; 
            }
        }
    }

    // Fires once when a collider exits this object's collider
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.name.Equals("player")){
            in_range = false;
            //FIXME: instead, check if mouse is over
            //if you move out of reach of waste, and if ur still hovering over it, unhighlight it
            if(my_waste.GetComponent<Renderer>().material.color == Color.yellow){
                my_waste.GetComponent<Renderer>().material.color = startcolor; 
            }
        }
    }
}
