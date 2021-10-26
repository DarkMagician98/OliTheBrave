using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera camera;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
     //  Debug.Log(camera.WorldToViewportPoint(target.position));
    }
}
