using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_controller : MonoBehaviour
{
    //inspector references
    [SerializeField] private GameObject waste_prefab;     //prefab reference, public so we can drag the prefab into inspector
    [SerializeField] private Texture2D cursor_pointer;
    [SerializeField] private Texture2D cursor_hand;
    //game variables
    public Vector2 map_border;
    public Vector3 mouse_world_pos;  // position of the mouse relative to world coordinates
    [HideInInspector]   //waste is public for other scripts to reference, but we don't need it to clutter the inspector. it'll be dynamically instantiated.
    public GameObject waste;    //waste object returned from instantiation. public so we can reference from player behavior
    public ArrayList waste_arr = new ArrayList();
    private GameObject map;


    //variable spawnrate

    // Start is called before the first frame update
    void Start()
    {        
        //instead of doing this, use a sprite.
        Cursor.SetCursor(cursor_pointer, new Vector2(cursor_pointer.width/2, cursor_pointer.height/10), CursorMode.Auto);

        map = GameObject.Find("pfc_map");
        map_border = map.transform.localScale / 2;  //gets x and y of map scale /2 and puts it into vector2 map_border as offsets

        waste = Instantiate<GameObject>(waste_prefab, new Vector3(0, 3.5f, 0), Quaternion.identity);
        waste_arr.Add(waste);
    }

    // Update is called once per frame
    void Update()
    {
        mouse_world_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_world_pos.z = 0;  // zero out the z component bc 2d game
    }

    
}
