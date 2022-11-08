
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_behavior : MonoBehaviour
{
    private GameObject map;
    private GameObject player;
    private GameObject game;
    private Vector2 map_border;
    [SerializeField]
    private Vector2 free_move_border = new Vector2(4f, 2f);    //x, y offsets relative to position

    
    // Start is called before the first frame update
    void Start()
    {    
        game = GameObject.Find("game");
        map_border = game.GetComponent<game_controller>().map_border;
        map = GameObject.Find("pfc_map");
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 player_pos = player.transform.position;
        Vector2 cam_bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0)); //gets right and top bounds of cam (relative to position)   
        float diff_x = cam_bounds.x - pos.x;     //diff b/t right bound and pos
        float diff_y = cam_bounds.y - pos.y;     //diff b/t top bound and pos
        
        Debug.Log("cam bound: " + cam_bounds.x + " map x bound: " + map_border.x + " total: " + (map.transform.position.x - map_border.x));
        if(cam_bounds.x < (map.transform.position.x + map_border.x) && player_pos.x > (pos.x + free_move_border.x)){  //to the right of free border
            pos.x += Mathf.Abs(player_pos.x - (pos.x + free_move_border.x));
        }
        if((cam_bounds.x - 2*diff_x) > (map.transform.position.x - map_border.x) && player_pos.x < (pos.x - free_move_border.x)){  //to the left of free border
            pos.x -= Mathf.Abs(player_pos.x - (pos.x - free_move_border.x));
        }
        if(cam_bounds.y < (map.transform.position.y + map_border.y) && player_pos.y > (pos.y + free_move_border.y)){  //to the top of free border
            pos.y += Mathf.Abs(player_pos.y - (pos.y + free_move_border.y));
        }
        if((cam_bounds.y - 2*diff_y) > (map.transform.position.y - map_border.y) && player_pos.y < (pos.y - free_move_border.y)){  //to the bottom of free border
            pos.y -= Mathf.Abs(player_pos.y - (pos.y - free_move_border.y));
        }
        transform.position = pos;
    }
}
