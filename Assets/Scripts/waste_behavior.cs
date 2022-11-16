using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waste_behavior : MonoBehaviour
{
    //references
    private game_controller gc;
    private CircleCollider2D player_collider;
    //gameObject variables
    public string desc;
    private BoxCollider2D col;
    [SerializeField] private bool in_range;
    [SerializeField] private bool clicked;
    [SerializeField] private bool mouse;
    public bool in_hand, in_truck;
    private Color startcolor;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        player_collider = GameObject.Find("player").GetComponent<CircleCollider2D>();
        col = gameObject.GetComponent<BoxCollider2D>();
        clicked = false;
        startcolor = gameObject.GetComponent<Renderer>().material.color;
        mouse = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseEnter() {
        // Cursor.SetCursor(gc.cursorHand, new Vector2(gc.cursorHand.width/2, gc.cursorHand.height/10), CursorMode.Auto);
        mouse = true;
        if (in_range && !clicked){
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    private void OnMouseExit() {
        // Cursor.SetCursor(gc.cursorPointer, new Vector2(gc.cursorPointer.width/2, gc.cursorPointer.height/10), CursorMode.Auto);
            mouse = false;
            if (!clicked){
                gameObject.GetComponent<Renderer>().material.color = startcolor; 
            }
    }

    private void OnMouseDown(){
        if (in_range){
            clicked = true;
            Destroy(gameObject.GetComponent<Renderer>());
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "player"){
            in_range = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.name == "player"){
            in_range = false;
        }
    }
}
