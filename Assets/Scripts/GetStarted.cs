using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetStarted : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void GetStartedNow()
   {
        GetAllData.readed = false;
        GetImage.usedImages = new List<string>();
        GetAllData.saveAge = null;
        GetAllData.saveInformation = null;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
