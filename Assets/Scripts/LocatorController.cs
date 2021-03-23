using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LocatorController : MonoBehaviour {

    private Transform targetTransform;
    
    [SerializeField]
    private GameObject imageTargetList = default;

    private Vector3 _followOffset;

    public ButtonComponent buttonComponentScript;

    [SerializeField]
    private GameObject SlideList = default;

    [SerializeField]
    private Canvas modalCanvas = default;

    [SerializeField]
    private GameObject LocatorArrowObject = default;
    
    private float originalY;

    public float floatStrength = 0.01f;

    // Start is called before the first frame update
    void Start(){
       if(targetTransform != null){
           _followOffset = transform.position - targetTransform.position;
       }
       transform.gameObject.SetActive(false);
       originalY = transform.position.y;
    }

    void Update() {
        if(targetTransform != null){
            Vector3 targetPosition = targetTransform.position + _followOffset;
            transform.position += (targetPosition - transform.position);
            originalY = targetTransform.transform.position.y + 0.05f;
        }
        initFloatingArrow();
    }

  private void initFloatingArrow(){
        LocatorArrowObject.transform.position = new Vector3(LocatorArrowObject.transform.position.x,
        originalY + ((float)Math.Sin(Time.time) * floatStrength), LocatorArrowObject.transform.position.z);
    }

    public void AddButtonListenerLocator(){
        modalCanvas.gameObject.SetActive(false);
        getTargetPosition();
    }


    private void getTargetPosition(){
        int slideLength = SlideList.transform.childCount;
        for(int i = 0; i < slideLength; i++) {
            Button button = SlideList.transform.GetChild(i).GetChild(4).GetComponent<Button>();
            Button currentGameObject = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            if(button == currentGameObject) {
               if(buttonComponentScript.firstRing && i < imageTargetList.transform.GetChild(0).childCount) {
                    targetTransform = imageTargetList.transform.GetChild(0).GetChild(i).GetComponent<Transform>();
               } else if(buttonComponentScript.secondRing && i < imageTargetList.transform.GetChild(1).childCount) {
                    targetTransform = imageTargetList.transform.GetChild(1).GetChild(i).GetComponent<Transform>();
               } else if(buttonComponentScript.thirdRing && i < imageTargetList.transform.GetChild(2).childCount) {
                    targetTransform = imageTargetList.transform.GetChild(2).GetChild(i).GetComponent<Transform>();
               } else if(buttonComponentScript.fourthRing && i < imageTargetList.transform.GetChild(3).childCount) {
                    targetTransform = imageTargetList.transform.GetChild(3).GetChild(i).GetComponent<Transform>();
               } else if(buttonComponentScript.fourthRing && i < imageTargetList.transform.GetChild(4).childCount) {
                    targetTransform = imageTargetList.transform.GetChild(4).GetChild(i).GetComponent<Transform>();
               }
            }

        }
    }

}