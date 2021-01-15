using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldPositionButton : MonoBehaviour
{

    [SerializeField]
    private Transform targetTransform;


    public RectTransform rectTransform;

    private Button labelButton;


    void Awake() {  
        rectTransform = GetComponent<RectTransform>();
        labelButton = GetComponent<Button>();
    }

    void Start(){
        
    }

    private void Update() {  
        var targetPosition = targetTransform.position;
        Vector3 labelPosition = rectTransform.position;
        rectTransform.position = targetPosition + new Vector3(-0.1f, 0.2f, 0.1f);

      //  float dist = Vector3.Distance(targetPosition, labelPosition);
      //  rectTransform.position = new Vector3(labelPosition.x + dist, labelPosition.y + dist, labelPosition.z + dist);
        //var show = distanceFromCenter < 0.3f;

    //    labelButton.enabled = show;
    }
}
