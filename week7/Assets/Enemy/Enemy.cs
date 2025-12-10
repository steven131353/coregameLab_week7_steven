using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("移动速度")]
    public float xSpeed = -3f;   // 往左负数，往右正数
    public float ySpeed = 0f;    // 往下负数，往上正数

    public enum RespawnMode
    {
        RightToLeft,   // 从右飞到左，再从右边重生
        LeftToRight,   // 从左飞到右，再从左边重生
        TopToBottom,   // 从上飞到下，再从上边重生
        BottomToTop    // 从下飞到上，再从下边重生
    }

    [Header("循环模式")]
    public RespawnMode respawnMode = RespawnMode.RightToLeft;

    [Header("水平循环参数")]
    public float leftLimit = -10f;    // x 小于这个值时重生
    public float rightLimit = 10f;    // x 大于这个值时重生
    public float minY = -4f;          // y 随机范围
    public float maxY = 4f;

    [Header("垂直循环参数")]
    public float bottomLimit = -6f;   // y 小于这个值时重生
    public float topLimit = 6f;       // y 大于这个值时重生
    public float minX = -8f;          // x 随机范围
    public float maxX = 8f;

    private HurtSound hurtSound;

    void Start()
    {
        hurtSound = FindObjectOfType<HurtSound>();

        if (hurtSound == null)
        {
            Debug.LogWarning("[Enemy] 在场景中找不到 HurtSound 组件！");
        }
    }

    void Update()
    {
        // 敌人移动
        transform.position += new Vector3(xSpeed, ySpeed, 0f) * Time.deltaTime;

        // 根据不同模式判断什么时候重生
        switch (respawnMode)
        {
            case RespawnMode.RightToLeft:
                if (transform.position.x < leftLimit)
                    RespawnFromRight();
                break;

            case RespawnMode.LeftToRight:
                if (transform.position.x > rightLimit)
                    RespawnFromLeft();
                break;

            case RespawnMode.TopToBottom:
                if (transform.position.y < bottomLimit)
                    RespawnFromTop();
                break;

            case RespawnMode.BottomToTop:
                if (transform.position.y > topLimit)
                    RespawnFromBottom();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("[Enemy] 撞到玩家，准备调用 AddHit()");

            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.AddHit();
            }
            else
            {
                Debug.LogWarning("[Enemy] 找不到 GameManager！AddHit 无法调用。");
            }

            if (hurtSound != null)
            {
                hurtSound.DisplayARandomHurtText();
            }
        }
    }

    // ===========================
    //    四种方向的重生函数
    // ===========================

    void RespawnFromRight()
    {
        float randomY = Random.Range(minY, maxY);
        transform.position = new Vector3(rightLimit, randomY, 0f);
        Debug.Log("[Enemy] 从右侧重生");
    }

    void RespawnFromLeft()
    {
        float randomY = Random.Range(minY, maxY);
        transform.position = new Vector3(leftLimit, randomY, 0f);
        Debug.Log("[Enemy] 从左侧重生");
    }

    void RespawnFromTop()
    {
        float randomX = Random.Range(minX, maxX);
        transform.position = new Vector3(randomX, topLimit, 0f);
        Debug.Log("[Enemy] 从上方重生");
    }

    void RespawnFromBottom()
    {
        float randomX = Random.Range(minX, maxX);
        transform.position = new Vector3(randomX, bottomLimit, 0f);
        Debug.Log("[Enemy] 从下方重生");
    }
}
