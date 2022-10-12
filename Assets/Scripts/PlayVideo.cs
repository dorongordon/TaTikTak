using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite playImage;
    public Sprite pauseImage;
    public GameObject pausePlayButton;

    public RectTransform canvas;
    public VideoPlayer video;

    private Slider tracking;
    private bool slide = false;

    private Vector2[] littleScreenPos;

    private bool bigScreen;

    void Awake()
    {
        video.Play();
        video.Pause();
        video.frame = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        bigScreen = false;
        littleScreenPos = new Vector2[2];
     /*   try
        {
            littleScreenPos[0] = canvas.rect.position;
            littleScreenPos[1] = canvas.rect.size;
        } catch
        {
            Debug.Log("Error:" + canvas);
        }*/

        tracking = GetComponent<Slider>();
    }

    public void OnPointerDown(PointerEventData a)
    {
        slide = true;
    }
    
    public void OnPointerUp(PointerEventData a)
    {
        float frame = (float)tracking.value * (float)video.frameCount;
        video.frame = (long)frame;
        slide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!slide && video.isPlaying)
        {
            tracking.value = (float)video.frame / (float)video.frameCount;
        }

        if (video.isPlaying)
        {
            pausePlayButton.GetComponent<Image>().sprite = pauseImage;
        }
        else
        {
            pausePlayButton.GetComponent<Image>().sprite = playImage;
        }
    }

/*    public void GetBigScreen()
    {
        if (!bigScreen)
        {
            canvas.rect.position.Set(0f, 0f);
            canvas.rect.size.Set(3f, 10f);
        }
        
        if (bigScreen)
        {
            canvas.rect.position.Set(littleScreenPos[0][0], littleScreenPos[0][1]);
            canvas.rect.size.Set(littleScreenPos[1][0], littleScreenPos[1][1]);
        }
    }*/

    public void PlayPause()
    {
        if (video.isPlaying)
        {
            video.Pause();
        }
        else
        {
            video.Play();
        }
    }

    public void Done()
    {
        SceneManager.LoadScene(0);
    }

}
