using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardEntry : GameUnit
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] TextMeshProUGUI scoreText;
    internal void Init(string name, string date, int score)
    {
        nameText.SetText(name);
        dateText.SetText(date);
        scoreText.SetText(score.ToString());
    }

    
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }


}
