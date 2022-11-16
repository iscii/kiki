using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_controller : MonoBehaviour
{
    Bounds bounds;
    float top;
    float bottom;
    float left;
    float right;
    
    // Start is called before the first frame update
    void Start()
    {
        bounds = gameObject.GetComponent<SpriteRenderer>().bounds; // if map bounds will change might move this out of Start()
        top = bounds.max.y;
        bottom = bounds.min.y;
        left = bounds.min.x;
        right = bounds.max.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
