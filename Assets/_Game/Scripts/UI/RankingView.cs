using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RankingView : MonoBehaviour
{
    [SerializeField] LeaderBoardEntry leaderBoardEntryPrefab;
    [SerializeField] RectTransform leaderBoardList;
    List<SaveDataManager.RankEntryData> entries;
    private void Start() 
    {
        RefreshLeaderBoard();
    }

    public void RefreshLeaderBoard()
    {
        ClearLeaderBoardList();

        entries =  SaveDataManager.GetSaveEntryList();
        LeaderBoardEntry newEntry ;
        for(int i=0; i< entries.Count; i++)
        {

            newEntry = SimplePool.Spawn<LeaderBoardEntry>(PoolType.LeaderBoard, leaderBoardList);
            newEntry.Init(entries[i].name, entries[i].date,entries[i].score);
            leaderBoardList.sizeDelta += new Vector2(0, newEntry.GetComponent<RectTransform>().sizeDelta.y);
        }
    }

    public void ClearLeaderBoardList()
    {
        List<LeaderBoardEntry> entriesToRemove = new List<LeaderBoardEntry>();


        for (int i = 0; i < leaderBoardList.childCount; i++)
        {
            LeaderBoardEntry entry = leaderBoardList.GetChild(i).GetComponent<LeaderBoardEntry>();

            if (entry != null)
            {
                entriesToRemove.Add(entry);
            }
        }

 
        for (int i = 0; i < entriesToRemove.Count; i++)
        {
            entriesToRemove[i].OnDespawn();
        }
    }
}
