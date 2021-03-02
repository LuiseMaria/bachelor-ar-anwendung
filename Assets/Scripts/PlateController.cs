using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateController : MonoBehaviour
{

    public Button RotateButton;
    public Button ResetButton;
    public Slider scaleSlider;
    public Slider rotateSlider;
    
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
    
    private bool targetFound;

    
    void Start() {   
        ResetButton.onClick.AddListener(ResetPlate);
        RotateButton.onClick.AddListener(StartRotationAnimation);
        showLabelButtons.onClick.AddListener(toggleLabelButton);
        rotateSlider = GameObject.Find("RotateSlider").GetComponent<Slider>();
        initScaleSclider();   
  
        _followOffset = highlightedRingParent.transform.position - transform.position;
    }
    
    private void toggleLabelButton(){
        labelCanvas.gameObject.SetActive(!labelCanvas.gameObject.activeSelf);
        if(labelCanvas.gameObject.activeSelf){
            showLabelButtons.image.sprite = Resources.Load<Sprite>("Icons/Hide");
        } else {
            showLabelButtons.image.sprite = Resources.Load<Sprite>("Icons/View");
        }      
    }

    private void initScaleSclider() {
        scaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();
        scaleSlider.maxValue = 1;
        scaleSlider.value = scaleSlider.maxValue;
        scaleSlider.minValue = 0.1f;
    }
 
    void Update() {
        var newScale = new Vector3(scaleSlider.value, scaleSlider.value, scaleSlider.value);
        transform.localScale = newScale;
       if(isRotating){
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        } else {
            transform.localEulerAngles = new Vector3(0.0f, rotateSlider.value, 0.0f);
        }
        transform.position = imageTarget.position;
    }

    void LateUpdate() {
        Vector3 targetPosition;
         targetPosition = transform.position + _followOffset;
         highlightedRingParent.transform.position += (targetPosition - highlightedRingParent.transform.position);    
    }

    public void StartRotationAnimation() {
        isRotating = !isRotating;
        rotateSlider.interactable = !isRotating;
    }

    public void ResetPlate(){
        isRotating = false;
        scaleSlider.value = 1;
        rotateSlider.value = 0;
        rotateSlider.interactable = true;
    }

    public void onTargetFound(bool isFound) {
        targetFound = isFound;
    }

}
