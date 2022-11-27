using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truck_behavior : MonoBehaviour
{
    private GameObject player, my_waste;
    private game_controller gc;
    [SerializeField] private bool in_range, mouse_over;
    private Color startcolor;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        player = GameObject.Find("player");
        startcolor = gameObject.GetComponent<Renderer>().material.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter() {
        Debug.Log("enter");
    }
}
