using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using UnityEngine.EventSystems;

namespace DanielLochner.Assets.SimpleScrollSnap {
public class DynamicPanelController : MonoBehaviour {
    
    [SerializeField]
    private Canvas modalCanvas = default;

    private SimpleScrollSnap srollSnapComponent;

    [SerializeField]
    private GameObject contentPanel = default;

    [SerializeField]
    private GameObject SlideList = default;

    [SerializeField]
    private Transform toggle = default;

    public ButtonComponent buttonComponentScript;

    [SerializeField]
    private GameObject imageTargetList = default;

    private float toggleWidth;

    private void Awake(){
        srollSnapComponent = GetComponent<SimpleScrollSnap>();
        toggleWidth = toggle.GetComponent<RectTransform>().sizeDelta.x * (Screen.width / 500f);
    }


    public void openModalButtonAddOnListener() {
        modalCanvas.gameObject.SetActive(true);
        setModalImageAndText(EventSystem.current.currentSelectedGameObject.transform.parent.name);
        srollSnapComponent.GoToPanel(0);
        getLocationPin();
    }

    private void setModalImageAndText(string selectedRing){
        List<String> files = getFiles(selectedRing);
        if(files.Count > 0){
            handleNumberOfSlides(files.Count);
            setImage(srollSnapComponent.NumberOfPanels, files);
            readTxtFile(srollSnapComponent.NumberOfPanels, "SlideContent/" + $"{selectedRing}");
        }
    }


    private List<String> getFiles(string selectedRing){
        selectedRing = selectedRing.Replace(" ", "");
        string folderPath = Application.streamingAssetsPath + "/SlideContent/" + $"{selectedRing}";
        string finalPath = folderPath + "/Images/";
        List<String> files = new List<String>(Directory.GetFiles(finalPath, "*.JPG"));
        files.AddRange(Directory.GetFiles(finalPath, "*.jpg"));
        files.AddRange(Directory.GetFiles(finalPath, "*.png"));
        files.Sort();
        return files;
    }

    private void readTxtFile(int slideLength, String assetPath){
        Debug.Log("assetPath " + assetPath);
        string textAsset = Resources.Load<TextAsset>(assetPath).text;
        string[] linesInFile = textAsset.Split(';');
        for(int i = 0; i < slideLength; i++){
            TextMeshProUGUI title = SlideList.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI description = SlideList.transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>();
            title.text = linesInFile[0];
             if(i < (linesInFile.Length - 1)){
                description.text = linesInFile[i+1];
            } else {
                description.text = "";
            }
        }
    }

    private void getLocationPin(){
        for(int i = 0; i < srollSnapComponent.NumberOfPanels; i++) {
            if(buttonComponentScript.firstRing && (i >= imageTargetList.transform.GetChild(0).childCount)){
                for(int index = 0; index < imageTargetList.transform.GetChild(0).childCount; index++){
                    SlideList.transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                }
            } else if(buttonComponentScript.secondRing && (i >= imageTargetList.transform.GetChild(1).childCount)){
                for(int index = 0; index < imageTargetList.transform.GetChild(1).childCount; index++){
                    SlideList.transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                }
            } else if(buttonComponentScript.thirdRing && (i >= imageTargetList.transform.GetChild(2).childCount)){
                for(int index = 0; index < imageTargetList.transform.GetChild(2).childCount; index++){
                    SlideList.transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                }
            } else if(buttonComponentScript.fourthRing && (i >= imageTargetList.transform.GetChild(3).childCount)){
                for(int index = 0; index < imageTargetList.transform.GetChild(3).childCount; index++){
                    SlideList.transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                }
            } else if(buttonComponentScript.fifthRing && (i >= imageTargetList.transform.GetChild(4).childCount)){
                for(int index = 0; index < imageTargetList.transform.GetChild(4).childCount; index++){
                    SlideList.transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                }
            }
        }
    }

  private void setImage(int slideLength, List<String> filePaths){
        for(int i = 0; i < slideLength; i++) {
            Image image = SlideList.transform.GetChild(i).GetChild(3).GetChild(0).GetComponent<Image>();
            if(i < filePaths.Count){
                image.sprite = GetSpritefromImage(filePaths[i]);
                setSizeOfImages(image);
            }
        }
    }

    private void handleNumberOfSlides(int fileLength){
        if(srollSnapComponent.NumberOfPanels < fileLength){
            int difference = fileLength - srollSnapComponent.NumberOfPanels;
            for(int i = 0; i < difference; i++){
                AddSlide();
            }
        } else if (srollSnapComponent.NumberOfPanels > fileLength){
            int difference = srollSnapComponent.NumberOfPanels - fileLength;
            for(int i = 0; i < difference; i++) {
                RemoveSlide(i);
            }
        }
    }

    private void AddSlide(){
        Instantiate(toggle, srollSnapComponent.pagination.transform.position + new Vector3(toggleWidth * (srollSnapComponent.NumberOfPanels), 0, 0), Quaternion.identity, srollSnapComponent.pagination.transform);
        srollSnapComponent.pagination.transform.position -= new Vector3(toggleWidth / 2f, 0, 0);
        srollSnapComponent.Add(contentPanel, 0);
    }

   private void setSizeOfImages(Image image) {
        Vector2 extraSize = new Vector2(30f, 30f);
        Debug.Log("slideLength "+image.sprite.rect.size.x);
        if(image.sprite.rect.size.x > 800 && image.sprite.rect.size.x < 900) {
            image.rectTransform.sizeDelta = image.sprite.rect.size * 0.5f;
        } else if(image.sprite.rect.size.x > 900) {
            image.rectTransform.sizeDelta = image.sprite.rect.size * 0.4f;
        } else {
            image.rectTransform.sizeDelta = image.sprite.rect.size;
        }
   }
   
   
    private void RemoveSlide(int index) {
        if (srollSnapComponent.NumberOfPanels > 0) {
            //Pagination
            DestroyImmediate(srollSnapComponent.pagination.transform.GetChild(srollSnapComponent.NumberOfPanels - 1).gameObject);
            srollSnapComponent.pagination.transform.position += new Vector3(toggleWidth / 2f, 0, 0);

            //Panel
            srollSnapComponent.Remove(index);
        }
    }

     private Sprite GetSpritefromImage(string imgPath) {
        //Converts desired path into byte array
        byte[] bytes = System.IO.File.ReadAllBytes(imgPath);
 
        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);
 
        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        return fromTex;
     }

}

}
