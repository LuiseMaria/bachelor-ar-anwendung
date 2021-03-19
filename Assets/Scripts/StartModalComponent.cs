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
    private Canvas CanvasWithLabelRingAndLocator = default;
    
    [SerializeField]
    private Canvas CanvasWithUIButtons = default;

    [SerializeField]
    private Canvas CanvasModal = default;

    public bool isDigitalTwin;

    public bool targetFound;

    // Start is called before the first frame update
    void Start() {
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
           
        });
    }

    // Update is called once per frame
    void Update() {
    }

    void setEverythingInactiveOnStart() {
        transform.gameObject.SetActive(false);
        PlateGameObject.SetActive(false);
        CanvasWithLabelRingAndLocator.gameObject.SetActive(false);
        CanvasWithUIButtons.gameObject.SetActive(false);
        CanvasModal.gameObject.SetActive(false);
    }

    void setLabelActiveAndOtherStuffInactive() {
        CanvasWithLabelRingAndLocator.gameObject.SetActive(true);
       // CanvasWithLabelRingAndLocator.transform.GetChild(0).gameObject.SetActive(true);
        CanvasWithLabelRingAndLocator.transform.GetChild(1).gameObject.SetActive(false);
    }
    // recognized Vuforia marker
    public void onTargetFound(bool isFound) {
        targetFound = isFound;
        transform.gameObject.SetActive(true);
    }

}
