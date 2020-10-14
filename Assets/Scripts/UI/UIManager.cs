using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI管理类,用来控制UI面板的显示和隐藏
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
        
    /// <summary>
    /// UI页面
    /// </summary>
    public enum UIStep
    {
        StartMenu,
        ExitPanel,
        SelectLevel,
        TipsPanel,
        Game,
        Pause,
        Win,
        Lose,
        Shop,
    }

    /// <summary>
    /// 当前UI页面
    /// </summary>
    public UIStep currentUIStep = UIStep.StartMenu;

    public GameObject startMenu;
    public GameObject exitPanel;
    public GameObject selectLevel;
    public GameObject game;
    public GameObject tipsPanel;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject shopPanel;

    public static UIManager GetInstance()
    {
        return instance;
    }
    
    private void Awake()
    {
        instance = this;
    }
        
    void Start()
    {
        currentUIStep = UIStep.StartMenu;
    }

    /// <summary>
    /// 显示/隐藏页面
    /// </summary>
    /// <param name="_isShow">显示true；隐藏false</param>
    /// <param name="_ext1">扩展参数1</param>
    public void ShowOrHideUI(UIStep _step, bool _isShow,string _ext1=null)
    {
        switch( _step)
        {
            case UIStep.StartMenu:
                startMenu.SetActive(_isShow);
                LocalData.GetInstance().SaveLocalData();
                break;
            case UIStep.ExitPanel:
                //startMenu.transform.Find("ExitPanel").gameObject.SetActive(_isShow);
                exitPanel.SetActive(_isShow);
                break;
            case UIStep.SelectLevel:
                selectLevel.SetActive(_isShow);
                if( _isShow)
                {
                    selectLevel.GetComponent<SelectLevel>().InitData();
                }                
                break;
            case UIStep.TipsPanel:
                tipsPanel.SetActive(_isShow);
                if(_isShow)
                {
                    tipsPanel.GetComponent<TipsPanel>().InitBaseData(_ext1, currentUIStep);
                }                
                break;
            case UIStep.Game:
                game.SetActive(_isShow);
                if (_isShow)
                {
                    GameController.GetInstance().InitGameData();
                    game.GetComponent<Game>().InitGame();
                }                   
                break;
            case UIStep.Pause:
                pausePanel.SetActive(_isShow);
                break;
            case UIStep.Win:
                winPanel.SetActive(_isShow);
                if (_isShow)
                {
                    winPanel.GetComponent<WinPanel>().InitData();
                }
                break;
            case UIStep.Lose:
                losePanel.SetActive(_isShow);
                if (_isShow)
                {
                    losePanel.GetComponent<LosePanel>().InitData();
                }
                break;
            case UIStep.Shop:
                shopPanel.SetActive(_isShow);
                if (_isShow)
                {
                    shopPanel.GetComponent<ShopPanel>().InitData();
                }                    
                break;
        }
        if( _isShow)
        {
            currentUIStep = _step;
        }        
    }
    

}
