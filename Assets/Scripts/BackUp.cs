using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackUp : MonoBehaviour
{
    public GameObject redPart;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < redPart.transform.childCount; i++)
        {
            EnableOrDisable(false, i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        redPart.GetComponent<Image>().enabled = e_D;

        if (redPart.transform.childCount > 0)
        {
            for (int i = 0; i < redPart.transform.childCount; i++)
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
    }

    public void Next()
    {
        GetImage.usedImages.Add(GetImage.imageName);
        GetImage.optionalTypes.Remove(DrawingPanel.GetImgData(GetImage.imageName).Int);

        if (GetImage._data.GetNextImg().Count > 0)
        {
            GameObject.FindObjectOfType<GetImage>().Done(true);
            GetAllData.GetType();
        }
        else
        {
            for (int i = 0; i < redPart.transform.childCount; i++)
            {
                StartCoroutine(MakeRedForTime(i));
            }
        }
    }

    public void Enough()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
