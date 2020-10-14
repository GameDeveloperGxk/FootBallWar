using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 失败页面
/// </summary>
public class LosePanel : MonoBehaviour {

    public Button btnMenu;
    public Button btnGoon;
    public Button btnRestart;

    public Text textCoin;
	// Use this for initialization
	void Start () {
        btnMenu.onClick.AddListener(MenuClick);
        btnGoon.onClick.AddListener(GoonClick);
        btnRestart.onClick.AddListener(RestartClick);
    }

    public void InitData()
    {
        textCoin.text = LocalData.GetInstance().GetCoin() + "";
        //刷新游戏UI
        UIManager.GetInstance().game.GetComponent<Game>().textUICoin.text = LocalData.GetInstance().GetCoin() + "";
    }

    private void MenuClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Lose)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Lose,false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.StartMenu, true);
            GameController.GetInstance().audioMgr.PlayMusic(AudioManager.MusicUI);
        }
    }

    private void GoonClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Lose)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Lose, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, true);
            GameController.GetInstance().audioMgr.PlayMusic(AudioManager.MusicUI);
        }
    }

    private void RestartClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Lose)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Lose, false);
            UIManager.GetInstance().currentUIStep = UIManager.UIStep.Game;
            //初始化游戏数据，重新开始游戏
            GameController.GetInstance().InitGameData();
            UIManager.GetInstance().game.GetComponent<Game>().InitGame();
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
