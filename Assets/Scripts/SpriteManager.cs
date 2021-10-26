using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpriteManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SpriteManager instance;
    [SerializeField] GameObject OliSprite;
    [SerializeField] GameObject oliPointLeft, oliPointRight, oliPointNowhere;
    [SerializeField] Sprite opl, opr, opn;
    [SerializeField] GameObject lockObj;

    private bool isUnlock;
    Image oliImage;

    AudioManager am;

    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
    }


    void Start()
    {

        if (OliSprite != null)
        {
            oliImage = OliSprite.GetComponent<Image>();
        }
        else
        {
            GameObject obj = Instantiate(OliSprite);
            obj.transform.position = new Vector3(999, 999, 999);
            oliImage = OliSprite.GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(!isUnlock && PlayerPrefs.GetString("unlock") == "true")
        {
            isUnlock = true;
            lockObj.SetActive(false);
        }


    }

    public void setSpritePointingLeft()
    {
        
       
        if (oliPointLeft != null)
        {
          //  GameObject obj = Instantiate(oliPointLeft);
           // obj.transform.position = new Vector3(999, 999, 999);
            if (oliImage != null)
            {
                oliImage.sprite = oliPointLeft.GetComponent<SpriteRenderer>().sprite;
            }
          //  oliImage.sprite = obj.GetComponent<SpriteRenderer>().sprite;
          //  obj.SetActive(false);
            // obj.transform.position = new Vector3(9999, 9999, 9999);
          //  oliImage.sprite = obj.GetComponent<SpriteRenderer>().sprite;
        }

    }

    public void setSpritePointingRight()
    {
        if (oliPointRight != null)
        {
           // GameObject obj = Instantiate(oliPointRight);
           // obj.transform.position = new Vector3(999, 999, 999);
            if (oliImage != null)
            {
                oliImage.sprite = oliPointRight.GetComponent<SpriteRenderer>().sprite;
            }
           // oliImage.sprite = obj.GetComponent<SpriteRenderer>().sprite;
            //  obj.SetActive(false);
            // obj.transform.position = new Vector3(9999, 9999, 9999);
            //  oliImage.sprite = obj.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void setSpritePointingNowhere()
    {
        if (oliPointNowhere != null)
        {
           // GameObject obj = Instantiate(oliPointNowhere);
         //   obj.transform.position = new Vector3(999, 999, 999);

            if(oliImage != null)
            {
              oliImage.sprite = oliPointNowhere.GetComponent<SpriteRenderer>().sprite;
            }
            //  obj.SetActive(false);
            // obj.transform.position = new Vector3(9999, 9999, 9999);
            //  oliImage.sprite = obj.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void exitLevel()
    {
        am.queueSound("click");  
        SceneManager.LoadScene("StartScreen");   
    }

    public void gotoEiffelScene()
    {
        am.queueSound("click");
        setSpritePointingNowhere();
        SceneManager.LoadScene("SampleScene");
    }

    public void gotoPisaScene()
    {
            am.queueSound("click");
            setSpritePointingNowhere();
            SceneManager.LoadScene("PisaScene");
    }
}


