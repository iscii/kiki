using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot_behavior : MonoBehaviour
{
    private game_controller gc;
    private CircleCollider2D player_collider;
    public string size;
    [SerializeField] private bool in_range;
    // [SerializeField] private bool clicked, mouse;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        player_collider = GameObject.Find("player").GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }   


    private void OnMouseOver() {
        Debug.Log("over");
        Cursor.SetCursor(gc.cursor_hand, new Vector2(gc.cursor_hand.width/2, gc.cursor_hand.height/10), CursorMode.Auto);
    }

    private void OnMouseExit() {
        Debug.Log("out");
        Cursor.SetCursor(gc.cursor_pointer, new Vector2(gc.cursor_pointer.width/2, gc.cursor_pointer.height/10), CursorMode.Auto);
    }

    private void OnMouseDown(){
        Debug.Log("click");
        if (in_range){  

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name.Equals("player")){
            in_range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.name.Equals("player")){
            in_range = false;
        }
    }
}
