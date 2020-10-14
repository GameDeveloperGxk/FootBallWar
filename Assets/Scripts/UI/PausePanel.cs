using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 暂停页面
/// </summary>
public class PausePanel : MonoBehaviour
{
    public Button btnYse;
    public Button btnNo;

    public Game.GameState upState;

    // Use this for initialization
    void Start()
    {
        btnYse.onClick.AddListener(YseClick);
        btnNo.onClick.AddListener(NoClick);
    }

    private void YseClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Pause)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Pause,false);
            UIManager.GetInstance().currentUIStep = UIManager.UIStep.Game;
            //继续游戏
            UIManager.GetInstance().game.GetComponent<Game>().currentGameState = upState;
        }
    }

    private void NoClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Pause)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Pause, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.StartMenu, true);
            GameController.GetInstance().audioMgr.PlayMusic(AudioManager.MusicUI);
            //退出游戏
            UIManager.GetInstance().game.GetComponent<Game>().Clear();
        }
    }
       
    void Update()
    {

    }
}
