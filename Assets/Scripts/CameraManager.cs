using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    ArrayList cameraList;
    Camera currentCamera;
    [SerializeField] GameObject player;
    [SerializeField] GameObject cursor;
    [SerializeField] Sprite teleportCursor;
    private bool isTeleportCursorSet;
    private Sprite defaultCursor;
    private Vector3 defaultScale;


    // private static CameraManager instance;
    void Start()
    {
        Cursor.visible = false;
        defaultCursor = cursor.GetComponent<Image>().sprite;
        defaultScale = cursor.transform.localScale;

        cameraList = new ArrayList();

        foreach(Transform child in transform)
        {

            Camera camChild = child.GetComponent<Camera>();
           // Debug.Log("Camera looping" + camChild.aspect);
           /* if(camChild.aspect >= 1.595 && camChild.aspect <= 1.70)
            {
              //  Debug.Log("In");
                camChild.orthographicSize = 2.8f;
            }*/
            camChild.gameObject.SetActive(false);
            cameraList.Add(child);
        }

        Transform cam = cameraList[0] as Transform;
        cam.GetComponent<Camera>().gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovement>().canTeleport() && !isTeleportCursorSet)
        {
            isTeleportCursorSet = true;
            cursor.GetComponent<Image>().sprite = teleportCursor;
        }

        if(!player.GetComponent<PlayerMovement>().canTeleport() && isTeleportCursorSet)
        {
            isTeleportCursorSet = false;
            cursor.GetComponent<Image>().sprite = defaultCursor;
            cursor.transform.localScale = defaultScale;
        }

        foreach(Transform camera in cameraList)
        {
            Camera cameraComp = camera.GetComponent<Camera>();

            if (player != null)
            {
                Vector3 cameraView = cameraComp.WorldToViewportPoint(player.transform.position);
             
                if (IsBetween(cameraView.x, 0, 1) && IsBetween(cameraView.y, 0, 1) && cameraView.z > 0)
                {
                    currentCamera = cameraComp;
                    cameraComp.gameObject.SetActive(true);
                }
                else
                {
                    cameraComp.gameObject.SetActive(false);
                }
            }
        }
        // Vector3 point = currentCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        //  cursor.transform.position = new Vector2(point.x, point.y);
        cursor.transform.position = Input.mousePosition;
    }

    bool IsBetween(double testValue, double bound1, double bound2)
    {
        return (testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2));
    }
}
