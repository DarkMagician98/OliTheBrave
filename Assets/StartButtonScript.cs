using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{

    AudioManager am;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();

        if (am.isPlaying("bg") == false)
        {
            am.queueSound("bg");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gotoCredits()
    {
        am.queueSound("click");
        SceneManager.LoadScene("CreditScene");
    }

    public void quitGame()
    {
        am.queueSound("click");
        Application.Quit();
    }

    public void gotoLevels()
    {
        am.queueSound("click");
        SceneManager.LoadScene("LevelScene");
    }
}
