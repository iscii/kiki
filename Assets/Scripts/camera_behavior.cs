
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_behavior : MonoBehaviour
{
    private Vector3 pos;
    private Vector2 cam_bounds;
    private float diff_x;   //diff b/t right bound and pos
    private float diff_y;   //diff b/t top bound and pos
    private GameObject map;
    private GameObject player;
    private Vector3 player_pos;
    private game_controller gc;
    [SerializeField]
    private Vector2 free_move_border;   //x, y offsets relative to position
    private int cam_mode; // mode 0 -> follow player. mode 1 -> zoom out to reveal entire map
    

    void ShowMap()
    {
        // TODO
    }

    void FollowPlayer()
    {
        if(cam_bounds.x < (map.transform.position.x + gc.map_border.x) && player_pos.x > (pos.x + free_move_border.x)){  //to the right of free border
            pos.x += Mathf.Abs(player_pos.x - (pos.x + free_move_border.x));
        }
        if((cam_bounds.x - 2*diff_x) > (map.transform.position.x - gc.map_border.x) && player_pos.x < (pos.x - free_move_border.x)){  //to the left of free border
            pos.x -= Mathf.Abs(player_pos.x - (pos.x - free_move_border.x));
        }
        if(cam_bounds.y < (map.transform.position.y + gc.map_border.y) && player_pos.y > (pos.y + free_move_border.y)){  //to the top of free border
            pos.y += Mathf.Abs(player_pos.y - (pos.y + free_move_border.y));
        }
        if((cam_bounds.y - 2*diff_y) > (map.transform.position.y - gc.map_border.y) && player_pos.y < (pos.y - free_move_border.y)){  //to the bottom of free border
            pos.y -= Mathf.Abs(player_pos.y - (pos.y - free_move_border.y));
        }
    }

    // Start is called before the first frame update
    void Start()
    {    
        //references
        gc = GameObject.Find("game").GetComponent<game_controller>();
        map = GameObject.Find("pfc_map");
        player = GameObject.Find("player");

        //gameObject variables
        free_move_border = new Vector2(3f, 1.5f);
        cam_mode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        player_pos = player.transform.position;
        cam_bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0)); //gets right and top bounds of cam (relative to position)   
        diff_x = cam_bounds.x - pos.x;     
        diff_y = cam_bounds.y - pos.y;

        if (cam_mode == 0) {
            FollowPlayer();
        } else {
            ShowMap();
        }
        
        transform.position = pos;
    }
}