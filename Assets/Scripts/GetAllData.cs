using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetAllData : MonoBehaviour
{
    static public bool descriptionBeforeDrawing;

    public InputField age;
    public List<Toggle> mailFemail = new List<Toggle>(); 
    public List<Toggle> handType = new List<Toggle>(); 
    public List<Toggle> withExperience = new List<Toggle>();
    public GameObject redPart;
    public GameObject thingsToRead;

    static public bool readed;

    static public List<Toggle>[] saveInformation = new List<Toggle>[3];
    static public string saveAge;

    // Start is called before the first frame update

    private void Start()
    {
        Debug.Log("Start: " + readed);

        thingsToRead.GetComponent<Toggle>().isOn = readed;

        List<Toggle>[] listOfall = new List<Toggle>[3];
        listOfall[0] = mailFemail;
        listOfall[1] = handType;
        listOfall[2] = withExperience;
        for (int i = 0; i < listOfall.Length; i++)
        {
            if(saveInformation != null)
            {
                listOfall[i][0].isOn = saveInformation[i][0].isOn;
                listOfall[i][1].isOn = saveInformation[i][1].isOn;
            }
        }
        if(saveAge != null)
        {
            age.text = saveAge;
        }

        for (int i = 0; i < redPart.transform.childCount; i++)
        {
            EnableOrDisable(false, i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoneMaetData()
    {
        List<Toggle>[] listOfall = new List<Toggle>[3];
        listOfall[0] = mailFemail;
        listOfall[1] = handType;
        listOfall[2] = withExperience;

        string[] radioTypes = new string[] { "Female", "Male", "Left Hand", "Right Hand", "Without Experience", "With Experience" };
        List<string> endCsv = new List<string>();

        bool everyThingHadDone = true;
        if (age.text.Length > 0 && age.text != "0")
        {
            endCsv.Add(age.text);
        }
        else
        {
            everyThingHadDone = false;
            StartCoroutine(MakeRedForTime(0));
        }

        if (!thingsToRead.GetComponent<Toggle>().isOn)
        {
            everyThingHadDone = false;
            StartCoroutine(MakeRedForTime(4));
        }
        for (int i = 0; i < listOfall.Length; i += 1)
        {
            if (listOfall[i][0].isOn)
            {
                endCsv.Add(radioTypes[i * 2]);
            }
            else if (listOfall[i][1].isOn)
            {
                endCsv.Add(radioTypes[i * 2 + 1]);
            }
            else
            {
                everyThingHadDone = false;
                StartCoroutine(MakeRedForTime(i+1));
            }
        }

        if (everyThingHadDone)
        {
            SetAllMetaData.metaData = endCsv;
            GetType();
        }
    }

    static public void GetType()
    {
        string imageType = DrawingPanel.GetImgData(GetImage.imageName).Int;
        Debug.Log(GetImage.imageName + imageType);
        GetImage.isFIL = false;
        if (imageType == "CMG")
        {
            if (Random.Range(0f, 1f) > 0.5f)
            {
                descriptionBeforeDrawing = true;
                SceneManager.LoadScene(3);
            }
            else
            {
                descriptionBeforeDrawing = false;
                SceneManager.LoadScene(4);
            }
        }
        else if (imageType == "COG")
        {
            descriptionBeforeDrawing = true;
            SceneManager.LoadScene(4);
        }
        else if (imageType == "FIL")
        {
            GetImage.isFIL = true;
            descriptionBeforeDrawing = true;
            SceneManager.LoadScene(4);
        }
    }

    IEnumerator MakeRedForTime(int childIndex)
    {

        EnableOrDisable(false, childIndex);
        yield return new WaitForSeconds(0.08f);
        EnableOrDisable(true, childIndex);

        yield return new WaitForSeconds(2f);

        EnableOrDisable(false, childIndex);
    }

    void EnableOrDisable(bool e_D, int childIndex)
    {
        redPart.transform.GetChild(childIndex).GetComponent<Image>().enabled = e_D;

        for (int i = 0; i < redPart.transform.GetChild(childIndex).childCount; i++)
        {
            GameObject moreTo = redPart.transform.GetChild(i).gameObject;
            if (moreTo.GetComponent<Text>())
            {
                moreTo.GetComponent<Text>().enabled = e_D;
            }
            if (moreTo.GetComponent<Image>())
            {
                moreTo.GetComponent<Image>().enabled = e_D;
            }
        }
    }

    public void ReadedPressed()
    {
        saveAge = age.text;

        saveInformation = new List<Toggle>[3];
        saveInformation[0] = mailFemail;
        saveInformation[1] = handType;
        saveInformation[2] = withExperience;
        readed = thingsToRead.GetComponent<Toggle>().isOn;
        Debug.Log("ReadedPressed: " + readed);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
