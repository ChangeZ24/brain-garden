using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRender : MonoBehaviour
{
    public static MapRender instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    public Transform playerStartPos;
    public int playerAreaCount;
    public int bossAreaCount;



    public float playerAreaRate;//玩家占据区域比例，可用于计算判别胜负
    public int[,] mapData = new int[3200, 1800];
    //
    public Transform mapLeftDown;//矩形地图左下角位置参考
    public Transform mapRightUp;//矩形地图右上角位置参考
    public int mapHeight;
    public int mapWidth;

    public LineRenderer playerLineRenderer;
    public LineRenderer bossMoveLineRenderer;

    public List<LineRenderer> fireBallLinesList = new List<LineRenderer>();

    private int totalAreaValue;//用于计算区域比例

    //玩家起始位置，以屏幕中心为坐标参考
    public int playerStartX;
    public int playerStartY;

    public float gameTimeCounter;//用于设置新痕迹的z轴位置--必须设置游戏时间在一定时间内
    private void Start()
    {
        //初始设置为0
        mapHeight = 1800;
        mapWidth = 3200;
        totalAreaValue = 1080000;//
        gameTimeCounter = 60;//初始位置要与背景初始z位置，时间对应,当位置越过摄像机的z轴时，消失
        ResetMap(0);
    }

    public bool isGameEnd;
    private void Update()
    {
        if (isGameEnd)
            return;
        //UpdateTimeDisplay();
        gameTimeCounter -= Time.deltaTime;
        playerAreaRate = (float)playerAreaCount / (float)totalAreaValue;
        //游戏结束检测
        if(gameTimeCounter<=0)
        {
            isGameEnd = true;
            //暂停游戏时间
            Time.timeScale = 0;
            //时间结束，进行判定
            if(playerAreaRate<0.3f)
            {
                //伤心
                GameObject.Find("GameEndUI").GetComponent<GameEndUI>().SetTargetUIActive(2);
            }
            else if(playerAreaRate < 0.7f)
            {
                //一般（平静）
                GameObject.Find("GameEndUI").GetComponent<GameEndUI>().SetTargetUIActive(1);
            }
            else
            {
                //开心
                GameObject.Find("GameEndUI").GetComponent<GameEndUI>().SetTargetUIActive(0);
            }
        }


    }

    public void GameStart()
    {

        Time.timeScale = 1;
        isGameEnd = false;
        ResetMap(0);
        //player移动到起点位置
        GameObject.Find("Player").gameObject.transform.position= playerStartPos.position;
        gameTimeCounter = 60;
        totalAreaValue = 1080000;//不精准，但问题不大
        playerAreaRate = 0;
        playerAreaCount = 0;
    }
    public void ResetMap(int startData)
    {
        for (int i = 0; i < mapWidth; i++)
            for (int j = 0; j < mapHeight; j++)
            {
                mapData[i, j] = startData;
            }
    }

    /// <summary>
    /// 玩家每一帧调用,radius为玩家路径宽度
    /// </summary>
    /// <param name="centerx"></param>
    /// <param name="centery"></param>
    /// <param name="radius"></param>
    public void SetPointAsPlayerArea(int centerx, int centery, float radius)
    {
        /*Debug.Log("X" + centerx + " " + "Y" + centery);
        Debug.Log("MX" + mapWidth + " " + "MY" + mapHeight);*/
        //根据中心坐标，和半径，对目标位置附近数据进行处理
        //检测范围内数据，如果是空白区域，给玩家面积++，如果是boss区域，则boss面积--，如果是玩家区域，啥也不做
        if (centerx < 0 || centerx > mapWidth - 1)
            return;
        if (centery < 0 || centery > mapHeight - 1)
            return;


        //Debug.Log("当前玩家染色面积" + playerAreaCount);
        //Debug.Log("当前玩家染色面积" + playerAreaCount);
        int leftCheckAreaIndex = (int)Mathf.Max(0, centerx - radius);
        int rightCheckAreaIndex = (int)Mathf.Min(centerx + radius, mapWidth - 1);
        int upIndex = (int)Mathf.Min(centery + radius, mapHeight - 1);
        int downIndex= (int)Mathf.Max(centery - radius, 0);
        for(int i=leftCheckAreaIndex;i<=rightCheckAreaIndex;i++)
            for(int j = downIndex; j <= upIndex; j++)
            {
                //检测该小矩形范围内的点是否符合条件
                if (!IsInRadius(centerx, centery, radius, i, j))
                    continue;
                //空白
                if (mapData[i, j] == 0)
                    playerAreaCount += 1;
                //玩家区域
                else if (mapData[i, j] == 1)
                {
                    return;
                }
                //boss区域
                else if (mapData[i, j] == 2)
                {
                    playerAreaCount += 1;
                    bossAreaCount --;
                }
                mapData[i, j] = 1;
            }


    }
    public bool IsInRadius(int x,int y,float radius,int checkX,int checkY)
    {
        float distance = Mathf.Sqrt((checkX-x)* (checkX - x)+(checkY-y)* (checkY - y));
        if (distance <= radius)
            return true;
        return false;
    }

    public void SetPointAsBossArea(int centerx, int centery, float radius)
    {
        //根据中心坐标，和半径，对目标位置附近数据进行处理

        //检测范围内数据，如果是空白区域，给玩家面积++，如果是boss区域，则boss面积--，如果是玩家区域，啥也不做
        if (centerx < 0 || centerx > mapWidth - 1)
            return;
        if (centery < 0 || centery > mapHeight - 1)
            return;
        int leftCheckAreaIndex = (int)Mathf.Max(0, centerx - radius);
        int rightCheckAreaIndex = (int)Mathf.Min(centerx + radius, mapWidth - 1);
        int upIndex = (int)Mathf.Min(centery + radius, mapHeight - 1);
        int downIndex = (int)Mathf.Max(centery - radius, 0);
        for (int i = leftCheckAreaIndex; i <= rightCheckAreaIndex; i++)
            for (int j = downIndex; j <= upIndex; j++)
            {
                //空白
                if (mapData[i, j] == 0)
                    bossAreaCount += 1;
                //玩家区域
                else if (mapData[i, j] == 1)
                {
                    playerAreaCount -=1;
                    bossAreaCount++;
                }
                //boss区域
                else if (mapData[i, j] == 2)
                {
                    return;
                }
                mapData[i, j] = 2;
            }


    }


    //火球攻击绘制轨迹
    public GameObject linesRendererPrefab;
    public LineRenderer AddFireBallLineRenderer(/*BossFireBall bfb*/)
    {
        GameObject g = Instantiate(linesRendererPrefab, this.transform);
        fireBallLinesList.Add(g.GetComponent<LineRenderer>());
        return fireBallLinesList[fireBallLinesList.Count - 1];
    }
}
