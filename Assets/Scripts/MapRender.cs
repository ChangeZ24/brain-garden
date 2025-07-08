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



    public float playerAreaRate;//���ռ����������������ڼ����б�ʤ��
    public int[,] mapData = new int[3200, 1800];
    //
    public Transform mapLeftDown;//���ε�ͼ���½�λ�òο�
    public Transform mapRightUp;//���ε�ͼ���Ͻ�λ�òο�
    public int mapHeight;
    public int mapWidth;

    public LineRenderer playerLineRenderer;
    public LineRenderer bossMoveLineRenderer;

    public List<LineRenderer> fireBallLinesList = new List<LineRenderer>();

    private int totalAreaValue;//���ڼ����������

    //�����ʼλ�ã�����Ļ����Ϊ����ο�
    public int playerStartX;
    public int playerStartY;

    public float gameTimeCounter;//���������ºۼ���z��λ��--����������Ϸʱ����һ��ʱ����
    private void Start()
    {
        //��ʼ����Ϊ0
        /*mapHeight = Mathf.Abs((int)((mapRightUp.position.y-mapLeftDown.position.y)*100));
        mapWidth = Mathf.Abs((int)((mapRightUp.position.x - mapLeftDown.position.x) * 100));*/
        totalAreaValue = 1080000;//mapHeight * mapWidth;
        //
        gameTimeCounter = 60;//��ʼλ��Ҫ�뱳����ʼzλ�ã�ʱ���Ӧ,��λ��Խ���������z��ʱ����ʧ
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
        //��Ϸ�������
        if(gameTimeCounter<=0)
        {
            isGameEnd = true;
            //ʱ������������ж�
            if(playerAreaRate<0.3f)
            {
                //����
                GameObject.Find("GameEndUI").GetComponent<GameEndUI>().SetTargetUIActive(2);
            }
            else if(playerAreaRate < 0.6f)
            {
                //һ�㣨ƽ����
                GameObject.Find("GameEndUI").GetComponent<GameEndUI>().SetTargetUIActive(1);
            }
            else
            {
                //����
                GameObject.Find("GameEndUI").GetComponent<GameEndUI>().SetTargetUIActive(0);
            }
        }


    }


    /*private void UpdateTimeDisplay()
    {
        //ʣ��ʱ���UI��ʾ
    }*/

    public void GameStart()
    {
        isGameEnd = false;
        ResetMap(0);
        //player�ƶ������λ��
        GameObject.Find("Player").gameObject.transform.position= playerStartPos.position;
        gameTimeCounter = 60;
        totalAreaValue = 1080000;//����׼�������ⲻ��
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
    /// ���ÿһ֡����,radiusΪ���·�����
    /// </summary>
    /// <param name="centerx"></param>
    /// <param name="centery"></param>
    /// <param name="radius"></param>
    public void SetPointAsPlayerArea(int centerx, int centery, float radius)
    {
        /*Debug.Log("X" + centerx + " " + "Y" + centery);
        Debug.Log("MX" + mapWidth + " " + "MY" + mapHeight);*/
        //�����������꣬�Ͱ뾶����Ŀ��λ�ø������ݽ��д���
        //��ⷶΧ�����ݣ�����ǿհ����򣬸�������++�������boss������boss���--��������������ɶҲ����
        if (centerx < 0 || centerx > mapWidth - 1)
            return;
        if (centery < 0 || centery > mapHeight - 1)
            return;


        //Debug.Log("��ǰ���Ⱦɫ���" + playerAreaCount);
        int leftCheckAreaIndex = (int)Mathf.Max(0, centerx - radius);
        int rightCheckAreaIndex = (int)Mathf.Min(centerx + radius, mapWidth - 1);
        int upIndex = (int)Mathf.Min(centery + radius, mapHeight - 1);
        int downIndex= (int)Mathf.Max(centery - radius, 0);
        for(int i=leftCheckAreaIndex;i<=rightCheckAreaIndex;i++)
            for(int j = downIndex; j <= upIndex; j++)
            {
                //����С���η�Χ�ڵĵ��Ƿ��������
                if (!IsInRadius(centerx, centery, radius, i, j))
                    continue;
                //�հ�
                if (mapData[i, j] == 0)
                    playerAreaCount += 1;
                //�������
                else if (mapData[i, j] == 1)
                {
                    return;
                }
                //boss����
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
        //�����������꣬�Ͱ뾶����Ŀ��λ�ø������ݽ��д���

        //��ⷶΧ�����ݣ�����ǿհ����򣬸�������++�������boss������boss���--��������������ɶҲ����
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
                //�հ�
                if (mapData[i, j] == 0)
                    bossAreaCount += 1;
                //�������
                else if (mapData[i, j] == 1)
                {
                    playerAreaCount -=1;
                    bossAreaCount++;
                }
                //boss����
                else if (mapData[i, j] == 2)
                {
                    return;
                }
                mapData[i, j] = 2;
            }


    }


    //���򹥻����ƹ켣
    public GameObject linesRendererPrefab;
    public LineRenderer AddFireBallLineRenderer(/*BossFireBall bfb*/)
    {
        GameObject g = Instantiate(linesRendererPrefab, this.transform);
        fireBallLinesList.Add(g.GetComponent<LineRenderer>());
        return fireBallLinesList[fireBallLinesList.Count - 1];
    }
}
