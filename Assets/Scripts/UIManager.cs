using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //References 
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject levelTitle;
    [SerializeField] private List<GameObject> hearts;

    public void UpdateLevel(int level)
    {
        {
            levelText.text = level.ToString();
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateHealth(int health)
    {
        if (health < hearts.Count && health >= 0)
            hearts[health].SetActive(false);
        else if (health < hearts.Count)
        {
            for (int i = 0; i < health - 1; i++)
            {
                if (!hearts[i].activeSelf)
                    hearts[health - 1].SetActive(true);
            }
        }
    }
}
