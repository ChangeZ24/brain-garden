using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpeed;
    public Vector3 FirePoint;
    public int count = 1;
    public PlayerController move;
    private void Awake()
    {
        Destroy(this.gameObject, 10);
        move = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (count==1)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                count=0;
                move.MoveSpeedChange(1.8f);
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (count==0)
        {
            count=1;
        }
    }
    void Update()
    {
        this.transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime, Space.Self);
    }
}
