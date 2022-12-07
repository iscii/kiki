using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class sign_behavior : MonoBehaviour
{
    private game_controller gc;
    private TextMeshProUGUI sign_text;
    private GameObject player;
    [SerializeField] private bool in_range, mouse_over;
    private Sprite sign_sprite, sign_sprite_highlighted;
    private Color startcolor;
    [TextArea]
    public string desc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        sign_text = gc.sign_popup.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("player");
        startcolor = gameObject.GetComponent<Renderer>().material.color;
        // sign_sprite = Resources.Load<Sprite>("");
        // sign_sprite_highlighted = Resources.Load<Sprite>("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log("mouse enter");

        Cursor.SetCursor(gc.cursor_hand, new Vector2(gc.cursor_hand.width / 2, gc.cursor_hand.height / 10), CursorMode.Auto);
        mouse_over = true;
        if (in_range){
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        //     gameObject.GetComponent<SpriteRenderer>().sprite = ;
    }

    private void OnMouseExit()
    {
        Debug.Log("mouse exit");

        Cursor.SetCursor(gc.cursor_pointer, new Vector2(gc.cursor_pointer.width / 2, gc.cursor_pointer.height / 10), CursorMode.Auto);
        mouse_over = false;
        if (in_range){
            gameObject.GetComponent<Renderer>().material.color = startcolor;
        }
        //     gameObject.GetComponent<SpriteRenderer>().sprite = ;
    }

    private void OnMouseDown()
    {
        Debug.Log("click");

        if(!in_range){
            Debug.Log("You're not in range!");
            return;
        }
        sign_text.text = desc;
        gc.sign_popup.SetActive(!gc.sign_popup.activeSelf);   //toggle active
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision enter");

        if (!in_range && other.gameObject.name.Equals("player"))
        {
            in_range = true;
            //if you move into reach of waste, and if ur still hovering over it, highlight it
            if (mouse_over){
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                // gameObject.GetComponent<SpriteRenderer>().sprite == ; //highlighted sprite
            }   
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("collision exit");
        
        //check if collision is player and player is completely out of range
        if (in_range && other.gameObject.name.Equals("player") &&
            !other.gameObject.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>()))
        {
            in_range = false;
            //if you move out of reach of waste, and if ur still hovering over it, unhighlight it
            if (mouse_over){
                gameObject.GetComponent<Renderer>().material.color = startcolor;
                // gameObject.GetComponent<SpriteRenderer>().sprite = ; //unhighlighted sprite
            }
        }
    }
}
