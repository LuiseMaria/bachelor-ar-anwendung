using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Label : MonoBehaviour {

  Camera cameraToLookAt;
public Button mybutton;
public RectTransform target;


    // void OnDrawGizmosSelected() {
    //     if (target != null)
    //     {
    //         // Draws a blue line from this transform to the target
    //         Gizmos.color = Color.red;
    //       //  Vector3 posLine = new Vector3(target.transform.position.x + 0.11f, target.transform.position.y - 0.01f, target.transform.position.z);
    //         Gizmos.DrawLine(transform.position, target.transform.position);
    //     }
    // }

   // Start is called before the first frame update
    void Start()  {
         cameraToLookAt = Camera.main;
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
      
        
    }
    // Update is called once per frame
    void Update() {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        Color myButtonsColor = mybutton.GetComponent<Image>().color;
        lineRenderer.startWidth = 0.005f;
        lineRenderer.material.color = myButtonsColor;
        lineRenderer.SetPosition(0, transform.position);
    
        target.pivot = new Vector2(1, 0);
        lineRenderer.SetPosition(1, target.transform.position);
    }

    //Methode damit Label immer richtung User zeigen
     void LateUpdate() {
         target.transform.LookAt(cameraToLookAt.transform);
         target.transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
     }
}
