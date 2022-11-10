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
    private bool in_range;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        player_collider = GameObject.Find("player").GetComponent<CircleCollider2D>();
        col = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private void OnMouseEnter() {
        // Cursor.SetCursor(gc.cursorHand, new Vector2(gc.cursorHand.width/2, gc.cursorHand.height/10), CursorMode.Auto);
    }

    private void OnMouseExit() {
        // Cursor.SetCursor(gc.cursorPointer, new Vector2(gc.cursorPointer.width/2, gc.cursorPointer.height/10), CursorMode.Auto);
    }

    private void OnMouseOver(){
        if (Input.GetMouseButtonDown(0)){
            //!Issue -> only works for the the bounds of the collider, not when inside collider.
            //Fix -> use OnCollisionEnter to set a flag in_range to true. 
                //then on mouse button down, if in_range, we can destroy.
                //would also allow us to easily deal with highlighting waste in range of player
            if (col.bounds.Intersects(player_collider.bounds)){
                //remember to destroy waste colliders as well
                Destroy(gameObject.GetComponent<Renderer>());
            }
            
        }
    }
    private void OnCollisionEnter(Collision other) {
        
    }
}
