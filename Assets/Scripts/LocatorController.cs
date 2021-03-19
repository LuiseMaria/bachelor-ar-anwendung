using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Vuforia;

public class LocatorController : MonoBehaviour {

    private Transform targetTransform;
    
    [SerializeField]
    private GameObject imageTargetList = default;

    private Vector3 _followOffset;

    public ButtonComponent buttonComponentScript;

    [SerializeField]
    private GameObject SlideList = default;

    private Vector3 camPos; 
    private Transform camTr;

    [SerializeField]
    private Canvas modalCanvas = default;


    // Start is called before the first frame update
    void Start(){
        camTr  = Camera.main.transform;
        camPos = camTr.position;
       // targetTransform = imageTargetList.transform.GetChild(0).GetChild(0).GetComponent<Transform>();
       if(targetTransform != null){
           _followOffset = transform.position - targetTransform.position;
       }
       transform.gameObject.SetActive(false);
        
    }

    void LateUpdate() {
        if(targetTransform != null){
            Transform arrow = transform.GetChild(0);
           // Transform circle = transform.GetChild(1);
            
            arrow.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
         //   Vector3 targetPos = new Vector3(targetTransform.position.x, targetTransform.position.y + 0.03f, targetTransform.position.z);
            Vector3 targetPosition = targetTransform.position + _followOffset;
            transform.position += (targetPosition - transform.position);
            
        }
    }

    public void AddButtonListenerLocator(){
        modalCanvas.gameObject.SetActive(false);
        camPos.x = transform.position.x;
        camPos.y = transform.position.y;
        camPos.z += 2.0f; // Zoom
        camTr.position = Vector3.Lerp(camTr.position, camPos, Time.deltaTime * 2.5f);
        Camera.main.transform.position = camTr.position;
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