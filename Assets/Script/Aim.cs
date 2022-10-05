using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    int zoom = 20;
    int normal = 60;
    float smooth = 5;

    private bool isZoomed = false;

    int zoomFPS = 10;
    int normalFPS = 60;
    float smoothFPS = 5;

    public bool isZoomedFPS = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && isZoomedFPS == false)
        {
            isZoomed = !isZoomed;

        }

        if (isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);

        } else
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);

        }

        if (Input.GetButtonDown("FPS") && isZoomed == false)
        {
            isZoomedFPS = !isZoomedFPS;
        }

        if (isZoomedFPS)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoomFPS, Time.deltaTime * smoothFPS);
            GameObject.Find("Ch50").GetComponent<SkinnedMeshRenderer>().enabled = false;
            GameObject.Find("barrell").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Chip").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("scope").GetComponent<SkinnedMeshRenderer>().enabled = false;

        }
        else
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normalFPS, Time.deltaTime * smoothFPS);
            GameObject.Find("Ch50").GetComponent<SkinnedMeshRenderer>().enabled = true;
            GameObject.Find("barrell").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Chip").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("scope").GetComponent<SkinnedMeshRenderer>().enabled = true;

        }

    }




}
