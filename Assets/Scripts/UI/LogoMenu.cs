using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * 品牌LOGO页面、遥控器页面
 * @date 20200328 21:59
 * 
 * **/
public class LogoMenu : MonoBehaviour
{

    public Image LogoImg;
    public Image RemoteControlImg;

    public bool IsShowRemoteControl = false;

    // Use this for initialization
    void Start()
    {
        //if (GameController.IsPhoneVersion)//Phone
        //{
            Invoke("LoadGameScene", 4f);
        //}
        //else//TV
        //{
        //    Invoke("ShowRemoteControlImg", 4f);
        //}
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowRemoteControlImg()
    {
        IsShowRemoteControl = true;
        LogoImg.GetComponent<CanvasGroup>().alpha = 0;
        Invoke("LoadGameScene", 2f);
    }


    // Update is called once per frame
    void Update()
    {
        //if (GameController.IsPhoneVersion == false && IsShowRemoteControl)//TV
        //{
        //    Debug.Log("down");
        //    KeyCode MIOkKeyCode = GameController.DEBUG ? KeyCode.Return : (KeyCode)10;//小米遥控器确认键
        //    if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.JoystickButton0) || Input.GetKeyUp(MIOkKeyCode))
        //    {
        //        LoadGameScene();
        //    }
        //}

    }
}
