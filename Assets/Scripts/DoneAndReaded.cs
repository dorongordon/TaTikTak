using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoneAndReaded : MonoBehaviour
{
    public Toggle readed;
    public GameObject redPart;

    // Start is called before the first frame update
    void Start()
    {
      //  readed.isOn = false;
      //  EnableOrDisable(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Done()
    {
    /*    if (readed.isOn)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            StartCoroutine(MakeRedForTime());
        }*/

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

   /* IEnumerator MakeRedForTime()
    {

        EnableOrDisable(false);
        yield return new WaitForSeconds(0.08f);
        EnableOrDisable(true);

        yield return new WaitForSeconds(3f);

        EnableOrDisable(false);
    }

    void EnableOrDisable(bool e_D)
    {
        redPart.GetComponent<Image>().enabled = e_D;

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

    public void Changed()
    {
        EnableOrDisable(false);
    }*/
}
