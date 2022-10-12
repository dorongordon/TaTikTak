using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Threading.Tasks;

public class GetDataOfDescribments : MonoBehaviour
{
    public bool[] completeTheImage = new bool[2];
    public bool describeThePicture;
    public GameObject redParts;

    private List<string> letters;
    private List<float> time;

    private DateTime startDateTime;

    private string TextUntilHere;
    public float timeFromStart;

    private int currentNumberOfLetters;
    private int beforeCurrentNumberOfLetters;

    // Start is called before the first frame update
    void Start()
    {
        currentNumberOfLetters = 0;
        beforeCurrentNumberOfLetters = 0;
        MakeRed(false);
        TextUntilHere = "";

        timeFromStart = 0;
        startDateTime = DateTime.Now;
        letters = new List<string>();
        time = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        currentNumberOfLetters = GetComponent<InputField>().text.ToCharArray().Length;
    }

    public void Describements(string letter)
    {
        beforeCurrentNumberOfLetters += 1;
        if (beforeCurrentNumberOfLetters > currentNumberOfLetters)
        {
            letters.Add("Deleted");
            beforeCurrentNumberOfLetters -= 1;
        }
        else
        {
            Char[] newLetter = GetComponent<InputField>().text.ToCharArray();
            letters.Add(newLetter[newLetter.Length - 1].ToString());//[GetComponent<InputField>().text.Length - 1].ToString());
        }
        timeFromStart = (float)(DateTime.Now - startDateTime).TotalMilliseconds;
        time.Add(timeFromStart);
    }

    public void DoneDescribements()
    {
        if (currentNumberOfLetters > 0)
        {
            Reverse();
            List<string> csvText = new List<string>();
            csvText.Add("Time, Letter, Describments");
            csvText.Add(time[0] + ", " + letters[0] + ", " + GetComponent<InputField>().text);
            for (int i = 1; i < letters.Count - 1; i++)
            {
                csvText.Add(time[i] + ", " + letters[i]);
            }
            ImageData _name = DrawingPanel.GetImgData(GetImage.imageName);
            _name.Gen += 1;

            string filename = GetImage.userDataPath + DrawingPanel.SetImgName(_name) + "_PictureDescribment.csv";
            // save string list to file
            File.WriteAllLines(filename, csvText.ToArray());
            SendClient server = GameObject.FindObjectOfType<SendClient>();
            if (server)
            {
                server.SendPublicFile(csvText);
            }
            if (GetAllData.descriptionBeforeDrawing)
            {
                SceneManager.LoadScene(4);
            }
            else
            {
                SceneManager.LoadScene(6);
            }
        }
        else
        {
            StartCoroutine(MakeRedForTime());
        }
    }

    void MakeRed(bool _do)
    {
        for (int i = 0; i < redParts.transform.childCount; i++)
        {
            if (redParts.transform.GetChild(i).GetComponent<Text>())
            {
                redParts.transform.GetChild(i).GetComponent<Text>().enabled = _do;
            }
            if (redParts.transform.GetChild(i).GetComponent<Image>())
            {
                redParts.transform.GetChild(i).GetComponent<Image>().enabled = _do;
            }
        }
    }

    IEnumerator MakeRedForTime()
    {
        MakeRed(false);
        yield return new WaitForSeconds(0.08f);
        MakeRed(true);

        yield return new WaitForSeconds(2f);

        MakeRed(false);
    }

    public void Reverse()
    {
        char[] charArray = GetComponent<InputField>().text.ToCharArray();
        Array.Reverse(charArray);
        string reversedText = new string(charArray);
        Debug.Log("REVERSE: text " + GetComponent<InputField>().text);
        Debug.Log("REVERSE: reversed text " + reversedText);
        GetComponent<InputField>().text = reversedText;
    }
}
