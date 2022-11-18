using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waste_behavior : MonoBehaviour
{
    //references
    private game_controller gc;
    //gameObject variables  
    public string size, desc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
