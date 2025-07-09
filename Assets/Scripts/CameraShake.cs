// 注意：该脚本目前可能未被项目使用，仅供后续确认或参考。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraShake : MonoBehaviour
{
    private Cinemachine.CinemachineCollisionImpulseSource MyInpulse;

    private void Start()
    {
        MyInpulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
        shakeTimeCounter = shakeInterval;
    }

    public float shakeInterval;
    public float shakeTimeCounter;


    private void Update()
    {
        shakeTimeCounter += Time.deltaTime;
        
        //按下右键产生相机抖动，抖动方式依照上面CM vcam1 Raw Signal内配置信息
        if (MapRender.instance.gameTimeCounter < 10f && !MapRender.instance.isGameEnd)
        {
            if(shakeTimeCounter>shakeInterval)
            {
                shakeTimeCounter = 0;
                Vector3 randValue = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
                MyInpulse.GenerateImpulse(randValue);
            }
            
        }
    }
}