    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtk1 : MonoBehaviour
{
    public float maxTime;
    public float nowTime;
    public GameObject[] Bullet;
    public GameObject[] atkPoint;
    public AudioSource audioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nowTime += Time.deltaTime;
        if (nowTime>=maxTime)
        {
            //ÊÍ·Å¼¼ÄÜ
            WriteAtkForWard();
            nowTime = 0;
        }
    }
    public void WriteAtkForWard()
    {
        for (int i = 0; i < atkPoint.Length; i++)
        {
            int num = Random.Range(1, 5);
            GameObject obj = Instantiate(Resources.Load<GameObject>("Bullet" + num));
            obj.transform.position = atkPoint[i].transform.position;
            obj.transform.rotation = atkPoint[i].transform.rotation;
            audioSource.Play();
        }
    }
    public void WriteAtkBack()
    {

    }
}
