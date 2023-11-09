using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : UICanvas
{
    [SerializeField] Transform CreatePlayerProfileMenu;
    [SerializeField] Transform Panal;
    [SerializeField] Transform playerProfilePanel;
    [SerializeField] TMP_InputField namePlayer_InputField;
    [SerializeField] TMP_Dropdown playerProfile_Dropdown;
    [SerializeField] Transform musicPanal;
    [SerializeField] Button mute;
    [SerializeField] Button unmute;


    private void Start() 
    {
        UpPlayerProfile();
        playerProfile_Dropdown.onValueChanged.AddListener(UpdateActivePlayer);
    }

    private void UpdateActivePlayer(int index)
    {
        string currentName = playerProfile_Dropdown.options[index].text;
        SaveDataManager.SetActivePlayer(currentName);
    }

    private void UpPlayerProfile()
    {
        List<string> playerProfiles = SaveDataManager.GetSavePlayerProfiles();
        playerProfile_Dropdown.ClearOptions();
        playerProfile_Dropdown.AddOptions(playerProfiles);
    }

    public void PlayButton()
    {
        LevelManager.Instance.OnStartGame();

        UIManager.Instance.OpenUI<GamePlay>();
        Close(0f);
    }

    public void RankButton()
    {
        UIManager.Instance.OpenUI<Rank>();
        Close(0f);
    }

    public void SettingButton()
    {
        musicPanal.gameObject.SetActive(true);
        unmute.gameObject.SetActive(false);
    }

    public void ExitSettingButton()
    {
        musicPanal.gameObject.SetActive(false);
    }

    public void MuteButton()
    {
        AudioManager.Instance.ToggleMusic();
        AudioManager.Instance.ToggleSFX();
        mute.gameObject.SetActive(false);
        unmute.gameObject.SetActive(true);
    }

    public void UnmuteButton()
    {
        AudioManager.Instance.ToggleMusic();
        AudioManager.Instance.ToggleSFX();
        mute.gameObject.SetActive(true);
        unmute.gameObject.SetActive(false);
    }






    public void ExitButton()
    {
        Application.Quit();
    }

    public void AddButton()
    {
        SetActiveProfileMenuPanel();
    }

    public void DeleteButton()
    {
        if(playerProfile_Dropdown.options.Count == 0)
            return;
       
        string playerName = playerProfile_Dropdown.options[playerProfile_Dropdown.value].text;
        SaveDataManager.DeletePlayerProfile(playerName);
        UpPlayerProfile();
        
    }

    public void CreateButton()
    {
        string newPlayerName =  namePlayer_InputField.text;
        SaveDataManager.SavePlayerProfile(newPlayerName);
        DeactiveProfileMenuPanel();
        UpPlayerProfile();
        namePlayer_InputField.text = null;
    }

    public void CanelButton()
    {
        DeactiveProfileMenuPanel(); 
        namePlayer_InputField.text = null;
    }

    private void SetActiveProfileMenuPanel()
    {
        CreatePlayerProfileMenu.gameObject.SetActive(true);
        Panal.gameObject.SetActive(false);
        playerProfilePanel.gameObject.SetActive(false);
    }

    private void DeactiveProfileMenuPanel()
    {
        CreatePlayerProfileMenu.gameObject.SetActive(false);
        Panal.gameObject.SetActive(true);  
        playerProfilePanel.gameObject.SetActive(true); 
    }

    
}
