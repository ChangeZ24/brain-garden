using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClear : MonoBehaviour
{
    public PlayerController player;
    private AIDestinationSetter aiPoint;
    public Transform target;
    
    private void Awake()
    {
        aiPoint = this.GetComponent<AIDestinationSetter>();
    }
    private void Update()
    {
        ViewCanMove();
    }
    private void ViewCanMove()
    {
        if (player.isMove==false)
        {
            aiPoint.target = target;
        }
        else
        {
            aiPoint.target = player.transform;
        }
    }
    public float stopPlayerMoveTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            player.isMove = false;
            player.GetComponent<Animator>().Play("idle");
            player.GetComponent<PlayerController>().StopMoveInput(stopPlayerMoveTime);
            player.GetComponent<PlayerController>().GetHit();
        }
    }
}
