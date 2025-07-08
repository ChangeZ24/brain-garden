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
            //��Ϸ�У�����ʣ��ʱ����ʾ
            leftTime = MapRender.instance.gameTimeCounter;
            int t = (int)leftTime;
            timeDisplay.text = (t / 60).ToString("D2") + ":" + (t % 60).ToString("D2");
        }
    }
}
