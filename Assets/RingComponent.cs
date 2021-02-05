using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RingComponent : MonoBehaviour {

    [SerializeField]
    private Button highlightedRing;

  //  private Button labelButton;

    [SerializeField]
    private Canvas modalCanvas;

    [SerializeField]
    private GameObject gameObjectWithButtonList;

    [SerializeField]
    private GameObject imgageListGameObject;

    private Image[] images;
    
    private Button[] labelButtonList;

    [SerializeField]
    private Text modalTitle;

    private string title;

    [SerializeField]
    private Text modalTextDescription;
    
    [SerializeField]
    private Sprite[] firstSprites;

    
    [SerializeField]
    private Sprite[] secondSprites;

    // Start is called before the first frame update
    void Start() {
        // highlightedRing = gameObject.GetComponent<Button>();
        // modalCanvas = gameObject.GetComponent<Canvas>();
        images = imgageListGameObject.GetComponentsInChildren<Image>();

        modalCanvas.gameObject.SetActive(false);
        AddButtonListener(highlightedRing);    
        highlightedRing.gameObject.SetActive(false);

        labelButtonList = gameObjectWithButtonList.GetComponentsInChildren<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void AddButtonListener(Button ringButton) {
        ringButton.onClick.AddListener( () => {
            modalCanvas.gameObject.SetActive(true);
            foreach(Button labelButton in labelButtonList){
                if(labelButton.gameObject.activeSelf){
                    if(labelButton.name == "SphinxButton"){
                        modalTitle.text = "Sphinx";
                        images[0].sprite = firstSprites[0];
                        images[1].sprite = secondSprites[0];
                    } else if(labelButton.name == "GriffinButton"){
                        modalTitle.text = "Greif";
                        images[0].sprite = firstSprites[1];
                        Debug.Log("ringClick " + firstSprites[1].bounds.size + " " +images[1].rectTransform.sizeDelta);
                        images[1].rectTransform.sizeDelta = new Vector2(518f, 175f);
                        images[1].sprite = secondSprites[1];
                    }
                }
            }
                
        });
    }
}
