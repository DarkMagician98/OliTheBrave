using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PortraitType { EIFFEL,PISA};
public class PortraitScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    SpriteManager spriteManager;
    [SerializeField] PortraitType portraitType;

    void Start()
    {
       // DontDestroyOnLoad(gameObject);
        spriteManager = FindObjectOfType<SpriteManager>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (portraitType == PortraitType.EIFFEL)
        {
            spriteManager.setSpritePointingRight();
        }
        else if (portraitType == PortraitType.PISA)
        {
            spriteManager.setSpritePointingLeft();
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        spriteManager.setSpritePointingNowhere();
    }

}
