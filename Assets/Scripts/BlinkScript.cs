using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlinkScript : MonoBehaviour
{

    //public TMPro.TextMeshProUGUI textController;

    SpriteRenderer spriteRenderer;
    [SerializeField] float blinkTime = .5f;
    float currentTime = .5f;
    // public text;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("bg");
        // FindObjectOfType<AudioManager>().Play("background");
        // textController = GetComponent<TMPro.TextMeshPro>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(blink());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("LevelScene");
        }
        // Debug.Log("playing..." + Time.time);
        // textController.text = "Press Space to Play";
    }

    IEnumerator blink()
    {
        while (true)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkTime);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkTime);
        }
    }
}
