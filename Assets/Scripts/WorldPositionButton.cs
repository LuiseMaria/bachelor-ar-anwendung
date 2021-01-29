using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Vuforia;

public class WorldPositionButton : MonoBehaviour {

    Camera cameraToLookAt;
    Vector3 _followOffset;

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private GameObject plateGameObject;

    [SerializeField]
    private RectTransform rectTransform;

    private Vector2 pivotRightBottomCorner = new Vector2(0.97f, 0.08f);
    private Vector2 pivoLeftBottomCorner = new Vector2(0.03f, 0.07f);

    private Button griffinButton;

    public Button sphinxButton;



    void Awake() {  
        rectTransform = GetComponent<RectTransform>();
    }

    void Start(){
        cameraToLookAt = Camera.main;
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
         lineRenderer.startColor = Color.gray;
         lineRenderer.endColor = Color.black;
        _followOffset = transform.position - targetTransform.position;
        griffinButton = GameObject.Find("GriffinButton").GetComponent<Button>();
        sphinxButton = GameObject.Find("SphinxButton").GetComponent<Button>();
        griffinButton.onClick.AddListener(ButtonClick);
    }
  
     
    private void Update() {  
        if(rectTransform.transform.tag == "RightLabelButtons") {
            rectTransform.pivot = pivoLeftBottomCorner;
        } else if(rectTransform.transform.tag == "LeftLabelButtons"){
            rectTransform.pivot = pivotRightBottomCorner;
        }
        // if((rectTransform.transform.tag == "RightLabelButtons" && cameraToLookAt.transform.rotation.y > 0.0) 
        // || (rectTransform.transform.tag == "LeftLabelButtons" && cameraToLookAt.transform.rotation.y < 0.0)){
        //     rectTransform.pivot = pivoLeftBottomCorner;
        // } 
        // if ((rectTransform.transform.tag == "LeftLabelButtons" && cameraToLookAt.transform.rotation.y > 0.0) 
        // || (rectTransform.transform.tag == "RightLabelButtons" && cameraToLookAt.transform.rotation.y < 0.0)){
        //     rectTransform.pivot = pivotRightBottomCorner;
        // }
   //     Debug.Log("y wert: " + cameraToLookAt.transform.rotation.y);
        implementLineRenderer();
    }

    void implementLineRenderer(){
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.0025f;
        lineRenderer.endWidth = 0.002f;
        lineRenderer.SetPosition(0, rectTransform.transform.position);
        lineRenderer.SetPosition(1, targetTransform.transform.position);
      //   lineRenderer.transform.rotation =  plateGameObject.transform.rotation;
     
    }

     void LateUpdate() {
     // damit Label immer richtung User zeigen
        rectTransform.transform.LookAt(cameraToLookAt.transform);
        //  rectTransform.transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
       transform.rotation = plateGameObject.transform.rotation;
       transform.localEulerAngles = plateGameObject.transform.localEulerAngles;
        
        // damit folgen die Label immer dem Ziel, also dem Teller
        Vector3 targetPosition = targetTransform.position + _followOffset;
        transform.position += (targetPosition - transform.position);      

     }
 

       public void ButtonClick() {
        Debug.Log("Click");
        sphinxButton.gameObject.SetActive(false);
    }
}
