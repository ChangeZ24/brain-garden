using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
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
    void Start()
    {
        
    }

    void Update()
    {
        #region 玩家移动
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

        #region 移动动画播放
        //当movex或movey值改变时，更改条件，播放动画，可能使用2d混合。
        playerAnimator.SetFloat("moveX", moveX);
        playerAnimator.SetFloat("moveY", moveY);
        #endregion

        #region 胜利与否条件判断
        
        #endregion
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
