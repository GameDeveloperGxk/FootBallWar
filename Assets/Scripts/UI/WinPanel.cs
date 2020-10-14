using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 胜利页面
/// </summary>
public class WinPanel : MonoBehaviour
{
    public Button btnMenu;
    public Button btnGoon;
    public Button btnRestart;
    public Button btnBox;

    private bool boxOpen = false ;
    /// <summary>
    /// 三星宝箱图
    /// </summary>
    public Sprite[] spriteBox;
    /// <summary>
    /// 获得金币数
    /// </summary>
    public Text textScore;
    /// <summary>
    /// 首次通关金币数
    /// </summary>
    public Text textAddCoinNum;
    /// <summary>
    /// 奖励总金币数
    /// </summary>
    private int totalCoinNum = 0;
    //关卡星级
    private int starNum = 1;

    public Image star1;
    public Image star2;
    /// <summary>
    /// 首次通关 图
    /// </summary>
    public Image imageFirst;
    /// <summary>
    /// 小诀窍提示文字
    /// </summary>
    public Text textTips;


    // Use this for initialization
    void Start()
    {
        btnMenu.onClick.AddListener(MenuClick);
        btnGoon.onClick.AddListener(GoonClick);
        btnRestart.onClick.AddListener(RestartClick);
        btnBox.onClick.AddListener(BoxClick);
    }

    /// <summary>
    /// 初始化结束界面数据
    /// </summary>
    public void InitData()
    {
        boxOpen = false;
        btnBox.GetComponent<Image>().sprite = spriteBox[0];
        //金币
        int firstWinAddCoinNum = 0;
        //首次通关
        if (GameController.GetInstance().currentLevel + 1 == LocalData.GetInstance().GetMaxOpenLevel())
        {
            firstWinAddCoinNum = new int[] { 100, 150, 200, 250, 300, 300, 300, 300, 300, 300, 350, 400, 450, 500, 550, 600, 600, 600, 600, 600 }[GameController.GetInstance().currentLevel];
            textAddCoinNum.text = "+"+ firstWinAddCoinNum;
            imageFirst.transform.gameObject.SetActive(true);
        }
        else
        {
            imageFirst.transform.gameObject.SetActive(false);
        }
        totalCoinNum = GameController.GetInstance().currentLevelCoinNum + firstWinAddCoinNum;
        LocalData.GetInstance().SetCoin(totalCoinNum);
        //textScore.text = "+"+totalCoinNum;
        //计算关卡星级//30秒内胜利三星，60秒内胜利两星，大于60秒1星
        if (GameController.GetInstance().currentLevelTime <= 30+GameController.GetInstance().currentLevel*5)
        {
            starNum = 3;
            star1.color = Color.white;
            star2.color = Color.white;
        }
        else if (GameController.GetInstance().currentLevelTime <= (30 + GameController.GetInstance().currentLevel * 5)*2)
        {
            starNum = 2;
            star1.color = Color.white;
            star2.color = Color.clear;
        }
        else
        {
            starNum = 1;
            star1.color = Color.clear;
            star2.color = Color.clear;
        }
        //刷新星级
        if(LocalData.GetInstance().GetLevelStarsIndex(GameController.GetInstance().currentLevel)< starNum)
        {
            LocalData.GetInstance().SetLevelStarsIndex(GameController.GetInstance().currentLevel, starNum);
        }      

        int _tipsID = (int)(Random.value * 3);
        textTips.text = "小诀窍:<color=#ffffff>三星评价可以打开三星宝箱</color>";
        switch (_tipsID)
        {
            case 0:
                textTips.text = "小诀窍:<color=#ffffff>规定时间以内胜利可以获得3星评价</color>";
                break;
            case 1:
                textTips.text = "小诀窍:<color=#ffffff>消灭怪兽可以获得金币</color>";
                break;
            case 2:
            default:
                textTips.text = "小诀窍:<color=#ffffff>合理使用道具可以快速获得胜利</color>";
                break;
        }

        textScore.text = LocalData.GetInstance().GetCoin() + "";
        //刷新游戏UI

        UIManager.GetInstance().game.GetComponent<Game>().textUICoin.text = LocalData.GetInstance().GetCoin() + "";
        if( GameController.GetInstance().currentLevel+1 == LocalData.GetInstance().GetMaxOpenLevel())
        {
            LocalData.GetInstance().SetMaxOpenLevel(GameController.GetInstance().currentLevel + 2);
        }        
        //保存本地数据
        LocalData.GetInstance().SaveLocalData();
    }

    private void MenuClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Win)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Win, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.StartMenu, true);
            GameController.GetInstance().audioMgr.PlayMusic(AudioManager.MusicUI);
        }
    }

    private void GoonClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Win)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);            
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Win, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, true);
            GameController.GetInstance().audioMgr.PlayMusic(AudioManager.MusicUI);            
        }
    }

    private void RestartClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Win)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Win, false);
            UIManager.GetInstance().currentUIStep = UIManager.UIStep.Game;
            //初始化游戏数据，重新开始游戏
            GameController.GetInstance().InitGameData();
            UIManager.GetInstance().game.GetComponent<Game>().InitGame();
        }
    }
    private void BoxClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Win)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            if( starNum == 3)
            {
                if(boxOpen == false)
                {
					GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundReward);
                    int boxCoin = 300;
                    LocalData.GetInstance().SetCoin(300);
                    LocalData.GetInstance().SaveLocalData();
                    UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "恭喜您获得金币" + boxCoin);
                    btnBox.GetComponent<Image>().sprite = spriteBox[1];
                    boxOpen = true;
                }
                else
                {
                    UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "宝箱已经打开");
                }                
            }
            else
            {
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "获得三星评价才能打开宝箱");
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
