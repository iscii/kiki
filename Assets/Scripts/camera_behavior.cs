
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_behavior : MonoBehaviour
{
    private GameObject map;   //pfc stands for proof of concept. don't ask me why it seemed appropriate
    private GameObject player;
    [SerializeField]
    private Vector2 free_move_border = new Vector2(2.5f, 1f);    //x, y offsets relative to position
    private Vector2 map_border;
    
    // Start is called before the first frame update
    void Start()
    {    
        map = GameObject.Find("pfc_map");
        player = GameObject.Find("player");

        map_border = map.transform.localScale / 2;  //gets x and y of map scale /2 and puts it into vector2 map_border as offsets
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 player_pos = player.transform.position;
        Vector2 cam_bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0)); //gets x and y bounds of cam (relative to position)   
        
        //!ISUE: if player position is greater (or less than neg of) than border offset AND we aren't at map edges, move the difference (b/t player pos and border)
        //pos.x - cam_bounds.x. same for y. but they aren't working fsr so we'll have to find a better way.
        //this works but is buggy af rn
        Debug.Log("cam x pos: " + pos.x + " cam x bound: " + cam_bounds.x + " total: " + (pos.x-cam_bounds.x));
        if((pos.x + cam_bounds.x) < (map.transform.position.x + map_border.x) && player_pos.x > (pos.x + free_move_border.x)){  //to the right of free border
            pos.x += Mathf.Abs(player_pos.x - (pos.x + free_move_border.x));
        }
        if((pos.x + cam_bounds.x) > (map.transform.position.x - map_border.x) && player_pos.x < (pos.x - free_move_border.x)){  //to the left of free border
            pos.x -= Mathf.Abs(player_pos.x - (pos.x - free_move_border.x));
        }
        if((pos.y + cam_bounds.y) < (map.transform.position.y + map_border.y) && player_pos.y > (pos.y + free_move_border.y)){  //to the top of free border
            pos.y += Mathf.Abs(player_pos.y - (pos.y + free_move_border.y));
        }
        if((pos.y + cam_bounds.y) > (map.transform.position.y - map_border.y) && player_pos.y < (pos.y - free_move_border.y)){  //to the bottom of free border
            pos.y -= Mathf.Abs(player_pos.y - (pos.y - free_move_border.y));
        }
        transform.position = pos;
    }
}
