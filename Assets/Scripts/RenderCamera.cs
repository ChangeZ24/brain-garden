using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCamera : MonoBehaviour
{
    public Transform mainCamera;

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        
        this.transform.position = mainCamera.position;
    }
}
