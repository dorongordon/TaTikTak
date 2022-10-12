using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SetAllMetaData : MonoBehaviour
{
    static public List<string> metaData;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    static public void Done(string _name)
    {
        List<string> lines = new List<string>();

        lines.Add("Age, Sex, Hand, Experience");
        lines.Add("");
        foreach (string item in metaData)
        {
            lines[1] = lines[1] + item + ", ";
        }

        string filename = _name + "_meta_data.csv";
        // save string list to file
        File.WriteAllLines(GetImage.userDataPath + filename, lines.ToArray());
        SendClient server = GameObject.FindObjectOfType<SendClient>();
        if (server)
        {
            server.SendPublicFile(lines);
        }
    }
}
