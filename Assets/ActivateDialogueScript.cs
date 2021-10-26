using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogueScript : MonoBehaviour
{
    // Start is called before the first frame update
    private string key;
    [SerializeField] DialogueScript dialogue;
    private bool isToBeDestroyed;
    private DialogueManager dialogueManager;

    void Start()
    {
        key = dialogue.key;

        if (!PlayerPrefs.HasKey(key)){
            PlayerPrefs.SetString(key, "false");
        }
        else
        {
            if(PlayerPrefs.GetString(key) == "true")
            {
                Destroy(this.gameObject);
            }
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isToBeDestroyed)
        {
            dialogueManager.addDialogue(dialogue);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (PlayerPrefs.GetString(key) == "false")
            {
               
                PlayerPrefs.SetString(key, "true");
                StartCoroutine(activateNewDialogue());
            }
        }
    }

    IEnumerator activateNewDialogue()
    {
        //Debug.Log(PlayerPrefs.GetString(key));
        yield return new WaitForSeconds(.10f);
        dialogueManager.activateDialogue();
        isToBeDestroyed = true;
    }

    private void OnDisable()
    {
     
    }
}
