using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SetImageBuType : MonoBehaviour
{
    public bool crackingPhone;
    public bool[] completeTheImage = new bool[2];
    public bool describeThePicture;

    // Start is called before the first frame update
    void Awake()
    {
        if (crackingPhone)
        {
            CrackingPhone();
        }
        else if (describeThePicture)
        {
            DescribePicture();
        }
        /*else if (completeTheImage[0])
        {
            CompletePicture();
        }*/
        else
        {
            PictureToComplete();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DescribePicture()
    {
        GetComponent<Image>().sprite = GetImage.shownImage;
    }
    
    public void CrackingPhone()
    {
        GetComponent<Image>().sprite = GetImage.shownImage;
    }
    
    /*public void CompletePicture()
    {
        GetComponent<Image>().sprite = GetImage.shownImage;
    }*/

    public void PictureToComplete()
    {
        if (GetImage.isFIL)
        {
            ImageData correctImage = DrawingPanel.GetImgData(GetImage.imageName);
            correctImage.Part = true;
            GetComponent<Image>().sprite = GetImage.ConvertNameToSprite(DrawingPanel.SetImgName(correctImage), true);
        }
    }

    /* public void Completing(string _using)
     {
         Debug.Log(GetImage.imageName);
         Dictionary<string, int> correctImage = DrawingPanel.GetImgData(GetImage.imageName);
         Debug.Log(correctImage);
         correctImage["Gen"] = 1;
         //correctImage[4] = _using;
         int imageNewName = correctImage["Img"];
         for (int i = 1; i <= correctImage; i++)
         {
             imageNewName = imageNewName + "_" + correctImage[i];
         }

         Dictionary<string, int> numbers = DrawingPanel.GetImgData(imageNewName);
         string filename = "";
         if (numbers["Gen"] == 1)
         {
             filename = GetImage.baseDataPath + imageNewName + ".png";
         } else
         {
             filename = GetImage.userDataPath + imageNewName + ".png";
         }

         byte[] bytes = File.ReadAllBytes(filename);

         Texture2D texture = new Texture2D(1000, 1000);
         texture.LoadImage(bytes);

         Sprite newSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
         GetComponent<Image>().sprite = newSprite;
     }*/
}
