using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("视觉效果")]
    public float rotateSpeed = 0f;          // 让小球自己慢慢转（不想转就设为 0）

    [Header("反馈效果（可选）")]
    public AudioSource collectSound;        // 拖一个带音效的 AudioSource
    public ParticleSystem collectEffect;    // 拖一个粒子特效

    private bool collected = false;

    void Update()
    {
        // 让小球慢慢转一转（可选）
        if (rotateSpeed != 0f)
        {
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // 只对 Player 有效
        if (!collected && col.CompareTag("Player"))
        {
            collected = true;

            // 调用 GameManager，加一分
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.AddCollect();
            }
            else
            {
                Debug.LogWarning("[Collectible] 找不到 GameManager，无法加分！");
            }

            // 播放音效
            if (collectSound != null)
            {
                collectSound.Play();
            }

            // 播放粒子特效
            if (collectEffect != null)
            {
                collectEffect.transform.parent = null;  // 脱离小球
                collectEffect.Play();
                Destroy(collectEffect.gameObject, collectEffect.main.duration);
            }

            // 销毁小球本体
            Destroy(gameObject);
        }
    }
}
