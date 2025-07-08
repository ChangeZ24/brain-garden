using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public MapRender mapRender;
    private LineRenderer lineRenderer;

    /*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mapRender.SetPointAsBossArea((int)Input.mousePosition.x, (int)Input.mousePosition.y, 50f);
           
            lineRenderer.positionCount += 1;
            Debug.Log(Input.mousePosition.x+","+ Input.mousePosition.y);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, 
                
                new Vector3(*//*Input.mousePosition.x-960, Input.mousePosition.y-540, *//*
                    this.transform.position.x+0.5f,this.transform.position.y + 0.5f, mapRender.gameTimeCounter));
        }
    }
*/

    public float drawRadius;
    private void Start()
    {
        drawRadius = 0.5f;
        jumpHeight = 1f;
        //轨迹渲染参数
        //lineRenderer = this.transform.GetComponent<LineRenderer>();
        lineRenderer = MapRender.instance.bossMoveLineRenderer;
        lineRenderer.startWidth = drawRadius;
        lineRenderer.endWidth = drawRadius;
        playerAnimator = this.GetComponent<Animator>();
    }
    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.Space))
            Skill01();
        if (Input.GetKeyDown(KeyCode.Q)) 
            BossFire();
    }

    //baseInfo

    public int bossState;//boss状态，0静止，1移动，2技能，3跳跃
    public Animator playerAnimator;

    //otherInfo
    public Transform playerTransform;


    private IEnumerator bossJump_ie;
    public void Skill01()
    {
        //切换状态到跳跃状态
        if (bossState == 3)
            return;
        bossState =3;
        bossJump_ie = BossJump();
        StartCoroutine(bossJump_ie); 
    }

    public float timeForPrepareJumping;
    private float jumpHeight;   //y轴跳跃偏移
    private float jumpHeightProgressCounter;//跳跃过程计时,1s达到目标点
    private IEnumerator BossJump()
    {
        //
        yield return new WaitForSeconds(timeForPrepareJumping);
        //逐渐跃起到目标高度位置
        playerAnimator.SetTrigger("JumpUp");
        Vector3 startPos = this.transform.position;//存储起跳点
        Vector3 targetPos = playerTransform.position;//存储起跳目标点
        Vector3 distanceVec = targetPos - startPos;//两者距离与方向
        //起跳，1s后达到 距离/2 +y轴偏移
        while (jumpHeightProgressCounter <= 1)
        {
            jumpHeightProgressCounter += Time.fixedDeltaTime;
            this.transform.position = Vector3.Lerp(
                startPos,
                new Vector3(distanceVec.x/2+startPos.x, targetPos.y + jumpHeight, this.transform.position.z),
                jumpHeightProgressCounter);
            yield return null;
        }

        //可以在这里重新获取玩家位置
        playerAnimator.SetTrigger("JumpDown");
        //跳跃到最高点，下落
        jumpHeightProgressCounter = 0;//开始
        startPos = this.transform.position;//获取在空中的开始下落位置
        while (jumpHeightProgressCounter <= 1)
        {
            jumpHeightProgressCounter += Time.fixedDeltaTime;
            this.transform.position = Vector3.Lerp(
                startPos,
                targetPos,
                jumpHeightProgressCounter);
            yield return null;
        }
        //落地时进行一次检测，检测附近是否有玩家，或者直接用碰撞体碰撞检测
        //
        playerAnimator.SetTrigger("JumpEnd");
        //重置参数
        StopCoroutine(bossJump_ie);
        jumpHeightProgressCounter = 0;
        //状态退回到静止
        bossState = 0;
    }

    //boss行走轨迹涂色
    public void BossWalk()
    {
        //跳跃时不染色
        if (bossState == 3)
            return;

        lineRenderer.positionCount += 1;
       
        lineRenderer.SetPosition(
            lineRenderer.positionCount - 1,
            //在boss脚底渲染
            new Vector3(this.transform.position.x , this.transform.position.y, mapRender.gameTimeCounter));
    }

    //喷出的火焰球体
    public GameObject fireBallPrefab;
    //
    public float fireBallDrawRadius;
    public void BossFire()
    {
        //在boss脚底生成火球
        GameObject g= Instantiate(fireBallPrefab, this.transform.position, this.transform.rotation);
        //设置火球方向
        g.GetComponent<BossFireBall>().SetInfo(playerTransform.position - this.transform.position, fireBallDrawRadius);
    }
}
