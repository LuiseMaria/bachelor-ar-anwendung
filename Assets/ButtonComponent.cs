using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonComponent : MonoBehaviour {
    
    private Transform buttonParent;

    [SerializeField]
    private Button highlightedRing;

    private Button highlightedRingInside;

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
        highlightedRingInside = Instantiate(highlightedRing, highlightedRing.transform.parent);
        childCount = gameObject.transform.childCount;
        for(int i = 0; i < childCount; i++){
            buttonParent = gameObject.transform.GetChild(i);
            labelButton = buttonParent.GetChild(indexOfLabelButton).GetComponent<Button>();
            backButton = buttonParent.GetChild(indexOfBackButton).GetComponent<Button>();
            AddButtonListener(labelButton, i);
            AddButtonListenerReturnButton(buttonParent);
            backButton.gameObject.SetActive(false);
        }
        
    }

    private void AddButtonListener(Button button, int index) {
        button.onClick.AddListener( () => {
            buttonParent =  gameObject.transform.GetChild(index);
            if(isActive) {
                DisableButtons(false, buttonParent);
                ToggleButton(buttonParent, index);
                handleRingSelection(index);
            }
        });
    }
    
    private void AddButtonListenerReturnButton(Transform buttonParent) {
        backButton.onClick.AddListener( () => {
            DisableButtons(true, buttonParent);
            highlightedRing.gameObject.SetActive(false);
            highlightedRingInside.gameObject.SetActive(false);
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

    //nur angeklickter Button
    private void ToggleButton(Transform buttonGO, int index) {
        if(!isActive) {
          //  buttonGO.GetChild(indexOfBackButton).gameObject.SetActive(true);
            buttonGO.gameObject.SetActive(!buttonGO.gameObject.activeSelf);
            Debug.Log("Toggle Button " + buttonGO.name + " " + buttonGO.gameObject.activeSelf);
        }
    }

    private void handleRingSelection(int indexOfRing){
        highlightedRingInside.gameObject.SetActive(!highlightedRing.gameObject.activeSelf);
        highlightedRing.gameObject.SetActive(!highlightedRing.gameObject.activeSelf); 
        if(indexOfRing == 1){ //Planeten&Krieger
            highlightedRing.transform.localScale = new Vector3(4.3f, 4.3f, 4.3f);
            highlightedRingInside.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        } else if(indexOfRing == 2){ //Ornamente
            highlightedRing.transform.localScale = new Vector3(5.4f, 5.4f, 5.4f);
            highlightedRingInside.transform.localScale = new Vector3(4.1f, 4.1f, 4.1f);
        } else if(indexOfRing == 3){ //HöfischeSzenen
            highlightedRing.transform.localScale = new Vector3(7f, 7f, 7f);
            highlightedRingInside.transform.localScale = new Vector3(5.5f, 5.5f, 5.5f);
        } else {
            highlightedRingInside.gameObject.SetActive(false);
            highlightedRing.transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }

}
