using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendClient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SendPublicFile(List<string> fileString)
    {
        StartCoroutine(SendPrivetData(fileString));
    }



    public IEnumerator SendPrivetData(List<string> file)
    {
        string fileText = file[0];
        for (int i = 1; i < file.Count; i++)
        {
            fileText = fileText + "/p + file[i]";
        }

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080", fileText);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
