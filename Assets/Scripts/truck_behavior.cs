using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truck_behavior : MonoBehaviour
{
    public Animator animator;
    private GameObject player, my_waste;
    private game_controller gc;
    [SerializeField] private bool in_range, mouse_over;
    private Sprite sprite, sprite_highlighted;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        player = GameObject.Find("player");
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        sprite_highlighted = Resources.Load<Sprite>(gameObject.name + "_highlighted");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter() {
        Cursor.SetCursor(gc.cursor_hand, new Vector2(gc.cursor_hand.width / 2, gc.cursor_hand.height / 10), CursorMode.Auto);
        mouse_over = true;
        if (in_range)
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_highlighted;
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(gc.cursor_pointer, new Vector2(gc.cursor_pointer.width / 2, gc.cursor_pointer.height / 10), CursorMode.Auto);
        mouse_over = false;
        if (in_range)
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    private void OnMouseDown()
    {
        // Debug.Log("click");
        if(player.transform.childCount == 0) 
        {
            Debug.Log("You're not carrying anything!");
            return; 
        }
        if (in_range)  //we'll use the renderer component to check if the waste is in hand or not
        {   
            my_waste = player.transform.GetChild(0).gameObject;
            Debug.Log("You've dropped off a(n) " + my_waste.name);
            my_waste.transform.parent = gameObject.transform;   //make the player the waste's parent
            player.GetComponent<player_behavior>().pickup_state(false);
            gc.waste_arr.Remove(my_waste);
            animator.SetTrigger("place");

            //hide item UI
            gc.holding.SetActive(false);

            if(gc.waste_arr.Count <= 0){
                gc.ui_active = true;
                animator.SetTrigger("drive");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("collision enter");
        if (!in_range && other.gameObject.name.Equals("player"))
        {
            in_range = true;
            //if you move out of reach of waste, and if ur still hovering over it, unhighlight it
            if (mouse_over)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_highlighted;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("collision exit");
        //check if collision is player and player is completely out of range
        if (in_range && other.gameObject.name.Equals("player") &&
            !other.gameObject.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>()))
        {
            in_range = false;
            //if you move out of reach of waste, and if ur still hovering over it, unhighlight it
            if (mouse_over)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
    }

    public void end(){
        gc.end();
    }
}
