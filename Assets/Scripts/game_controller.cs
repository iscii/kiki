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
    private GameObject spawner;
    [SerializeField] public TextAsset waste_info;
    private Wastes pile;
    //ඞ
    //variable spawnrate

    // Documentation: Awake is called before start, after all objects are initialized so you can safely speak to other objects
    // Because of this, you should use Awake to set up references between scripts, and use Start() to pass any information back and forth
    // TODO: use awake and start accordingly so we can reduce future bugs in program
    void Awake()
    {        
        //error in parameter here
        pile = JsonUtility.FromJson<Wastes>(waste_info.text);
        pile.accessor = new Waste[3][] {pile.small, pile.medium, pile.big};

        //instead of doing this, use a sprite.
        Cursor.SetCursor(cursor_pointer, new Vector2(cursor_pointer.width/2, cursor_pointer.height/10), CursorMode.Auto);

        map = GameObject.Find("pfc_map");
        map_border = map.transform.localScale / 2;  //gets x and y of map scale /2 and puts it into vector2 map_border as offsets
        spawner = GameObject.Find("spawner");

        spawnWaste();
    }

    // Update is called once per frame
    void Update()
    {
        mouse_world_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_world_pos.z = 0;  // zero out the z component bc 2d game
    }

    void spawnWaste(){
        for(int i=0; i<spawner.transform.childCount;i++){
            Transform slot = spawner.transform.GetChild(i);
            int size = slot.gameObject.GetComponent<slot_behavior>().size;
                
            //spawn waste
            waste = Instantiate<GameObject>(waste_prefab, new Vector3(0, 3.5f, 0), Quaternion.identity);    
            waste.transform.parent = slot;     //sets waste's parent to slot

            // get random object
            int lottery = Random.Range(0, pile.accessor[size].Length);
            Waste winner = pile.accessor[size][lottery];

            // set waste's properties to this object
            Debug.Log(winner.name);
            waste.name = winner.name;
            waste.GetComponent<waste_behavior>().desc = winner.desc;
            waste.GetComponent<waste_behavior>().size = size;
            waste.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Waste/" + winner.name);

            waste.transform.position = waste.transform.parent.position;
            waste_arr.Add(waste);   //maybe we don't need this if we can track everything by children
        }
    }
}﻿