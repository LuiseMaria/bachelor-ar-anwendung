using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LocatorController : MonoBehaviour {

    private Transform targetTransform;
    
    private Vector3 _followOffset;

    public ButtonComponent buttonComponentScript;

    private Button searchIconButton;

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
        if(targetTransform != null){
            _followOffset = transform.position - targetTransform.position;
        }
    }


    void LateUpdate() {
        if(targetTransform != null){
            // damit folgen die Label immer dem Ziel, also dem Teller
            Vector3 targetPos = new Vector3(targetTransform.position.x, targetTransform.position.y + 0.01f, targetTransform.position.z);
            Vector3 targetPosition = targetPos + _followOffset;
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
            button.name = "LocateImage" + i;
            Button currentGameObject = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            if(button == currentGameObject){
                Debug.Log("Button ausgewählt bei Slide " + i);
            }

        }
        if(buttonComponentScript.firstRing){
            if(EventSystem.current.currentSelectedGameObject.transform.name.Contains("1")){
                targetTransform = GameObject.Find("Sphinx").GetComponent<Transform>();
            } else if(EventSystem.current.currentSelectedGameObject.transform.name.Contains("2")){
                targetTransform = GameObject.Find("Greif").GetComponent<Transform>();
            }
        } else if(buttonComponentScript.secondRing){
            Debug.Log("EventSystem.current.currentSelected " + EventSystem.current.currentSelectedGameObject.transform.name);
            if(EventSystem.current.currentSelectedGameObject.transform.name.Contains("1")){
                targetTransform = GameObject.Find("Vollmond").GetComponent<Transform>();
            } else if(EventSystem.current.currentSelectedGameObject.transform.name.Contains("2")){
                targetTransform = GameObject.Find("Sichel").GetComponent<Transform>();
            }
        } else {
            targetTransform = GameObject.Find("Sphinx").GetComponent<Transform>();
        }
    }

}