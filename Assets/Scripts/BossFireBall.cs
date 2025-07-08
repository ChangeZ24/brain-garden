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
        //生成时赋予第一个点
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
        //每次移动时更新lineRender
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1,
            new Vector3(this.transform.position.x, this.transform.position.y, MapRender.instance.gameTimeCounter));
    }

    public void FireBallMove()
    {
        //根据方向移动
        //ray射线检测是否碰到墙体
        Vector3 nextpos = this.transform.position + moveDir.normalized * moveSpeed;
        this.transform.position =new Vector3(nextpos.x,nextpos.y,MapRender.instance.gameTimeCounter-0.1f);//注意，可以适当调整相机位置，避免在59.9s时看不见火球（此时火球已经越过了相机位置
        //保持火球的贴图渲染始终高于轨迹
        
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

    //test，出屏幕销毁
    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
