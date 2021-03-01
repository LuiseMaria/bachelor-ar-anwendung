using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Vuforia;

public class WorldPositionButton : MonoBehaviour {

    private Camera cameraToLookAt;
    private Vector3 _followOffset;

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private RectTransform rectTransform;

    private Vector2 pivotRightBottomCorner = new Vector2(0.97f, 0.08f);
    private Vector2 pivoLeftBottomCorner = new Vector2(0.03f, 0.07f);

    void Awake() {  
        rectTransform = GetComponent<RectTransform>();
    }

    void Start(){
        cameraToLookAt = Camera.main;
        addLineRenderer();
        _followOffset = transform.position - targetTransform.position;
    }
  
     
    private void Update() {  
        if(rectTransform.transform.tag == "RightLabelButtons") {
            rectTransform.pivot = pivoLeftBottomCorner;
        } else if(rectTransform.transform.tag == "LeftLabelButtons"){
            rectTransform.pivot = pivotRightBottomCorner;
        }
        implementLineRenderer();
    }

    void addLineRenderer(){
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.gray;
        lineRenderer.endColor = Color.black;
    }

    void implementLineRenderer(){
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.0025f;
        lineRenderer.endWidth = 0.002f;
        lineRenderer.SetPosition(0, rectTransform.transform.position);
        lineRenderer.SetPosition(1, targetTransform.transform.position);
    }

    void LateUpdate() {
     // damit Label immer richtung User zeigen
        rectTransform.transform.LookAt(cameraToLookAt.transform);
        rectTransform.transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
        
        // damit folgen die Label immer dem Ziel, also dem Teller
        Vector3 targetPosition = targetTransform.position + _followOffset;
        transform.position += (targetPosition - transform.position);      
    }


}
