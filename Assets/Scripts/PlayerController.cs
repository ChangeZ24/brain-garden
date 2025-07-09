using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public MapRender mapRender;
    private LineRenderer lineRenderer;
    public bool isMove;
    public float drawRadius;
    public AudioSource stopSource;
    public AudioSource speedSource;

    private void Start()
    {
        MapRender.instance.GameStart();
        //允许移动
        isCanMove = true;
        this.gameObject.transform.position = MapRender.instance.playerStartPos.position;

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

        if (isMove == false)
        {
            nowTime += Time.deltaTime;
        }
        if (nowTime > cantMoveTime)
        {
            isMove = true;
            nowTime = 0;
        }
        //游戏结束，不可在进行交互与移动
        if (MapRender.instance.isGameEnd)
            return;

        //修改地图数据
        MapRender.instance.SetPointAsPlayerArea((int)(this.transform.position.x * 100 + MapRender.instance.mapWidth * 0.5f), (int)(this.transform.position.y * 100 + MapRender.instance.mapHeight * 0.5f), drawRadius);



        if (isSlow == true && nowSlowTime <= slowTime)
        {
            nowSlowTime += Time.deltaTime;
        }
        else if (nowSlowTime > slowTime)
        {
            nowSlowTime = 0;
            isSlow = false;
            MoveSpeedReturn(1.8f);
        }



        #region 移动动画播放
        //当movex或movey值改变时，更改条件，播放动画，可能使用2d混合。
        playerAnimator.SetFloat("moveX", moveX);
        playerAnimator.SetFloat("moveY", moveY);
        #endregion

        #region 胜利与否条件判断

        #endregion
        if (Time.timeScale == 0)
            return;
        if (!isCanMove)
            return;

        Move();
    }

    private void FixedUpdate()
    {
       // Move();
    }

    //public Transform mainCamera;

    public bool isCanMove;  //用于禁止玩家移动输入
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
        if (moveX > 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            speedDropBuffDisplay.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveX < 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            speedDropBuffDisplay.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveX==0&&moveY==0)
        {
            this.transform.localScale = new Vector3(1, 1, 1); 
            speedDropBuffDisplay.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveY!=0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            speedDropBuffDisplay.transform.localScale = new Vector3(1, 1, 1);
        }
        Vector2 p = this.transform.position;
        p.x += moveX * moveSpeed * Time.deltaTime;
        p.y += moveY * moveSpeed * Time.deltaTime;
        transform.position = p;
        #endregion
    }
    
    /// <summary>
    /// 根据传入time，对玩家做出持续时间为time的禁锢效果。
    /// </summary>
    private bool hasStopFunc;//确保只进行一次停止移动协程
    public void StopMoveInput(float time)
    {
        if (hasStopFunc == true)
            return;
        hasStopFunc = true;
        StartCoroutine(MoveStop(time));
        //触发一次受击GitGit
        GetHit();
    }

    private IEnumerator MoveStop(float t)
    {
        if(stopSource != null)
            stopSource.Play();
        isCanMove = false;
        yield return new WaitForSeconds(t);
        isCanMove = true;
        hasStopFunc = false;
    }



    public float XYDistance(Vector3 v1, Vector3 v2)
    {

        float xDis = v1.x - v2.x;
        float yDist = v1.y - v2.y;
        /*Debug.Log("v1" + v1);
        Debug.Log("V2" + v2);
        Debug.Log("NExtPointDistance" + Mathf.Sqrt(xDis * xDis + yDist * yDist));*/
        return Mathf.Sqrt(xDis * xDis + yDist * yDist);
    }

    public void GetHit()
    {
        //红光闪烁
        GameObject.Find("ScreenBorder").GetComponent<ScreenBorder>().DoFlashOnce();
    }


    //全给你粘喽

    /// <summary>
    /// 玩家移动速度
    /// </summary>
    public float moveSpeed;
    public float moveX;
    public float moveY;
    public float cantMoveTime;
    public float nowTime;
    private bool isSlow;
    public float slowTime;
    private float nowSlowTime;
    private Animator playerAnimator;
    private SpriteRenderer PlayerSprite;
    private void Awake()
    {
        playerAnimator = this.GetComponent<Animator>();
        PlayerSprite = this.GetComponent<SpriteRenderer>();
        SoundManager.instance.BgAudio();
    }


    public GameObject speedDropBuffDisplay;

    /// <summary>
    /// boss对玩家移速的减少
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void MoveSpeedChange(float speed)
    {
        GetHit();
        //头顶显示减速
        speedDropBuffDisplay.SetActive(true);
        speedSource.Play();
        this.moveSpeed -= speed;
        isSlow = true;
    }
    public void MoveSpeedReturn(float moveSpeed)
    {
        //头顶关闭减速
        speedDropBuffDisplay.SetActive(false);
        this.moveSpeed += moveSpeed;
    }
}
