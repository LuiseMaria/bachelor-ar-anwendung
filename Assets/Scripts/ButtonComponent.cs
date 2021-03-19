using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleScrollSnap;

public class ButtonComponent : MonoBehaviour {

    [SerializeField]
    private Button highlightedRing = default;
    
    [SerializeField]
    private GameObject imageLocationPin = default;

    public DynamicPanelController modalPanelComponentScript;

    private Transform buttonParent;

    private Button highlightedRingInside;

    private bool isActive = true;

    private int indexOfReturnButton = 0;

    private int indexOfBackButton = 1;

    private int indexOfModalButton = 2;

    private Button labelButton;
    
    private Button backButton;

    private Button startModalButton;

    private int childCount;

    public bool firstRing = false;
    public bool secondRing = false;
    public bool thirdRing = false;
    public bool fourthRing = false;
    public bool fifthRing = false;

    void Start() {
        highlightedRingInside = Instantiate(highlightedRing, highlightedRing.transform.parent);
        childCount = gameObject.transform.childCount;
        for(int i = 0; i < childCount; i++) {
            buttonParent = gameObject.transform.GetChild(i);
            labelButton = buttonParent.GetChild(indexOfReturnButton).GetComponent<Button>();
            backButton = buttonParent.GetChild(indexOfBackButton).GetComponent<Button>();
            startModalButton = buttonParent.GetChild(indexOfModalButton).GetComponent<Button>();
            AddButtonListenerLabel(labelButton, i);
            AddButtonListenerReturnButton(buttonParent);
            startModalButton.onClick.AddListener( () => {
                modalPanelComponentScript.openModalButtonAddOnListener();
            });
            backButton.gameObject.SetActive(false);
            startModalButton.gameObject.SetActive(false);
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
    
    private void AddButtonListenerReturnButton(Transform buttonParent) {
        backButton.onClick.AddListener( () => {
            DisableButtons(true, buttonParent);
            highlightedRing.transform.parent.gameObject.SetActive(false);
            imageLocationPin.gameObject.SetActive(false);
        });
    }

    // ALLE Label Buttons deaktiviert/aktiviert
    private void DisableButtons(bool enable, Transform buttonParent) {
            buttonParent.GetChild(indexOfBackButton).gameObject.SetActive(!enable);
            buttonParent.GetChild(indexOfModalButton).gameObject.SetActive(!enable);
            for(int buttonIndex = 0; buttonIndex < childCount; ++buttonIndex ){
                gameObject.transform.GetChild(buttonIndex).gameObject.SetActive(enable);
            }
        isActive = !isActive;
    }

    //nur angeklickter Button
    private void ToggleButton(Transform buttonGO, int index) {
        if(!isActive) {
            buttonGO.gameObject.SetActive(!buttonGO.gameObject.activeSelf);
        }
    }

    private void handleRingSelection(int indexOfRing){
        highlightedRing.transform.parent.gameObject.SetActive(!highlightedRing.transform.parent.gameObject.activeSelf);
        highlightedRingInside.gameObject.SetActive(true);
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
        } else if(indexOfRing == 0){ // Sphinghen & Greife
            firstRing = true;
            highlightedRingInside.gameObject.SetActive(false);
            highlightedRing.image.rectTransform.sizeDelta = new Vector2 (0.3f, 0.3f);
        }
    }

}
