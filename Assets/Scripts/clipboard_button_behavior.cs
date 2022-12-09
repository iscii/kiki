using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class clipboard_button_behavior : MonoBehaviour
{
    private game_controller gc;
    [SerializeField]
    private GameObject truck;
    [TextArea]
    private string todo_list;   //todo list will be rebuilt every time a waste is picked 
                                //up or dropped off (function in slot_b). iterate thru waste_arr to build.
    private TextMeshProUGUI clipboard_text;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("game").GetComponent<game_controller>();
        clipboard_text = gc.clipboard.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        compile_text();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click(){
        if(gc.ui_active){
            Debug.Log("Another UI is currently active");
            return;
        }
        compile_text();
        clipboard_text.text = todo_list;
        gc.close_button.GetComponent<close_button_behavior>().cur_popup = gc.clipboard;
        
        gc.clipboard.SetActive(!gc.clipboard.activeSelf);
        gc.close_button.SetActive(true);
        gc.ui_active = true;
    }
    void compile_text(){
        todo_list = "";
        if(gc.waste_arr.Count > 0){
            todo_list = "<u>Items:</u> <br>";
        }
        //if in truck, strikethrough
        foreach(GameObject waste in gc.waste_arr){
            // Debug.Log(waste.name + " " + dropped.Contains(waste));
            todo_list += waste.name + "<br>";
        }
        todo_list += "<br>";
        foreach(Transform child in truck.GetComponentsInChildren<Transform>()){
            if(child.gameObject.name == truck.name) continue;
            todo_list += "<s>" + child.gameObject.name + "</s>" + "<br>";
        }
        
    }
}
