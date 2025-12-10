using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("游戏目标")]
    public int targetCollectCount = 10; // 必须收集的小球数量
    public int maxHits = 5;             // 最大可被敌人撞击次数

    [Header("当前进度")]
    public int currentCollect = 0;
    public int currentHits = 0;

    [Header("UI 引用")]
    public TextMeshProUGUI collectText;
    public TextMeshProUGUI hitText;
    public GameObject winPanel;
    public GameObject losePanel;

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        UpdateUI();
    }

    void UpdateUI()
    {
        if (collectText != null)
            collectText.text = "Collected: " + currentCollect + " / " + targetCollectCount;

        if (hitText != null)
            hitText.text = "Hits: " + currentHits + " / " + maxHits;
    }

    public void AddCollect()
    {
        currentCollect++;
        Debug.Log("[GameManager] AddCollect 被调用, 当前收集数量 = " + currentCollect);
        UpdateUI();

        if (currentCollect >= targetCollectCount)
        {
            WinGame();
        }
    }

    public void AddHit()
    {
        currentHits++;
        Debug.Log("[GameManager] AddHit 被调用, 当前 hit 次数 = " + currentHits);
        UpdateUI();

        if (currentHits >= maxHits)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        Debug.Log("[GameManager] WinGame 被触发");
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void LoseGame()
    {
        Debug.Log("[GameManager] LoseGame 被触发");
        if (losePanel != null) losePanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
