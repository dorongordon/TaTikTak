using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReverseText : MonoBehaviour
{

    [SerializeField] private string visableText;

    // Start is called before the first frame update
    void Awake()
    {
        Text text = GetComponent<Text>();

        string newText = "";
        if (visableText == null || visableText.Length <= 0)
        {
            return;
        }
        for (int i = visableText.Length - 1; i < 0; i--)
        {
            if (visableText[i].ToString() ==  ";")
            {
                newText = newText + "/n";
            }
            newText = newText + visableText[i];
        }

        text.text = newText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
