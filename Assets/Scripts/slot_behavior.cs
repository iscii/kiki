using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot_behavior : MonoBehaviour
{
    private game_controller gc;
    private GameObject player, my_waste;
    public int size;
    [SerializeField] private bool in_range, mouse_over;
    private Color startcolor;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        player = GameObject.Find("player");
        startcolor = gameObject.GetComponent<Renderer>().material.color;
        my_waste = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Fires once when mouse is over collider
    private void OnMouseEnter()
    {
        // Debug.Log("enter");
        Cursor.SetCursor(gc.cursor_hand, new Vector2(gc.cursor_hand.width / 2, gc.cursor_hand.height / 10), CursorMode.Auto);
        mouse_over = true;
        if (in_range)
            my_waste.GetComponent<Renderer>().material.color = Color.yellow;
    }

    // Fires once when mouse exits collider
    private void OnMouseExit()
    {
        // Debug.Log("exit");
        Cursor.SetCursor(gc.cursor_pointer, new Vector2(gc.cursor_pointer.width / 2, gc.cursor_pointer.height / 10), CursorMode.Auto);
        mouse_over = false;
        if (in_range)
            my_waste.GetComponent<Renderer>().material.color = startcolor;
    }

    // Fires once when mouse clicks on collider
    private void OnMouseDown()
    {
        // Debug.Log("click");
        if(player.transform.childCount > 0) 
        {
            Debug.Log("You're holding something already!");
            return; 
        }
        if (in_range && my_waste.GetComponent<Renderer>().enabled)  //we'll use the renderer component to check if the waste is in hand or not
        {
            Debug.Log("You've picked up a(n) " + my_waste.name);
            my_waste.GetComponent<Renderer>().enabled = false;
            my_waste.transform.parent = player.transform;   //make the player the waste's parent
        }
    }

    // Fires once when a collider enters this object's collider
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("collision enter");
        if (!in_range && other.gameObject.name.Equals("player"))
        {
            in_range = true;
            //if you move out of reach of waste, and if ur still hovering over it, unhighlight it
            if (mouse_over && my_waste.GetComponent<Renderer>().enabled)
            {
                my_waste.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }

    // Fires once when a collider exits this object's collider
    private void OnCollisionExit2D(Collision2D other)
    {
        // Debug.Log("collision exit");
        
        //check if collision is player and player is completely out of range
        if (in_range && other.gameObject.name.Equals("player") &&
            !other.gameObject.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>()))
        {
            in_range = false;
            //if you move out of reach of waste, and if ur still hovering over it, unhighlight it
            if (mouse_over && my_waste.GetComponent<Renderer>().enabled)
            {
                my_waste.GetComponent<Renderer>().material.color = startcolor;
            }
        }
    }
}
