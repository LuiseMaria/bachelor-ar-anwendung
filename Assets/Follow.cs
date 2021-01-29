using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

  public Transform leader;
  Vector3 _followOffset;
  Vector3 initalTargetScale;
  Vector3 initalScaleOfOrigin;

    void Start() {
        // Cache the initial offset at time of load/spawn:
        _followOffset = transform.position - leader.position;
        initalTargetScale = leader.localScale;
        initalScaleOfOrigin = transform.localScale;  
    }


 void LateUpdate () {
        // Apply that offset to get a target position.
        Vector3 targetPosition = leader.position + _followOffset;
        float dist = Vector3.Distance(initalTargetScale, leader.localScale);
        float diffInPercent = dist / 100;
        
        // Smooth follow.    
        transform.position += (targetPosition - transform.position);
        Vector3 calculatedScale = initalScaleOfOrigin - (initalScaleOfOrigin * diffInPercent);
        transform.localScale = leader.localScale;
    
      //  transform.GetChild(0).gameObject.transform.localScale = leader.localScale;
    }
}
