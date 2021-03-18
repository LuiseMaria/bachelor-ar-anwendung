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

    private Button labelButton;
    
    private Button backButton;

    private Button openModal;

    private Transform Locator;

    private int childCount;

    public bool firstRing = false;
    public bool secondRing = false;
    public bool thirdRing = false;
    public bool fourthRing = false;
    public bool fifthRing = false;


    // public float approachSpeed = 0.02f;
    // public float growthBound = 1f;
    // public float shrinkBound = 0.5f;
    // private float currentRatio = 1f;

    // private Coroutine routine;
    // private bool keepGoing = false;

    void Start() {
        highlightedRingInside = Instantiate(highlightedRing, highlightedRing.transform.parent);
        childCount = gameObject.transform.childCount;
        Locator = GameObject.Find("Locator").GetComponent<Transform>();
        for(int i = 0; i < childCount; i++) {
            buttonParent = gameObject.transform.GetChild(i);
            labelButton = buttonParent.GetChild(indexOfLabelButton).GetComponent<Button>();
            backButton = buttonParent.GetChild(indexOfBackButton).GetComponent<Button>();
            AddButtonListenerLabel(labelButton, i);
            AddButtonListener(buttonParent);
            backButton.gameObject.SetActive(false);
        }
        
    }

    private void AddButtonListenerLabel(Button button, int index) {
        button.onClick.AddListener( () => {
            buttonParent =  gameObject.transform.GetChild(index);
            if(isActive) {
                DisableButtons(false, buttonParent);
                ToggleButton(buttonParent, index);
                handleRingSelection(index);
            }
        });
    }
    
    private void AddButtonListener(Transform buttonParent) {
        backButton.onClick.AddListener( () => {
            DisableButtons(true, buttonParent);
            highlightedRing.transform.parent.gameObject.SetActive(false);
            Locator.gameObject.SetActive(false);
            // highlightedRing.gameObject.SetActive(false);
            // highlightedRingInside.gameObject.SetActive(false);
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
        firstRing = false;
        secondRing = false;
        thirdRing = false;
        fourthRing = false;
        if(indexOfRing == 1){ //Planeten&Krieger
            secondRing = true;
            highlightedRing.image.rectTransform.sizeDelta = new Vector2(0.6f, 0.6f);
            highlightedRingInside.image.rectTransform.sizeDelta = new Vector2(0.35f, 0.35f);
        } else if(indexOfRing == 2){ //Ornamente
            thirdRing = true;
            highlightedRing.image.rectTransform.sizeDelta = new Vector2(0.8f, 0.8f);
            highlightedRingInside.image.rectTransform.sizeDelta = new Vector2(0.6f, 0.6f);
        } else if(indexOfRing == 3){ //HöfischeSzenen
            fourthRing = true;
            highlightedRing.image.rectTransform.sizeDelta = new Vector2(1.1f, 1.1f);
            highlightedRingInside.image.rectTransform.sizeDelta = new Vector2(0.78f, 0.78f);
        } else if(indexOfRing == 0){
            firstRing = true;
            highlightedRingInside.gameObject.SetActive(false);
            highlightedRing.image.rectTransform.sizeDelta = new Vector2 (0.3f, 0.3f);
        }
    }

}
