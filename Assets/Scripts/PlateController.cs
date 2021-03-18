using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class PlateController : MonoBehaviour {

    public Button RotateButton;
    public Button ResetButton;
    public Toggle pinchToZoomButton;
    
  //  public ButtonComponent buttonComponentScript;

    public GameObject highlightedRingParent;
    
    [SerializeField]
    private Transform imageTarget;

    [SerializeField]
    private Button showLabelButtons;

    [SerializeField]
    private Canvas labelCanvas;

    private Vector3 _followOffset;

    private bool isRotating = false;
    private float rotationSpeed = 20;

    private Vector3 defaultLocalScale;

    private Vector3 defaultRingScale;
    
    private bool targetFound;

    
    void Start() {   
        ResetButton.onClick.AddListener(ResetPlate);
        RotateButton.onClick.AddListener(StartRotationAnimation);
        showLabelButtons.onClick.AddListener(ToggleLabelButton);
        pinchToZoomButton.onValueChanged.AddListener(delegate {
            PinchToZoom();
        });
        // defaultRingScale = highlightedRingParent.transform.GetChild(0).localScale;
        defaultRingScale = new Vector3(1f, 1f, 1f);
        defaultLocalScale = transform.localScale;
        // var newScale = new Vector3(scaleSlider.value, scaleSlider.value, scaleSlider.value);
        // transform.localScale = newScale;
        
        _followOffset = highlightedRingParent.transform.position - transform.position;
    }
    
    private void ToggleLabelButton(){
        labelCanvas.gameObject.SetActive(!labelCanvas.gameObject.activeSelf);
        if(labelCanvas.gameObject.activeSelf){
            showLabelButtons.image.sprite = Resources.Load<Sprite>("Icons/Hide");
        } else {
            showLabelButtons.image.sprite = Resources.Load<Sprite>("Icons/View");
        }      
    }

    public void PinchToZoom() {
        gameObject.GetComponent<LeanTouch>().enabled = pinchToZoomButton.isOn;
         pinchToZoomButton.image.color =  new Color32(200, 200, 200, 128);
        if(pinchToZoomButton.isOn){
            pinchToZoomButton.image.color = new Color32(255, 255, 255, 255);
        } else {
            pinchToZoomButton.image.color =  new Color32(200, 200, 200, 128);
        }
        
    }
 
    void Update() {
       if(isRotating){
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    //     } else {
    //         transform.localEulerAngles = new Vector3(0.0f, rotateSlider.value, 0.0f);
        }
        transform.position = imageTarget.position;
       
        highlightedRingParent.transform.localScale = transform.localScale;
        // if(highlightedRingParent.transform.GetChild(0).gameObject.activeSelf){

        // }
    //   Debug.Log("differenz : " + differenz + " defaultRingScale: " + defaultRingScale);
        if(transform.localScale == defaultLocalScale){
            ResetButton.interactable = false;
            // defaultRingScale = highlightedRingParent.transform.GetChild(0).localScale;
        } else {
            ResetButton.interactable = true;
            // float differenz = Vector3.Distance(defaultLocalScale, transform.localScale);
            // Vector3 ggg = defaultRingScale * differenz;
            // for(int i = 0; i < highlightedRingParent.transform.childCount -1; i++){
            //     highlightedRingParent.transform.GetChild(0).GetComponent<Button>().transform.localScale = transform.localScale;
            //     highlightedRingParent.transform.GetChild(i+1).localScale = transform.localScale;
            // }
           // float differenzProzent = differenz / 1;
        //    if(differenz <= 1.0){
                // for(int i = 0; i < highlightedRingParent.transform.childCount -1; i++){
        //         highlightedRingParent.transform.GetChild(0).GetComponent<Button>().transform.localScale = defaultRingScale + ggg;
        //         // if(highlightedRingParent.transform.childCount > 1){
                //     highlightedRingParent.transform.GetChild(i+1).localScale = transform.localScale;
                // }
            // }
        //    } else if(differenz > 1.0){
        //         highlightedRingParent.transform.GetChild(0).GetComponent<Button>().transform.localScale = defaultRingScale - ggg;
        //    }
            
        }
    }

    //  private void getTargetPosition(){
    //      Button button = highlightedRingParent.transform.GetChild(0).GetComponent<Button>();
    //     if(buttonComponentScript.firstRing){
            
    //     } else if(buttonComponentScript.secondRing){
    //         Debug.Log("second ring is active");
    //         targetTransform = GameObject.Find("Vollmond").GetComponent<Transform>();
    //     } 
    // }

    void LateUpdate() {
        Vector3 targetPosition;
        targetPosition = transform.position + _followOffset;
        highlightedRingParent.transform.position += (targetPosition - highlightedRingParent.transform.position);    
    }

    public void StartRotationAnimation() {
        isRotating = !isRotating;
    }

    public void ResetPlate(){
        isRotating = false;
        transform.localScale = defaultLocalScale;
    }

    public void onTargetFound(bool isFound) {
        targetFound = isFound;
    }

}
