using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Victory : UICanvas
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button retryButton;
    [SerializeField] Button menuButton;

    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        Close(0f);
        DeAtiveButton();
    }

    public void MenuButton()
    {
        LevelManager.Instance.OnMenu();
        Close(0f);
        DeAtiveButton();
    }

    public void ShowScore(int score)
    {
        scoreText.text = "Score \n" +  score.ToString();
    }

    public void ActiveButton()
    {
        retryButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    private void DeAtiveButton()
    {
        retryButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
    }

    public void ActiveButtonAfterTime()
    {
        Invoke(nameof(ActiveButton), 3f);
    }

}
