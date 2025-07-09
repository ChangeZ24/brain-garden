using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public Transform goPoint;
    public Transform fromPoint;
    public float flySpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, goPoint.position, flySpeed * Time.deltaTime);
        if (this.transform.position == goPoint.position)
        {
            this.transform.position = fromPoint.position;
        }
    }
}
