using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftTimeDisplay : MonoBehaviour
{
    public Text timeDisplay;
    public float leftTime;

    public void Update()
    {
        if(MapRender.instance.isGameEnd==false)
        {
            //游戏中，更新剩余时间显示
            leftTime = MapRender.instance.gameTimeCounter;
            int t = (int)leftTime;
            timeDisplay.text = (t / 60).ToString("D2") + ":" + (t % 60).ToString("D2");
        }
    }
}
