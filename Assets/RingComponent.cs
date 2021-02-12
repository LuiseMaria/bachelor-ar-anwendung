using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.IO;
using UnityEditor;


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
    private TextMeshProUGUI modalTitle;

    private string title;

    private string[] descriptionList = new string[5];

    private string description;

    [SerializeField]
    private TextMeshProUGUI modalTextDescription;
    
    [SerializeField]
    private Sprite[] firstSprites;

    private Vector2 initalImageSize;
    
    [SerializeField]
    private Sprite[] secondSprites;

    private string[] ringNames = new string[]{"Sphinx", "Greif", "Krieger", "Planeten", "Tänzer"};

    // Start is called before the first frame update
    void Start() {
        readTxtFile();
        images = imgageListGameObject.GetComponentsInChildren<Image>();
        labelButtonList = gameObjectWithButtonList.GetComponentsInChildren<Button>();

        modalCanvas.gameObject.SetActive(false);
        AddButtonListener(highlightedRing);    
        highlightedRing.gameObject.SetActive(false);
        
        initalImageSize = images[0].rectTransform.sizeDelta;

    }

    // Update is called once per frame
    void Update() {
       
    }

     private void AddButtonListener(Button ringButton) {
        ringButton.onClick.AddListener( () => {
            modalCanvas.gameObject.SetActive(true);
          //  foreach(Button labelButton in labelButtonList){
              for(int i = 0; i < labelButtonList.Length; i++){
                if(labelButtonList[i].gameObject.activeSelf){
                    if(labelButtonList[i].name == "SphinxButton"){
                        title = ringNames[i];
                        description = descriptionList[i];
                        images[0].sprite = firstSprites[0];
                        images[1].sprite = secondSprites[0];
                        setSizeOfImages(firstSprites[0], secondSprites[0]);
                    } else if(labelButtonList[i].name == "GriffinButton"){
                       title = ringNames[i];
                       description = descriptionList[i];
                        images[0].sprite = firstSprites[1];
                        images[1].sprite = secondSprites[1];
                        setSizeOfImages(firstSprites[1], secondSprites[1]);
                    } else if(labelButtonList[i].name == "KriegerButton"){
                        title = ringNames[i];
                    } else if(labelButtonList[i].name == "TänzerButton"){
                       title = ringNames[i];
                    } else if(labelButtonList[i].name == "PlanetenButton"){
                       title =ringNames[i];
                    }
                    
                }
            }
            modalTitle.text = title;
            modalTextDescription.text = description; 
           
        });
    }

    private void setSizeOfImages(Sprite firstSprite, Sprite secondSprite){
        Vector2 extraSize = new Vector2(30f, 30f);
        images[0].rectTransform.sizeDelta = firstSprite.rect.size + extraSize;
        images[1].rectTransform.sizeDelta = secondSprite.rect.size + extraSize;
    }

    // [MenuItem("Tools/Read file")]
    // static void ReadString(){
        
    //     string path = "Assets/TextRecources/GriffinDescription.txt";
    //       //Read the text from directly from the test.txt file
    //     StreamReader reader = new StreamReader(path);
    //     //TextAsset mydata = Resources.Load("MyTexts/text", typeof(TextAsset));
    //     textDescription = reader.ReadToEnd();
    //     reader.Close();
    // }

    private void readTxtFile(){
        string path = "TextDescription/";
        for(int i = 0; i < ringNames.Length; i++){
            string finalPath = path + ringNames[i];
           descriptionList[i] = Resources.Load<TextAsset>(finalPath).text;
        }
    }

}
