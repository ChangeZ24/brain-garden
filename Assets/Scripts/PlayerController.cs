using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public MapRender mapRender;
    private LineRenderer lineRenderer;

    public float drawRadius;

    private void Start()
    {
        lineRenderer = MapRender.instance.playerLineRenderer;
        //绘制线条
        /*lineRenderer.startWidth = drawRadius/100;
        lineRenderer.endWidth = drawRadius / 100;*/
        lineRenderer.startWidth = 0;
        lineRenderer.endWidth = 0;
    }

    public Vector3 curPosWithZ;
    private void Update()
    {
        //如果当前点与上一个不同,添加新点
        if (lineRenderer.positionCount == 0)
        {
            lineRenderer.positionCount += 1;
            curPosWithZ = new Vector3(this.transform.position.x/*+6.8f*/, this.transform.position.y/*+2.5f*/, MapRender.instance.gameTimeCounter);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, curPosWithZ);
        }
        else if (XYDistance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), this.transform.position) > 0.01f)
        {
            lineRenderer.positionCount += 1;
            curPosWithZ = new Vector3(this.transform.position.x /*+ 6.8f*/, this.transform.position.y/* + 2.5f*/, MapRender.instance.gameTimeCounter);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, curPosWithZ);
        }
        //修改地图数据
        MapRender.instance.SetPointAsPlayerArea((int)(this.transform.position.x*100+ MapRender.instance.mapWidth*0.5f), (int)(this.transform.position.y * 100 + MapRender.instance.mapHeight*0.5f), drawRadius);
        Move();
        

        #region 移动动画播放
        //当movex或movey值改变时，更改条件，播放动画，可能使用2d混合。
        playerAnimator.SetFloat("moveX", moveX);
        playerAnimator.SetFloat("moveY", moveY);
        #endregion

        #region 胜利与否条件判断

        #endregion
    }

    private void FixedUpdate()
    {
       // Move();
    }

    //public Transform mainCamera;

    private void  Move()
    {
        #region 弃用的测试代码--移动
        /*if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + Time.fixedDeltaTime,
                MapRender.instance.gameTimeCounter);
            //
       //mainCamera.position = new Vector3(mainCamera.position.x, mainCamera.position.y + Time.fixedDeltaTime, mainCamera.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - Time.fixedDeltaTime,
                MapRender.instance.gameTimeCounter);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = new Vector3(this.transform.position.x- Time.fixedDeltaTime, this.transform.position.y ,
                MapRender.instance.gameTimeCounter);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = new Vector3(this.transform.position.x+ Time.fixedDeltaTime, this.transform.position.y,
                MapRender.instance.gameTimeCounter);
        }*/
        #endregion

        #region 玩家移动
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        if (moveX < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveX > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        if (moveY < 0)
        {
            PlayerSprite.sprite = forWardSprite;
        }
        else if (moveY > 0)
        {
            PlayerSprite.sprite = backSprite;
        }
        Vector2 p = this.transform.position;
        p.x += moveX * moveSpeed * Time.deltaTime;
        p.y += moveY * moveSpeed * Time.deltaTime;
        transform.position = p;
        #endregion
    }
    public float XYDistance(Vector3 v1,Vector3 v2)
    {
        
        float xDis = v1.x-v2.x;
        float yDist = v1.y - v2.y;
        /*Debug.Log("v1" + v1);
        Debug.Log("V2" + v2);
        Debug.Log("NExtPointDistance" + Mathf.Sqrt(xDis * xDis + yDist * yDist));*/
        return Mathf.Sqrt(xDis * xDis + yDist * yDist);
    }

    //全给你粘喽

    /// <summary>
    /// 玩家移动速度
    /// </summary>
    public float moveSpeed;
    private float moveX;
    private float moveY;
    private Animator playerAnimator;
    private SpriteRenderer PlayerSprite;
    public Sprite forWardSprite;
    public Sprite backSprite;
    private void Awake()
    {
        playerAnimator = this.GetComponent<Animator>();
        PlayerSprite = this.GetComponent<SpriteRenderer>();
    }
    

    /// <summary>
    /// boss对玩家移速的减少
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void MoveSpeedChange(float speed)
    {
        this.moveSpeed -= speed;
    }
    public void MoveSpeedReturn(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }








}
