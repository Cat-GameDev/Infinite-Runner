using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : UICanvas
{
    [SerializeField] RankingView rankingView;
    public void BackButton()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        Close(0f);
    }

    public void RefeshButton()
    {
        rankingView.RefreshLeaderBoard();
    }

}
