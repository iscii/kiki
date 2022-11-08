using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_controller : MonoBehaviour
{
    public GameObject waste_prefab;     //prefab reference, public so we can drag the prefab into inspector
    [HideInInspector]   //waste is public for other scripts to reference, but we don't need it to clutter the inspector. it'll be dynamically instantiated.
    public GameObject waste;    //waste object returned from instantiation. public so we can reference from player behavior

    // Start is called before the first frame update
    void Start()
    {
        waste = Instantiate<GameObject>(waste_prefab, new Vector3(0, 3.5f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
