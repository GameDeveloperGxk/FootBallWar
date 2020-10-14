using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : MonoBehaviour
{
    public StartMenu startMenu;
    public ExitPanel exitPanel;
    public SelectLevel selectLevel;
    public Game game;
    public TipsPanel tipsPanel;
    public PausePanel pausePanel;
    public WinPanel winPanel;
    public LosePanel losePanel;
    public ShopPanel shopPanel;

    private Button[] allBtn;
    private float timer = 0;
    private bool isHaveClick = false;
    public Image hand;
    void Start()
    {
        if(GameController.PHONE_VERSION)
        {
            hand.color = Color.clear;
        }
        else
        {
            hand.color = Color.white;
        }
        
        //LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.3f).setLoopPingPong();
        //mtra.position = startMenu.btn_start.transform.position;
        //allBtn = new Button[] {startMenu.btn_start, startMenu.btn_sound, startMenu.btn_close,  //0  1  2
        //closeMenu.btn_ok,closeMenu.btn_no,                                                     //3  4
        //selectPlayerMenu.btn_Home,selectPlayerMenu.btn_Play,selectPlayerMenu.btn_1,selectPlayerMenu.btn_2,selectPlayerMenu.btn_3,   // 5  6  7  8  9
        //selectPlayerMenu.btn_4,selectPlayerMenu.btn_5,selectPlayerMenu.btn_6,selectPlayerMenu.btn_7,selectPlayerMenu.btn_8,         //10  11 12 13 14
        //selectPlayerMenu.btn_9,selectPlayerMenu.btn_10,selectPlayerMenu.btn_11,selectPlayerMenu.btn_12,selectPlayerMenu.btn_13,     //15  16 17 18 19
        //selectPlayerMenu.btn_14,selectPlayerMenu.btn_15,selectPlayerMenu.btn_16,selectPlayerMenu.btn_17,selectPlayerMenu.btn_18,selectPlayerMenu.btn_19,selectPlayerMenu.btn_20,  // 20---26
        //selectLvMenu.btn_Home,selectLvMenu.btn_Left,selectLvMenu.btn_Right,selectLvMenu.levelBtnAll[0],selectLvMenu.levelBtnAll[1],selectLvMenu.levelBtnAll[2],                   // 27---32
        //selectLvMenu.levelBtnAll[3],selectLvMenu.levelBtnAll[4],selectLvMenu.levelBtnAll[5],selectLvMenu.levelBtnAll[6],selectLvMenu.levelBtnAll[7],selectLvMenu.levelBtnAll[8],  // 33---38
        //selectLvMenu.levelBtnAll[9],selectLvMenu.levelBtnAll[10],selectLvMenu.levelBtnAll[11],selectLvMenu.levelBtnAll[12],selectLvMenu.levelBtnAll[13],selectLvMenu.levelBtnAll[14],   // 39 ---44
        //selectLvMenu.levelBtnAll[15],selectLvMenu.levelBtnAll[16],selectLvMenu.levelBtnAll[17],selectLvMenu.levelBtnAll[18],selectLvMenu.levelBtnAll[19],selectLvMenu.levelBtnAll[20],  // 45---50
        //selectLvMenu.levelBtnAll[21],selectLvMenu.levelBtnAll[22],selectLvMenu.levelBtnAll[23],selectLvMenu.levelBtnAll[24],selectLvMenu.levelBtnAll[25],selectLvMenu.levelBtnAll[26],  //51---56
        //selectLvMenu.levelBtnAll[27],selectLvMenu.levelBtnAll[28],selectLvMenu.levelBtnAll[29],           //57---59
        //gameMenu.btn_Home,                                            //60 
        //stopMenu.btn_ok, stopMenu.btn_no,                            // 61  62
        //gameOver.btnHome, gameOver.btnRes, gameOver.btnPlay,       // 63  64   65
        //selectPlayerMenu.buy,selectPlayerMenu.noBuy};
    }

    void Update()
    {
        ClickDirection();
        ClickSure();
        ClickReturn();
        if (isHaveClick == true)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                timer = 0;
                isHaveClick = false;
            }
        }
    }

    public void ClickOK(Button btn)
    {
        KeyCode MIOkKeyCode = GameController.DEBUG ? KeyCode.Return : (KeyCode)10;//小米遥控器确认键
        if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.JoystickButton0) || Input.GetKeyUp(MIOkKeyCode))
        {
            btn.onClick.Invoke();          
        }
    }

   

    public void ClickSure()
    {
        //if (isHaveClick == false)
        //{
        //    KeyCode MIOkKeyCode = GameController.DEBUG ? KeyCode.Return : (KeyCode)10;//小米遥控器确认键
        //    if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.JoystickButton0) || Input.GetKeyUp(MIOkKeyCode))
        //    {
        //        isHaveClick = true;
        //        for (int i = 0; i < allBtn.Length; i++)
        //        {
        //            if (mtra.position == allBtn[i].transform.position)
        //            {
        //                if (UIManager._instance.uiStep == UIManager.UIStep.start)
        //                {
        //                    if (i == 0)
        //                    {//开始界面跳转选人界面
        //                        mtra.position = selectPlayerMenu.btn_1.transform.position;
        //                    }
        //                    else if (i == 2)
        //                    {//开始界面跳转关闭界面
        //                        mtra.position = closeMenu.btn_ok.transform.position;
        //                    }
        //                    allBtn[i].onClick.Invoke();
        //                    return;
        //                }
        //                else if (UIManager._instance.uiStep == UIManager.UIStep.close)
        //                {
        //                    if (i == 4)
        //                    {//关闭界面跳转开始界面
        //                        mtra.position = startMenu.btn_start.transform.position;
        //                    }
        //                    allBtn[i].onClick.Invoke();
        //                    return;
        //                }
        //                else if (UIManager._instance.uiStep == UIManager.UIStep.selectPlayer)
        //                {
        //                    if (i == 5)
        //                    {//选人界面跳转开始界面
        //                        mtra.position = startMenu.btn_start.transform.position;
        //                    }
        //                    else if (i == 6)
        //                    {//选人界面跳转选关界面
        //                     //  mtra.position = selectLvMenu.levelBtnAll[selectLvMenu.GetLevelData].transform.position;
        //                    }
        //                    allBtn[i].onClick.Invoke();
        //                    return;
        //                }
        //                else if (UIManager._instance.uiStep == UIManager.UIStep.selectLevel)
        //                {
        //                    if (i == 27)
        //                    {//选关界面跳转开始界面
        //                        mtra.position = startMenu.btn_start.transform.position;
        //                    }
        //                    else if (i >= 30 && i <= 59)
        //                    {//选关界面跳转游戏界面 
        //                     // if (i -30 <= selectLvMenu.GetLevelData)
        //                     //    mtra.position = gameMenu.btn_Home.transform.position;
        //                    }
        //                    else if (i == 28)
        //                    {//28 left
        //                        if (selectLvMenu.GetMenuID == 1)
        //                        {
        //                            mtra.position = selectLvMenu.levelBtnAll[10].transform.position;
        //                        }
        //                    }
        //                    else if (i == 29)
        //                    {// 29  right
        //                        if (selectLvMenu.GetMenuID == 1)
        //                        {
        //                            mtra.position = selectLvMenu.levelBtnAll[10].transform.position;
        //                        }
        //                    }
        //                    allBtn[i].onClick.Invoke();
        //                    return;
        //                }
        //                else if (UIManager._instance.uiStep == UIManager.UIStep.game)
        //                {
        //                    if (i == 60)
        //                    {//游戏界面跳转到暂停界面
        //                     // mtra.position = stopMenu.btn_ok.transform.position;
        //                    }
        //                    // allBtn[i].onClick.Invoke();
        //                    return;
        //                }
        //                else if (UIManager._instance.uiStep == UIManager.UIStep.gameStop)
        //                {
        //                    if (i == 61)
        //                    {//暂停界面跳转到游戏界面
        //                        mtra.position = gameMenu.btn_Home.transform.position;
        //                    }
        //                    else
        //                    if (i == 62)
        //                    {//暂停界面跳转到开始界面
        //                        mtra.position = startMenu.btn_start.transform.position;
        //                    }
        //                    allBtn[i].onClick.Invoke();
        //                    return;
        //                }
        //                else if (UIManager._instance.uiStep == UIManager.UIStep.gameOver)
        //                {
        //                    if (i == 63)
        //                    {//结束界面跳转到开始界面
        //                        mtra.position = startMenu.btn_start.transform.position;
        //                    }
        //                    else if (i == 64)
        //                    {//结束界面跳转到游戏界面
        //                        mtra.position = gameMenu.btn_Home.transform.position;
        //                    }
        //                    else if (i == 65)
        //                    {//结束界面跳转到选关界面
        //                     //   mtra.position = selectLvMenu.levelBtnAll[selectLvMenu.GetLevelData].transform.position;
        //                    }
        //                    allBtn[i].onClick.Invoke();
        //                    return;
        //                }
        //                else if (UIManager._instance.uiStep == UIManager.UIStep.selectPlayerBuy)
        //                {
        //                    //if (i == 66)
        //                    //{//选人界面购买界面购买
        //                    //    mtra.position = startMenu.btn_start.transform.position;
        //                    //}
        //                    //else if (i == 67)
        //                    //{//选人购买界面取消
        //                    //    mtra.position = gameMenu.btn_Home.transform.position;
        //                    //} 
        //                    allBtn[i].onClick.Invoke();
        //                    return;
        //                }


        //            }
        //        }
        //    }
        //}
    }
    public void ClickReturn()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (UIManager.GetInstance().currentUIStep)
            {                
                case UIManager.UIStep.StartMenu:
                    ClickBack(startMenu.buttonExit);
                    break;
                case UIManager.UIStep.ExitPanel:
                    ClickBack(exitPanel.ButtonNo);
                    break;
                case UIManager.UIStep.SelectLevel:
                    ClickBack(selectLevel.ButtonBack);
                    break;
                case UIManager.UIStep.Game:
                    ClickBack(game.btnPause);
                    transform.position = pausePanel.btnYse.transform.position;
                    break;
                case UIManager.UIStep.Pause:
                    ClickBack(pausePanel.btnYse);
                    break;
                case UIManager.UIStep.Win:
                    ClickBack(winPanel.btnGoon);
                    break;
                case UIManager.UIStep.Lose:
                    ClickBack(losePanel.btnMenu);
                    break;
                case UIManager.UIStep.Shop:
                    ClickBack(shopPanel.buttonBack);
                    break;
                default:
                    break;
            }
        }

    }

    public void ClickBack(Button btn)
    {
        btn.onClick.Invoke();
    }

    void ClickDirection()
    {
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    switch (UIManager._instance.uiStep)
        //    {
        //        case UIManager.UIStep.start:
        //            ClickMenuLeft();
        //            break;
        //        case UIManager.UIStep.close:
        //            ClickCloseLeft();
        //            break;
        //        case UIManager.UIStep.selectPlayer:
        //            ClickSelectPlayerLeft();
        //            selectPlayerMenu.ChangeScoll(mtra);
        //            break;
        //        case UIManager.UIStep.selectPlayerBuy:
        //            ClickSelectPlayerBuyLeft();
        //            break;
        //        case UIManager.UIStep.selectLevel:
        //            ClickSelectLvLeft();
        //            break;
        //        case UIManager.UIStep.game:
        //            break;
        //        case UIManager.UIStep.gameStop:
        //            ClickGameStopLeft();
        //            break;
        //        case UIManager.UIStep.gameOver:
        //            ClickGameOverLeft();
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    switch (UIManager._instance.uiStep)
        //    {
        //        case UIManager.UIStep.start:
        //            ClickMenuRight();
        //            break;
        //        case UIManager.UIStep.close:
        //            ClickCloseLeft();
        //            break;
        //        case UIManager.UIStep.selectPlayer:
        //            ClickSelectPlayerRight();
        //            selectPlayerMenu.ChangeScoll(mtra);
        //            break;
        //        case UIManager.UIStep.selectPlayerBuy:
        //            ClickSelectPlayerBuyLeft();
        //            break;
        //        case UIManager.UIStep.selectLevel:
        //            ClickSelectLvRight();
        //            break;
        //        case UIManager.UIStep.game:
        //            break;
        //        case UIManager.UIStep.gameStop:
        //            ClickGameStopLeft();
        //            break;
        //        case UIManager.UIStep.gameOver:
        //            ClickGameOverRight();
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    switch (UIManager._instance.uiStep)
        //    {
        //        case UIManager.UIStep.start:
        //            ClickMenuLeft();
        //            break;
        //        case UIManager.UIStep.close:
        //            ClickCloseLeft();
        //            break;
        //        case UIManager.UIStep.selectPlayer:
        //            ClickSelectPlayerUp();
        //            selectPlayerMenu.ChangeScoll(mtra);
        //            break;
        //        case UIManager.UIStep.selectPlayerBuy:
        //            ClickSelectPlayerBuyLeft();
        //            break;
        //        case UIManager.UIStep.selectLevel:
        //            ClickSelectLvUp();
        //            break;
        //        case UIManager.UIStep.game:
        //            break;
        //        case UIManager.UIStep.gameStop:
        //            ClickGameStopLeft();
        //            break;
        //        case UIManager.UIStep.gameOver:
        //            ClickGameOverLeft();
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    switch (UIManager._instance.uiStep)
        //    {
        //        case UIManager.UIStep.start:
        //            ClickMenuRight();
        //            break;
        //        case UIManager.UIStep.close:
        //            ClickCloseLeft();
        //            break;
        //        case UIManager.UIStep.selectPlayer:
        //            ClickSelectPlayerDown();
        //            selectPlayerMenu.ChangeScoll(mtra);
        //            break;
        //        case UIManager.UIStep.selectPlayerBuy:
        //            ClickSelectPlayerBuyLeft();
        //            break;
        //        case UIManager.UIStep.selectLevel:
        //            ClickSelectLvDown();
        //            break;
        //        case UIManager.UIStep.game:
        //            break;
        //        case UIManager.UIStep.gameStop:
        //            ClickGameStopLeft();
        //            break;
        //        case UIManager.UIStep.gameOver:
        //            ClickGameOverRight();
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
        
}
