using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public float drawRadius;

    private void Start()
    { 
        lineRenderer=MapRender.instance.AddFireBallLineRenderer();
        //lineRenderer = this.GetComponent<LineRenderer>();
        //����ʱ�����һ����
        lineRenderer.startWidth = drawRadius;
        lineRenderer.endWidth = drawRadius;
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, 
            new Vector3(this.transform.position.x,this.transform.position.y,MapRender.instance.gameTimeCounter));
    }
    //
    public void FixedUpdate()
    {
        FireBallMove();
        //ÿ���ƶ�ʱ����lineRender
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1,
            new Vector3(this.transform.position.x, this.transform.position.y, MapRender.instance.gameTimeCounter));
    }

    public void FireBallMove()
    {
        //���ݷ����ƶ�
        //ray���߼���Ƿ�����ǽ��
        Vector3 nextpos = this.transform.position + moveDir.normalized * moveSpeed;
        this.transform.position =new Vector3(nextpos.x,nextpos.y,MapRender.instance.gameTimeCounter-0.1f);//ע�⣬�����ʵ��������λ�ã�������59.9sʱ���������򣨴�ʱ�����Ѿ�Խ�������λ��
        //���ֻ������ͼ��Ⱦʼ�ո��ڹ켣
        
    }


    public float moveSpeed;
    public Ray moveDirectionRay;
    public Vector3 moveDir;
    public void SetInfo(Vector3 dir,float radius)
    {
        //moveDirection = dir;
        moveDir = dir;
        drawRadius = radius;
        /*lineRenderer.startWidth = radius;
        lineRenderer.endWidth = radius;*/
    }

    //test������Ļ����
    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
