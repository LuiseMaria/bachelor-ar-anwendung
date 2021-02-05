using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonComponent : MonoBehaviour {
    
    private Button[] labelButtonList;

    [SerializeField]
    private Button highlightedRing;

    private bool isActive = true;

    void Start() {
        labelButtonList = gameObject.GetComponentsInChildren<Button>();
        
        for(int i = 0; i < labelButtonList.Length; i++){
            if(labelButtonList[i] != null){
                AddButtonListener(labelButtonList[i], i);
            }
        }
    }

//    public void addLineRendererToButton(){
//         for(int i = 0; i < labelButtonList.Length; i++){
//             LineRenderer lineRenderer = labelButtonList[i].gameObject.AddComponent<LineRenderer>();
//             lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
//             lineRenderer.startColor = Color.gray;
//             lineRenderer.endColor = Color.black;
//             lineRenderer.startWidth = 0.0025f;
//             lineRenderer.endWidth = 0.002f;
           
//             targetObject = getTargetObject(labelButtonList[i]);
//             lineRenderer.SetPosition(0, labelButtonList[i].transform.position);
//             lineRenderer.SetPosition(1, targetObject.transform.position);
//         }

    private void AddButtonListener(Button button, int index) {
        button.onClick.AddListener( () => {
            if(labelButtonList[index] != null) {
                DisableButtons(index);
                ToggleButton(index);
                handleRingSelection(button.name);
            }
        });
 }

    // private Transform getTargetObject(Button button){
    //     Debug.Log("Gameobject: " + button.name);
    //         if (button.name == "SphinxButton") {
    //             return gameObjectWithTargetList.transform.Find("SphinxSphere");
    //         } else if (button.name == "GriffinButton") {
    //             return gameObjectWithTargetList.transform.Find("GriffinSphere");
    //         } else if (button.name == "KriegerButton") {
    //             return gameObjectWithTargetList.transform.Find("KriegerSphere");
    //         } else if (button.name == "PlanetenButton") {
    //             return gameObjectWithTargetList.transform.Find("PlanetsSphere");
    //         } else if (button.name == "TänzerButton") {
    //             return gameObjectWithTargetList.transform.Find("TänzerSphere");
    //         } else {
    //             return null;
    //         }
    // }

    // ALLE Buttons deaktiviert/aktiviert
    private void DisableButtons(int index) {
        for(int buttonIndex = 0; buttonIndex < labelButtonList.Length; ++buttonIndex ){
                labelButtonList[buttonIndex].gameObject.SetActive(!isActive);
        }
        isActive = !isActive;
    }
   

    //nur angeklickter Button + zugehöriger Ring wird getoggled
    private void ToggleButton(int index) {
        if(!isActive && index >= 0 && index < labelButtonList.Length) {
            labelButtonList[index].gameObject.SetActive(!labelButtonList[index].gameObject.activeSelf); 
        }
    }


    void handleRingSelection(string selectedButtonName){
        highlightedRing.gameObject.SetActive(!highlightedRing.gameObject.activeSelf); 
        if(selectedButtonName == "GriffinButton"){
            highlightedRing.transform.localScale = new Vector3(2f, 2f, 2f);
        } else if(selectedButtonName == "KriegerButton"){
            highlightedRing.transform.localScale = new Vector3(4f, 4f, 4f);
        } else if(selectedButtonName == "PlanetenButton"){
            highlightedRing.transform.localScale = new Vector3(5.3f, 5.3f, 5.3f);
        } else if(selectedButtonName == "TänzerButton"){
            highlightedRing.transform.localScale = new Vector3(7f, 7f, 7f);
        } else {
            highlightedRing.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

}
