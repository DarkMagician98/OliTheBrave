using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditCanvasScript : MonoBehaviour
{

    AudioManager am;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leaveCredits()
    {
        am.queueSound("click");
        SceneManager.LoadScene("StartScreen");
    }
}
