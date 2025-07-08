using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// ����ƶ��ٶ�
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
    void Start()
    {
        
    }

    void Update()
    {
        #region ����ƶ�
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        if (moveX<0)
        {
            this.transform.localScale = new Vector3(-1,1,1);
        }
        else if(moveX>0)
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

        #region �ƶ���������
        //��movex��moveyֵ�ı�ʱ���������������Ŷ���������ʹ��2d��ϡ�
        playerAnimator.SetFloat("moveX", moveX);
        playerAnimator.SetFloat("moveY", moveY);
        #endregion

        #region ʤ����������ж�
        
        #endregion
    }

    /// <summary>
    /// boss��������ٵļ���
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
