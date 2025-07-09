using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeControl : MonoBehaviour
{
    private int moveX;
    public PlayerController player;
    private Animator anima;
    void Start()
    {
        anima = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.moveX!=0||player.moveY!=0)
        {
            anima.SetInteger("move", 1);
        }
        else if (player.moveX == 0 && player.moveY == 0)
        {
            anima.SetInteger("move", 0);
        }
    }
}
