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
    
    Button[] ringButtonList;

    public GameObject ringList;


  //  private RectTransform rectTransform;

   //[SerializeField]
    private Button firstRing, secondRing, thirdRing, fourthRing, lastRing;

    [SerializeField]
    private Transform imageTarget;

    public bool isRotating = false;
    public float rotationSpeed = 20;
    
    void Start() {   
        ringButtonList = ringList.GetComponentsInChildren<Button> ();
        ResetButton.onClick.AddListener(ResetPlate);
        RotateButton.onClick.AddListener(StartRotationAnimation);
        rotateSlider = GameObject.Find("RotateSlider").GetComponent<Slider>();
        initScaleSclider();
        
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
         transform.localScale = transform.localScale;
        //rectTransform.transform.localScale = transform.localScale;
       if(isRotating){
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        } else {
            transform.localEulerAngles = new Vector3(0.0f, rotateSlider.value, 0.0f);
        }
        transform.position = imageTarget.position;
          foreach (Button btn in ringButtonList) {
            btn.transform.position = transform.position;
         }
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

     public void ShowDebugText() {
        Debug.Log("Click");
    }

}
