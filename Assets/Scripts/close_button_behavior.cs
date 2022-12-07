using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_button_behavior : MonoBehaviour
{
    private game_controller gc;

    public GameObject cur_popup;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();

        //on click, set state of assigned gameobject to inactive
        //set gameobject reference to whichever ui element is shown when... a ui element is shown. 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void close(){
        cur_popup.SetActive(false);
        gameObject.SetActive(false);
        gc.ui_active = false;
    }
}
