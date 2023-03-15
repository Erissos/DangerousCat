using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamSys : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 offset;
    public Transform Player;

    private float _camSize;
    private Camera cam;

    public float fullSize;
    public float minSize;


    private void Start()
    {
        cam = GetComponent<Camera>();
        _camSize = cam.orthographicSize;
    }




    private void Update()
    {
        camZoom();
        followCam();
    }

    private void camZoom()
    {


        if(Mathf.Abs(cam.orthographicSize - _camSize)> 0.1)
        {
            float change = Mathf.Lerp(cam.orthographicSize, _camSize, Time.deltaTime * 2);
            cam.orthographicSize = change;
        }

        if(Input.mouseScrollDelta.y > 0 && _camSize > minSize)
        {
            _camSize -= 1;
        }

        else if (Input.mouseScrollDelta.y < 0 && _camSize < fullSize)
        {
            _camSize += 1;
        }
        

    }

    public void followCam()
    {
        transform.position = Vector3.Lerp(transform.position, Player.transform.position + offset, moveSpeed * Time.deltaTime);
    }
}

//DragonCubeGames