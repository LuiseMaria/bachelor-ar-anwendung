using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateAnimation : MonoBehaviour
{
    private Animator Anim;

    void Start() {
        Anim = GetComponent<Animator>();
        Anim.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AnimScale() {
        Anim.Play("ScalePlate", -1, 0f);
        Anim.speed = 1f;
    }

       public void AnimRotate() {
        Anim.Play("RotatePlate", -1, 0f);
        Anim.speed = 1f;
    }

       public void AnimReset() {
        Anim.Play("Plateback", -1, 0f);
        Anim.speed = 1f;
    }


}
