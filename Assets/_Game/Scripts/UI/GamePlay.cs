using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI scoreText;
    public void SetCoin(int score)
    {
        scoreText.text = "Score \n" +  score.ToString();
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<Setting>();
    }
}
