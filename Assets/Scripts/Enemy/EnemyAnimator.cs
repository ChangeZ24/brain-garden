using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public EnemyAtk1 atk;
    private Animator anima;
    private int count=1;
    void Start()
    {
        anima = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (count==1 && atk.nowTime >= 9.7f)
        {
            anima.SetTrigger("atk");
            count = 0;
        }
        if (atk.nowTime>1&&atk.nowTime<2)
        {
            count = 1;
        }
    }
}
