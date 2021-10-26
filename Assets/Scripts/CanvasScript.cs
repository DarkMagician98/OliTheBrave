using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject dialogueMenu;
    [SerializeField] private GameObject cameraParent;

    private Canvas canvas;
    DialogueManager dialogueManager;

    AudioManager am;

   // private static  CanvasScript instance;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
       /* if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);*/
        canvas = GetComponent<Canvas>();
      //  canvas.cam
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < cameraParent.transform.childCount - 1; i++)
        {
            if (cameraParent.transform.GetChild(i).gameObject.activeSelf)
            {
                canvas.worldCamera = cameraParent.transform.GetChild(i).gameObject.GetComponent<Camera>();
            }
        }
      //  cameraParent.transform.get
       /* foreach(GameObject obj in cameraParent)
        {
            if (obj.activeSelf)
            {
                Debug.Log("Active");
                canvas.worldCamera = obj.GetComponent<Camera>();
              //  break;
            }
            Debug.Log("Inactive");
        }*/
       // dialogueManager = FindObjectOfType<DialogueManager>();

       /* if (dialogueManager.hasDialogue())
        {
            dialogueMenu.SetActive(true);
        }
        else
        {
            dialogueMenu.SetActive(false);
        }*/
    }

    public void activatePauseMenu()
    {
            am.queueSound("click");
            Time.timeScale = Time.timeScale > 0 ? 0 : 1;
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            pauseButton.SetActive(!pauseButton.activeSelf);
        
    }

    public void resumeGame()
    {
        am.queueSound("click");
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        pauseButton.SetActive(!pauseButton.activeSelf);
    }

    public void leaveLevel()
    {
        am.queueSound("click");
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        pauseButton.SetActive(!pauseButton.activeSelf);
        SceneManager.LoadScene("LevelScene"); 
    }
}
