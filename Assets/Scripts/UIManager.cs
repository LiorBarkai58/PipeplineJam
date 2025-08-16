using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //References 
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private CanvasGroup levelTitle;
    [SerializeField] private List<GameObject> hearts;

    private bool updatedLevel = false;
    public void UpdateLevel(int level)
    {
        {
            levelText.text = level.ToString();
            updatedLevel = false;
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
    }

    public void UpdateHealthHeal(int health)
    {
        for (int i = 0; i < health; i++)
        {
            if (!hearts[i].activeSelf)
                hearts[health].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player) && !updatedLevel)
        {
            FadeInAndOut(1.5f,4);
            updatedLevel = true;

        }
    }

    public void FadeInAndOut(float fadeDuration, float waitTime)
    {
        StartCoroutine(FadeInOutRoutine(levelTitle ,fadeDuration, waitTime));
    }

    private IEnumerator FadeInOutRoutine(CanvasGroup canvasGroup, float fadeDuration, float waitTime)
    {
        // Fade in
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, fadeDuration, 1f));

        // Wait
        yield return new WaitForSeconds(waitTime);

        // Fade out
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, fadeDuration, 0f));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float duration, float targetAlpha)
    {
        float startAlpha = cg.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        cg.alpha = targetAlpha;
    }

}
