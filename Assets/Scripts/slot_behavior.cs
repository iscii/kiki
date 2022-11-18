using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot_behavior : MonoBehaviour
{
    private game_controller gc;
    private CircleCollider2D player_collider;
    private GameObject my_waste;
    public string size;
    [SerializeField] private bool in_range, mouse_over;
    private Color startcolor;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        startcolor = gameObject.GetComponent<Renderer>().material.color;
        player_collider = GameObject.Find("player").GetComponent<CircleCollider2D>();
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
        if (in_range && my_waste.GetComponent<Renderer>().enabled)  //we'll use the renderer component to check if the waste is in hand or not
        {
            //TODO: make parent player
            my_waste.GetComponent<Renderer>().enabled = false;
        }
    }

    // Fires once when a collider enters this object's collider
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("in range");
        if (!in_range && other.gameObject.name.Equals("player"))
        {
            in_range = true;
            //FIXME: instead, check if mouse is over
            if (mouse_over && my_waste.GetComponent<Renderer>().enabled)
            {
                my_waste.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }

    // Fires once when a collider exits this object's collider
    private void OnCollisionExit2D(Collision2D other)
    {
        // Debug.Log("out of range");
        if (in_range && other.gameObject.name.Equals("player"))
        {
            in_range = false;
            //FIXME: instead, check if mouse is over
            //if you move out of reach of waste, and if ur still hovering over it, unhighlight it
            if (mouse_over && my_waste.GetComponent<Renderer>().enabled)
            {
                my_waste.GetComponent<Renderer>().material.color = startcolor;
            }
        }
    }
}
