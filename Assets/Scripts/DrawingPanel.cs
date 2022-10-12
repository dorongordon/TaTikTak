using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Threading.Tasks;


public class DrawingPanel : MonoBehaviour
{

    static public string[] dataTypes = new string[] { "Int", "Img", "Rep", "Gen", "Sub" };

    public Camera m_camera;
    public GameObject brush;

 //   private List<List<Vector2>> positions;
    private int strokeNumber;
    private List<GameObject> strokes;
    private List<List<Vector3>> allData;
    public static string publicImageName;

    private float timeFromStart;
    private DateTime startDateTime;
    private WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();


    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    private void Start()
    {
        startDateTime = DateTime.Now;
        timeFromStart = 0;
        strokeNumber = -1;
        strokes = new List<GameObject>();
        allData = new List<List<Vector3>>();
        allData.Clear();
    }

    private void Update()
    {
        Drawing();
    }

    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && MousHover())
        {
            strokeNumber += 1;
            allData.Add(new List<Vector3>());

            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0) && MousHover())
        {
            timeFromStart = (float)(DateTime.Now - startDateTime).TotalMilliseconds;
            allData[allData.Count - 1].Add(new Vector3(Input.mousePosition.x, Input.mousePosition.y, timeFromStart));
            //Debug.Log(Input.mousePosition.x + "," + Input.mousePosition.y);
            PointToMousePos();
        }
        else
        {
            currentLineRenderer = null;
        }
    }

    void CreateBrush()
    {
       // komticNumber += 1;
       // positions.Add(new List<Vector2>());

        GameObject brushInstance = Instantiate(brush);
        brushInstance.transform.parent = gameObject.transform;
        brushInstance.name = "brush" + strokeNumber.ToString();
        strokes.Add(brushInstance);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //because you gotta have 2 points to start a line renderer, 
        Vector3 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0.1f;

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        Vector3 mousePos = new Vector3(pointPos.x, pointPos.y, 0.1f);
        currentLineRenderer.SetPosition(positionIndex, mousePos);

       // positions[komticNumber].Add(pointPos);
    }

    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }

    }

    bool MousHover()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    public void Undo()
    {
        if(strokeNumber >= 0)
        {
            Destroy(strokes[strokeNumber]);
            strokes.Remove(strokes[strokeNumber]);
            strokeNumber -= 1;
        }
        allData.Add(new List<Vector3>());
        allData[allData.Count - 1].Add(new Vector3(-100, -100, -100));
    }

    public void DeletAll()
    {
        foreach (var stroke in strokes)
        {
            Destroy(stroke);
        }
        strokes.Clear();
        strokeNumber = -1;

        allData.Add(new List<Vector3>());
        allData[allData.Count - 1].Add(new Vector3(-1000, -1000, -1000));
    }

    static public ImageData GetImgData(string imgName)
    {
        ImageData data = new ImageData();
        data.SetDataFromString(imgName);
        return data;
    }
    
    static public string SetImgName(ImageData imgData)
    {
        return imgData.GetStringFromData();
    }



    public IEnumerator saveToImageFile()
    {
        yield return frameEnd;

        var rectTransform = GetComponent<RectTransform>();
        int width = (int)rectTransform.rect.width;// 1280;// (int)rectTransform.sizeDelta.x;
        int height = (int)rectTransform.rect.height;//800;// (int)rectTransform.sizeDelta.y;


        var startX = 1090;// rectTransform.rect.position.x; // (int)Math.Round(-442.64 - (843.61f/2));
        var startY = 270;// rectTransform.rect.position.y; // (int)Math.Round(843.61 - (843.61f/2));
        width = 1735 - startX;
        height = 900 - startY;

        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        Rect rex = new Rect(startX, startY, width, height);

        tex.ReadPixels(rex, 0, 0);
        tex.Apply();

        // Encode texture into PNG
        var bytes = tex.EncodeToPNG();
        Destroy(tex);

        Debug.Log(GetImage.imageName);
        ImageData imgData = GetImgData(GetImage.imageName);
        imgData.Gen += 1; // increase gen
        string newName = SetImgName(imgData);
        Debug.Log(newName);
        publicImageName = newName;
        SetAllMetaData.Done(newName);

        Debug.Log(Application.dataPath);
        System.IO.File.WriteAllBytes(GetImage.userDataPath + newName + ".png", bytes);

        var logFile = File.ReadAllLines(GetImage.leavesDataPath + "leavesList.csv");
        var logList = new List<string>(logFile);
        logList[GetImage.imageIndex] = newName;

        File.WriteAllLines(GetImage.leavesDataPath + "leavesList.csv", logList);
    }

    public void Done()
    {
        // Convert allData to string list
        StartCoroutine(saveToImageFile());

        List<string> lines = new List<string>();
        lines.Add("Time, Stroke, Point, x, y");
        for (int i = 0; i < allData.Count; i++)
        {   // go over all stroke
            for (int j = 0; j < allData[i].Count; j++)
            {   // go over all points in a stroke
                if (allData[i][j] == new Vector3(-100, -100, -100))
                {
                    lines.Add("Undo, " + i + ", " + j + ", , ");
                }
                else if (allData[i][j] == new Vector3(-1000, -1000, -1000))
                {
                    lines.Add("DeletAll, " + i + ", " + j + ", , ");
                }
                else
                {
                    lines.Add(allData[i][j].z.ToString() + ", " + i + ", " + j + ", " + allData[i][j].x.ToString() + ", " + allData[i][j].y.ToString());
                }
            }
        }
        // get file name
        ImageData imgData = GetImgData(GetImage.imageName);
        imgData.Gen += 1; // increase gen
        string newName = SetImgName(imgData);

        string filename = newName + "_way_of_drawing.csv";
        // save string list to file
        File.WriteAllLines(GetImage.userDataPath + filename, lines.ToArray());
        SendClient server = GameObject.FindObjectOfType<SendClient>();
        if (server)
        {
            server.SendPublicFile(lines);
        }

    }

    public void NextPage()
    {
        if (!GetAllData.descriptionBeforeDrawing)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }
}
