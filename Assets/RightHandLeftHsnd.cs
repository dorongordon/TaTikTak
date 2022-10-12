using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandLeftHsnd : MonoBehaviour
{
    public GameObject rightPart;
    public GameObject leftPart;

    // Start is called before the first frame update
    void Awake()
    {
        string side = "";
        foreach (string item in SetAllMetaData.metaData)
        {
            side = side + item;
        }
        if (side.Contains("Left"))
        {
            GameObject attractionPartSide = Instantiate(leftPart, transform);
            attractionPartSide.transform.parent = transform;
            attractionPartSide.transform.name = "LeftAttractionPart";
        }
        else
        {
            GameObject attractionPartSide = Instantiate(rightPart, transform);
            attractionPartSide.transform.parent = transform;
            attractionPartSide.transform.name = "RightAttractionPart";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
