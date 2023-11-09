using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Data;
using Unity.VisualScripting;

public static class SaveDataManager 
{
    static List<string> players;
    static List<RankEntryData> rankEntryDatas;

    [Serializable]
    class PlayerProfilesData
    {
        public List<string> playerNames;
        public PlayerProfilesData(List<string> names)
        {
            playerNames = names;
        }
    }

    [Serializable]
    public class RankEntryData
    {
        public string name;
        public string date;
        public int score;

        public RankEntryData(string name, DateTime date, int score)
        {
            this.name = name;
            this.date = date.ToString();
            this.score = score;
        }
    }

    [Serializable]
    class RankListData
    {
        public List<RankEntryData> entryDatas = new List<RankEntryData>();

        public RankListData(List<RankEntryData> entryDatas)
        {
            this.entryDatas = entryDatas;
        }
    }



    private static string GetSaveDir()
    {
        return Application.persistentDataPath;
    }

    private static string GetPlayerProfileName()
    {
        return "players.pl";
    }

    private static string GetPlayerProfileSaveDir()
    {
        return GetSaveDir() + "/" + GetPlayerProfileName();
    }

    public static void SavePlayerProfile(string playerName)
    {
        players = GetSavePlayerProfiles();
        if(players.Contains(playerName))
        {
            return;
        }
        players.Insert(0, playerName);
        SavaPlayerProfileFromList(players);

    }

    private static void SavaPlayerProfileFromList(List<string> players)
    {
        PlayerProfilesData data = new PlayerProfilesData(players);
        string dataJSON = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPlayerProfileSaveDir(), dataJSON);
    }

    public static List<string> GetSavePlayerProfiles()
    {
        if (File.Exists(GetPlayerProfileSaveDir()))
        {
            string dataJSON = File.ReadAllText(GetPlayerProfileSaveDir());
            PlayerProfilesData loadedData = JsonUtility.FromJson<PlayerProfilesData>(dataJSON); // copy data form Json
            return loadedData.playerNames;
        }
        return new List<string>();
    }

    public static void DeletePlayerProfile(string playerName)
    {
        players =  GetSavePlayerProfiles();
        players.Remove(playerName);

        SavaPlayerProfileFromList(players);

    }

    public static void SaveNewRankEntry(string name, DateTime date, int score)
    {
        RankEntryData newEntry = new RankEntryData(name, date, score);
        rankEntryDatas = GetSaveEntryList();
        if(rankEntryDatas.Count == 0)
        {
            rankEntryDatas.Add(newEntry);
        }
        else
        {
            
            for(int i=0; i< rankEntryDatas.Count; i++)
            {
                if(newEntry.score > rankEntryDatas[i].score)
                {
                    rankEntryDatas.Insert(i, newEntry);
                    break;
                }
            }
        }
  

        RankListData data = new RankListData(rankEntryDatas);
        string dataJSON = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetRankSaveDir(), dataJSON);
    }

    public static List<RankEntryData> GetSaveEntryList()
    {
        List<RankEntryData> entryDatas = new List<RankEntryData>();

        if (File.Exists(GetRankSaveDir()))
        {
            string loadedDataJSON = File.ReadAllText(GetRankSaveDir());
            RankListData loadedData = JsonUtility.FromJson<RankListData>(loadedDataJSON);
            entryDatas = loadedData.entryDatas;
        }

        return entryDatas;
    }


    private static string GetRankSaveDir()
    {
        return GetSaveDir() + "/" + GetRankSaveFileName(); 
    }

    private static string GetRankSaveFileName()
    {
        return "Rank.lb";
    }

    public static void SetActivePlayer(string playerName)
    {
        players = GetSavePlayerProfiles();
        if(players.Remove(playerName))
        {
            players.Insert(0, playerName);
            SavaPlayerProfileFromList(players);
        }

    }

    public static string GetPlayerName()
    {
        players = GetSavePlayerProfiles();
        if(players.Count != 0)
        {
            return players[0];
        }
        return "unknow";
    }
}
