using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{

    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void Close(float delayTime)
    {
        Time.timeScale = 1;
        base.Close(0f);
    }

    public void ContinueButton()
    {
        Close(0f);
    }

    public void MenuButton()
    {
        LevelManager.Instance.OnMenu();
        Close(0f);
    }
}
