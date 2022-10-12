using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImageData
{
    public string Int;
    public int Img;
    public int Rep;
    public int Gen;
    public string Sub;
    public bool Part;

    public static List<string> GetIntTypes()
    {
        List<string> IntTypes = new List<string>();
        IntTypes.Add("COG");
        IntTypes.Add("CMG");
        IntTypes.Add("FIL");
        return IntTypes;
    }

    public void SetDataFromString(string imgName)
    {
        Part = false;

        string[] imgNameData = imgName.Split('_');
        for (int i = 0; i < imgNameData.Length; i += 2)
        {
            switch (imgNameData[i])
            {
                case "Int":
                    Int = imgNameData[i + 1]; break;
                case "Img":
                    Img = Convert.ToInt32(imgNameData[i + 1]);  break;
                case "Rep":
                    Rep = Convert.ToInt32(imgNameData[i + 1]); break;
                case "Gen":
                    Gen = Convert.ToInt32(imgNameData[i + 1]); break;
                case "Sub":
                    Sub = imgNameData[i + 1]; break;
                case "PART":
                    Part = true; break;
            }
        }
    }

    public string GetStringFromData()
    {
        string imgName = "Int_" + Int;
        imgName += "_Img_" + String.Format("{0:D3}", Img);
        if (Part)
        {
            imgName += "_PART";
            imgName += "_Sub_" + Sub;
        }
        else
        {
            imgName += "_Rep_" + String.Format("{0:D3}", Rep);
            imgName += "_Gen_" + String.Format("{0:D3}", Gen);
            imgName += "_Sub_" + Sub;
        }
        return imgName;
    }
}