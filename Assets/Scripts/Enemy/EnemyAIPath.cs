using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIPath : MonoBehaviour
{
    public AIPath aiPath;
    private SpriteRenderer EnemySprite;
    private void Awake()
    {
        EnemySprite = this.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        #region 敌人ai寻路角度转变
        if (aiPath.desiredVelocity.x>=0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        #endregion
    }
}
