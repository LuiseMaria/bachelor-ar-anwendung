using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonComponent : MonoBehaviour {
    
    private Transform buttonParent;

    [SerializeField]
    private Button highlightedRing;

    private bool isActive = true;

    private int indexOfLabelButton = 0;

    private int indexOfBackButton = 1;

    private int indexOfModalButton = 2;

    private Button backButton;

    private Button labelButton;

    private int childCount;


    // public float approachSpeed = 0.02f;
    // public float growthBound = 1f;
    // public float shrinkBound = 0.5f;
    // private float currentRatio = 1f;

    // private Coroutine routine;
    // private bool keepGoing = false;

    void Start() {
        childCount = gameObject.transform.childCount;
        for(int i = 0; i < childCount; i++){
                buttonParent = gameObject.transform.GetChild(i);
                labelButton = buttonParent.GetChild(indexOfLabelButton).GetComponent<Button>();
                backButton = buttonParent.GetChild(indexOfBackButton).GetComponent<Button>();
                AddButtonListener(labelButton, i);
                backButton.onClick.AddListener( () => {
                    DisableButtons(true, buttonParent);
                    highlightedRing.gameObject.SetActive(!highlightedRing.gameObject.activeSelf); 
                    if(highlightedRing.transform.parent.childCount > 2){
                        DestroyImmediate(highlightedRing.transform.parent.GetChild(2).gameObject);
                    }
                });
                backButton.gameObject.SetActive(false);
        }
        
    }

    private void AddButtonListener(Button button, int index) {
        button.onClick.AddListener( () => {
            buttonParent =  gameObject.transform.GetChild(index);
            DisableButtons(false, buttonParent);
            ToggleButton(buttonParent, index);
            handleRingSelection(buttonParent.name);
        });
 }

    // ALLE Buttons deaktiviert/aktiviert
    private void DisableButtons(bool enable, Transform buttonParent) {
        buttonParent.GetChild(indexOfBackButton).gameObject.SetActive(!enable);
        buttonParent.GetChild(indexOfModalButton).gameObject.SetActive(!enable);
        for(int buttonIndex = 0; buttonIndex < childCount; ++buttonIndex ){
            Debug.Log("buttonIndex: " + buttonIndex + " buttonGO.gameObject.activeSelf: " + buttonParent.gameObject.activeSelf + " enable: " + enable);
            gameObject.transform.GetChild(buttonIndex).gameObject.SetActive(enable);
        }
        isActive = !isActive;
    }
   
//    // ALLE Buttons deaktiviert/aktiviert
//     private void DisableButtons() {
//         for(int buttonIndex = 0; buttonIndex < childCount; ++buttonIndex ){
//             gameObject.transform.GetChild(buttonIndex).gameObject.SetActive(!isActive);
//         }
//         isActive = !isActive;
//     }

    //nur angeklickter Button
    private void ToggleButton(Transform buttonGO, int index) {
        if(!isActive) {
          //  buttonGO.GetChild(indexOfBackButton).gameObject.SetActive(true);
            buttonGO.gameObject.SetActive(!buttonGO.gameObject.activeSelf);
            Debug.Log("Toggle Button " + buttonGO.name + " " + buttonGO.gameObject.activeSelf);
        }
    }


    void handleRingSelection(string selectedButtonName){
        Button copy;
        highlightedRing.gameObject.SetActive(!highlightedRing.gameObject.activeSelf); 
        if(selectedButtonName == "SphinxGriffin"){
            highlightedRing.transform.localScale = new Vector3(2f, 2f, 2f);
        } else if(selectedButtonName == "PlanetenKrieger"){
            highlightedRing.transform.localScale = new Vector3(4f, 4f, 4f);
        } else if(selectedButtonName == "Ornamente"){
            highlightedRing.transform.localScale = new Vector3(5.3f, 5.3f, 5.3f);
        } else if(selectedButtonName == "HofSzenen"){
            highlightedRing.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
            copy = Instantiate(highlightedRing, highlightedRing.transform.parent);
            copy.transform.localScale = new Vector3(5.5f, 5.5f, 5.5f);
        } else {
             highlightedRing.transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }


    // IEnumerator Pulse(Button button) {
    //      // Run this indefinitely
    //      while (keepGoing) {
    //             Debug.Log(keepGoing + " keepGoing");
    //         // for(int i = 0; i < gameObject.transform.childCount; i++){
    //             // buttonParent = gameObject.transform.GetChild(i);

    //          // Get bigger for a few seconds
    //          while (this.currentRatio != this.growthBound) {
    //              // Determine the new ratio to use
    //              currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);
 
    //              // Update our text element
    //              if(button.gameObject.activeSelf){
    //                 button.transform.localScale = Vector3.one * currentRatio;
    //              }

    //              yield return new WaitForEndOfFrame();
    //          }
 
    //          // Shrink for a few seconds
    //          while (this.currentRatio != this.shrinkBound) {
    //              // Determine the new ratio to use
    //              currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);
 
    //              // Update our text element
    //             if(button.gameObject.activeSelf){
    //                button.transform.localScale = Vector3.one * currentRatio;
    //             }
    //              yield return new WaitForEndOfFrame();
    //          }
    //     //  }
    //      }
    //  }

}
