using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_controller : MonoBehaviour
{
    //inspector references
    [SerializeField] private GameObject waste_prefab;     //prefab reference, public so we can drag the prefab into inspector
    public Texture2D cursor_pointer,cursor_hand;
    //game variables
    public Vector2 map_border;
    public Vector3 mouse_world_pos;  // position of the mouse relative to world coordinates
    [HideInInspector]   //waste is public for other scripts to reference, but we don't need it to clutter the inspector. it'll be dynamically instantiated.
    public GameObject waste;    //waste object returned from instantiation. public so we can reference from player behavior
    public ArrayList waste_arr = new ArrayList();   //global array of waste
    private GameObject map;
    private GameObject slot;
    [SerializeField] public TextAsset waste_info;
    private Wastes pile;
    //ඞ
    //variable spawnrate

    // Start is called before the first frame update
    void Start()
    {        
        //error in parameter here
        pile = JsonUtility.FromJson<Wastes>(waste_info.text);

        //instead of doing this, use a sprite.
        Cursor.SetCursor(cursor_pointer, new Vector2(cursor_pointer.width/2, cursor_pointer.height/10), CursorMode.Auto);

        map = GameObject.Find("pfc_map");
        map_border = map.transform.localScale / 2;  //gets x and y of map scale /2 and puts it into vector2 map_border as offsets

        spawnWaste();
    }

    // Update is called once per frame
    void Update()
    {
        mouse_world_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_world_pos.z = 0;  // zero out the z component bc 2d game
    }

    void spawnWaste(){
        for(int i=0; i<map.transform.childCount;i++){
            //TODO: implement size checking
            
            waste = Instantiate<GameObject>(waste_prefab, new Vector3(0, 3.5f, 0), Quaternion.identity);    //spawn waste
            waste.transform.parent = map.transform.GetChild(i);     //sets waste's parent to slot
            waste_arr.Add(waste);   //maybe we don't need this if we can track everything by children
        }
    }
}