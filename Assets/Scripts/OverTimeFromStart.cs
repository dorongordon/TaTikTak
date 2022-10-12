using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverTimeFromStart : MonoBehaviour
{
    private bool stillHere;

    private float time;
    private bool truetrue;

    public Text textDanger;
    public GameObject timerPrefab;

    public List<string> warnings = new List<string>();

    public int firstTimeDelay = 10;
    public int secondTimeDelay = 20;
    public int thirdTimeDelay = 30;

    public bool isParent;

    // Start is called before the first frame update
    void Start()
    {
        if (isParent)
        {
            GameObject timer = Instantiate(timerPrefab.gameObject, transform);
            timer.transform.parent = transform;
        }
        else
        {
            truetrue = true;
            time = 0;
            GetComponent<Canvas>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isParent)
        {
            NotParent();
        }
    }

    void NotParent()
    {
        time += Time.deltaTime;

        if (time >= firstTimeDelay)
        {
            GetComponent<Canvas>().enabled = true;
            if (time >= firstTimeDelay + secondTimeDelay)
            {
                textDanger.GetComponent<Text>().text = warnings[1];
                if (time >= firstTimeDelay + secondTimeDelay + thirdTimeDelay)
                {
                    SceneManager.LoadScene(0);
                }
            }
            else
            {
                textDanger.GetComponent<Text>().text = warnings[0];
            }
        }
        else
        {
            GetComponent<Canvas>().enabled = false;
        }

        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)) && !GetComponent<Canvas>().enabled)
        {
            time = 0;
        }
    }

    public void Continue()
    {
        time = 0;
    }

    public void FromStart()
    {
        SceneManager.LoadScene(0);
    }
}