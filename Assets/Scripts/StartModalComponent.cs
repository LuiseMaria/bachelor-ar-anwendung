using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class StartModalComponent : MonoBehaviour {

    [SerializeField]
    private GameObject PlateGameObject = default;
    
    [SerializeField]
    private Button startWithDigitalTwin = default;
    
    [SerializeField]
    private Button startOnOriginalObject = default;
 
    [SerializeField]
    private Canvas CanvasWithLabel = default;

    [SerializeField]
    private Canvas CanvaswithLocator = default;
    
    [SerializeField]
    private Canvas CanvasWithUIButtons = default;

    [SerializeField]
    private Canvas CanvasModal = default;

    [SerializeField]
    private Transform imageTarget = default;

    public bool isDigitalTwin;

    public bool targetFound;

    private Vector3 _followOffsetImageTarget;

    // Start is called before the first frame update
    void Start() {

        _followOffsetImageTarget = PlateGameObject.transform.position - imageTarget.transform.position;
        Debug.Log(_followOffsetImageTarget + " _followOffsetImageTarget");
        setEverythingInactiveOnStart();

        startOnOriginalObject.onClick.AddListener( () => {
            isDigitalTwin = false;
            setLabelActiveAndOtherStuffInactive();
            PlateGameObject.transform.GetChild(0).gameObject.SetActive(false);
            // PlateGameObject.GetComponent<LeanTouch>().enabled = false;

            // inactivate ResetButton, 360° Button & PinchToZoom Button
            CanvasWithUIButtons.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            CanvasWithUIButtons.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            CanvasWithUIButtons.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        });
        startWithDigitalTwin.onClick.AddListener( () => {
            isDigitalTwin = true;
            setLabelActiveAndOtherStuffInactive();
            PlateGameObject.GetComponent<LeanTouch>().enabled = true;
            PlateGameObject.transform.GetChild(0).gameObject.SetActive(true);
         //   setPosition();
           
        });
    }

//    void setPosition() {
//         Vector3 targetPosition;
//         targetPosition = imageTarget.transform.position + _followOffsetImageTarget;
//         PlateGameObject.transform.position += (targetPosition - PlateGameObject.transform.position);
//     }

    void setEverythingInactiveOnStart() {
        transform.gameObject.SetActive(false);
        PlateGameObject.SetActive(false);
        CanvasWithLabel.gameObject.SetActive(false);
        CanvaswithLocator.gameObject.SetActive(false);
        CanvasWithUIButtons.gameObject.SetActive(false);
        CanvasModal.gameObject.SetActive(false);
    }

    void setLabelActiveAndOtherStuffInactive() {
        CanvasWithLabel.gameObject.SetActive(true);
        CanvaswithLocator.gameObject.SetActive(true);
        CanvaswithLocator.transform.GetChild(1).gameObject.SetActive(false);
    }
    
    // recognized Vuforia marker
    public void onTargetFound(bool isFound) {
        targetFound = isFound;
        transform.gameObject.SetActive(true);
    }

}
