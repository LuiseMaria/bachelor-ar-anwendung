using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonComponent : MonoBehaviour {

    public GameObject gameObjectButtonList;

    public GameObject gameObjectRingList;

    private Button[] labelButtonList;

    private Button[] ringButtonList;

    bool isActive = true;

    Button selectedButton;

    // Start is called before the first frame update
    void Start() {
        ringButtonList = gameObjectRingList.GetComponentsInChildren<Button>();
        labelButtonList = gameObjectButtonList.GetComponentsInChildren<Button>();
        for(int i = 0; i < labelButtonList.Length; i++){
            if(labelButtonList[i] != null){
                AddButtonListener(labelButtonList[i], i);
            }
        }
        // bei Start Ringe deaktivieren
           foreach (Button btn in ringButtonList) {
            btn.gameObject.SetActive(false);
         }
    }


 private void AddButtonListener(Button button, int index) {
    button.onClick.AddListener( () => {
        // if(selectedButton == null){
        //     selectedButton = button;
        //     // disable other buttons
        // } else {
        //     selectedButton = null;
        //     // enable other buttons
        // }
        if(labelButtonList[index] != null) {
            DisableButtons(index);
            ToggleButton(index);
        }
    });
 }

// Alle Buttons deaktiviert
 private void DisableButtons(int index) {
    for(int buttonIndex = 0; buttonIndex < labelButtonList.Length; ++buttonIndex ){
            labelButtonList[buttonIndex].gameObject.SetActive(!isActive);
    }
    isActive = !isActive;
 }
    // Update is called once per frame
    void Update()
    {
        
    }

    // angeklickter Button mit index getooglet
    public void ToggleButton(int index) {
        Debug.Log("Click" + index + isActive);
        if(index >= 0 && index < labelButtonList.Length && !isActive) {
            labelButtonList[index].gameObject.SetActive(!labelButtonList[index].gameObject.activeSelf); 
            ringButtonList[index].gameObject.SetActive(!ringButtonList[index].gameObject.activeSelf); 
        } else {
            ringButtonList[index].gameObject.SetActive(!ringButtonList[index].gameObject.activeSelf); 
        }
    }
}
