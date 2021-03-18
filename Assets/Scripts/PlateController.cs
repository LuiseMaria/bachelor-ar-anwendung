using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class PlateController : MonoBehaviour {

    public Button RotateButton;
    public Button ResetButton;
    public Toggle pinchToZoomButton;

    public GameObject highlightedRingParent;
    
    [SerializeField]
    private Transform imageTarget = default;

    [SerializeField]
    private Button showHideButton = default;

    [SerializeField]
    private Canvas labelCanvas = default;

    private Vector3 _followOffset;

    private bool isRotating = false;
    private float rotationSpeed = 20;

    private Vector3 defaultScale;
    
    public bool targetFound;

    public bool isDigitalTwin = true;
    
    void Start() { 
        isDigitalTwin = true;
        settingsDigitalTwin();  
        AddListenerControllButtons();
        defaultScale = transform.localScale;
        _followOffset = highlightedRingParent.transform.position - transform.position;
    }
    
    private void AddListenerControllButtons() {
        ResetButton.onClick.AddListener(ResetPlate);
        RotateButton.onClick.AddListener(StartRotationAnimation);
        showHideButton.onClick.AddListener(ToggleLabelView);
        pinchToZoomButton.onValueChanged.AddListener(delegate {
            PinchToZoom();
        });
    }

    private void settingsDigitalTwin(){
        if(!isDigitalTwin){
            ResetButton.gameObject.SetActive(false);
            RotateButton.gameObject.SetActive(false);
            pinchToZoomButton.gameObject.SetActive(false);
        }
    }

    private void ToggleLabelView(){
        labelCanvas.gameObject.SetActive(!labelCanvas.gameObject.activeSelf);
        if(labelCanvas.gameObject.activeSelf){
            showHideButton.image.sprite = Resources.Load<Sprite>("Icons/View");
        } else {
            showHideButton.image.sprite = Resources.Load<Sprite>("Icons/Hide");
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
       if(isRotating) {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        transform.position = imageTarget.position;
        highlightedRingParent.transform.localScale = transform.localScale;

        if(transform.localScale == defaultScale){
            ResetButton.interactable = false;
        } else {
            ResetButton.interactable = true;
        }
    }

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
        transform.localScale = defaultScale;
    }

    public void onTargetFound(bool isFound) {
        targetFound = isFound;
    }

}
