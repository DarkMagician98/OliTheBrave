using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SceneType { StartScreen, LevelScene }
public class CursorScript : MonoBehaviour
{

    [SerializeField] private SceneType scene;
    [SerializeField] private GameObject cursor;
    private Sprite defaultCursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
       // defaultCursor = cursor.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {

        if (scene == SceneType.StartScreen)
        {
            cursor.transform.position = Input.mousePosition;
        }
        else if (scene == SceneType.LevelScene)
        {
            Vector3 point = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            cursor.transform.position = new Vector2(point.x, point.y);
        }
    }
}
