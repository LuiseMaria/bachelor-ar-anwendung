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
    
//    Button[] ringButtonList;

    public Button highlightedRing;

  //  public GameObject ringList;

     private Vector3 _followOffset;


  //  private RectTransform rectTransform;

    [SerializeField]
    private Transform imageTarget;

    public bool isRotating = false;
    public float rotationSpeed = 20;
    
    public bool targetFound;

    void Start() {   
      //  ringButtonList = ringList.GetComponentsInChildren<Button> ();
        ResetButton.onClick.AddListener(ResetPlate);
        RotateButton.onClick.AddListener(StartRotationAnimation);
        rotateSlider = GameObject.Find("RotateSlider").GetComponent<Slider>();
        initScaleSclider();   
        //  foreach (Button btn in ringButtonList) {
        //     _followOffset = btn.transform.position - transform.position;
        // }
        _followOffset = highlightedRing.transform.position - transform.position;
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
        // damit folgen die Ringe immer dem Ziel, also dem Teller
        // foreach (Button btn in ringButtonList) {
        //     targetPosition = transform.position + _followOffset;
        //     btn.transform.position += (targetPosition - btn.transform.position);    
        // }
         targetPosition = transform.position + _followOffset;
         highlightedRing.transform.position += (targetPosition - highlightedRing.transform.position);    
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
