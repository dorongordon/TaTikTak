using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GetImage : MonoBehaviour
{
    // Start is called before the first frame update
    static public string imageName;
    static public int imageIndex;
    static public string baseDataPath;
    static public string leavesDataPath;
    static public string userDataPath;

    static public bool isFIL;

    static public List<string> usedImages;
    static public List<string> optionalTypes = new List<string>();

    static public Sprite shownImage;

    static public GetImage _data;

    void Start()
    {

        // define the list of all leaves
        //      get number of drawings
        //      get number of lineages
        //      get last generation of each drawing/lineage

        // get random drawing number
        // get random lineage number
        // get last generation

        // get correct image
        // baseDataPath = Application.dataPath + "/Data/";
        baseDataPath = Application.persistentDataPath + "/Data/base/";

        leavesDataPath = Application.persistentDataPath + "/Data/";
        // C:/Users/User/AppData/LocalLow/DefaultCompany/TaTikTak/Data/
        userDataPath = Application.persistentDataPath + "/Data/userdata/";
        Debug.Log("Path: " + baseDataPath);

        optionalTypes = new List<string>(ImageData.GetIntTypes());

        _data = this;
    }

    public void Done(bool isAgain)
    {
        var logList = GetNextImg();
        // Read leaves.csv file
        imageIndex = (int)Mathf.Round(Random.Range(0, logList.Count));
        imageName = logList[imageIndex].ToString();
        ImageData numbers = DrawingPanel.GetImgData(imageName);
        shownImage = GetImage.ConvertNameToSprite(imageName, numbers.Rep == 1);

        if (!isAgain)
        {
            GameObject.FindObjectOfType<GetAllData>().DoneMaetData();
        }
    }

    static public Sprite ConvertNameToSprite(string imgName, bool isBasicImg)
    {
        string imagePath = "";
        if (isBasicImg)
        {
            imagePath = userDataPath + imgName + ".png";
        }
        else
        {
            imagePath = userDataPath + imgName + ".png";
        }

        byte[] bytes = File.ReadAllBytes(imagePath);

        Texture2D texture = new Texture2D(1000, 1000);
        texture.LoadImage(bytes);

        Sprite newSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);

        return newSprite;
    }

    public List<string> GetNextImg()
    {
        // given leaves and used images (what I did until now), select the next random image
        // if used images is "full", reset it
        string filename = GetImage.leavesDataPath + "leavesList.csv";
        Debug.Log(filename);
        var logFile = File.ReadAllLines(filename);
        var logList = new List<string>(logFile);

        Debug.Log("logList: " + logList.Count);

        Dictionary<string, List<int>> usedImagesImgAttribute = new Dictionary<string, List<int>>();
        usedImagesImgAttribute["CMG"] = new List<int>();
        usedImagesImgAttribute["COG"] = new List<int>();
        usedImagesImgAttribute["FIL"] = new List<int>();
        foreach (string item in usedImages)
        {
            ImageData itemData = DrawingPanel.GetImgData(item);

            usedImagesImgAttribute[itemData.Int].Add(itemData.Img);
        }

        List<string> optionalImages = new List<string>();
        Debug.Log("optionalTypes: " + optionalTypes.Count);
        if (optionalTypes.Count <= 0)
        {
            optionalTypes = new List<string>(ImageData.GetIntTypes());
        }
        Debug.Log("optionalTypes: " + optionalTypes.Count);

        foreach (string imgName in logList)
        {
            ImageData imgData = DrawingPanel.GetImgData(imgName);
            Debug.Log("imgData.Int: " + imgData.Int + ", " + usedImagesImgAttribute[imgData.Int].Count);
            if (optionalTypes.Contains(imgData.Int))
            {
                if(usedImagesImgAttribute[imgData.Int].Count <= 0)
                {
                    optionalImages.Add(imgName);
                }
                else if (!usedImagesImgAttribute[imgData.Int].Contains(imgData.Img))
                {
                    optionalImages.Add(imgName);
                }
            }
        }

        return optionalImages;
    }

    /*static public List<string> GetImages()
    {
        //string filename = Application.dataPath + "/Data/leavesList.csv";
        string filename = Application.persistentDataPath + "/Data/leavesList.csv";
        Debug.Log(filename);
        var logFile = File.ReadAllLines(filename);
        var logList = new List<string>(logFile);

        var startLogList = new List<string>(logList);
        foreach (string item in usedImages)
        {
            string drawing = item.Split('_')[0].ToString();
            foreach (string _item in startLogList)
            {
                if (_item.Contains(drawing))
                {
                    logList.Remove(_item);
                }
            }
        }
        return logList;
    }*/

    // Update is called once per frame
}