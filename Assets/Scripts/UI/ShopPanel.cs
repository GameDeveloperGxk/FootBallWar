using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public Button buttonBack;
    public Button buttonGet0;
    public Button buttonGet1;
    public Button buttonGet2;

    public Text textCoin;
    public Text textItemNum0;
    public Text textItemNum1;

    public UIManager.UIStep nextStep;

    // Use this for initialization
    void Start()
    {
        buttonBack.onClick.AddListener(ClickBack);
        buttonGet0.onClick.AddListener(ClickGet0);
        buttonGet1.onClick.AddListener(ClickGet1);
        buttonGet2.onClick.AddListener(ClickGet2);        
    }

    public void InitData()
    {
        textCoin.text = LocalData.GetInstance().GetCoin() + "";
        textItemNum0.text = LocalData.GetInstance().GetItemSaveNum() + "";
        textItemNum1.text = LocalData.GetInstance().GetItemFireNum() + "";
    }

    void ClickGet0()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Shop)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            if (LocalData.GetInstance().GetCoin() >= 400)
            {
                LocalData.GetInstance().SetCoin(-400);
                LocalData.GetInstance().ChangeItemSaveNum(1);
                //UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "恭喜您使用1000金币购买无敌护盾X1");
                //更新显示文本
                textItemNum0.text = LocalData.GetInstance().GetItemSaveNum()+"";
                textCoin.text = LocalData.GetInstance().GetCoin() + "";
                GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundReward);
                //刷新游戏UI
                if (nextStep == UIManager.UIStep.Game)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().textItemNum0.text = LocalData.GetInstance().GetItemSaveNum() + "";
                    UIManager.GetInstance().game.GetComponent<Game>().textUICoin.text = LocalData.GetInstance().GetCoin() + "";
                }
            }
            else
            {
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "金币不足" + 400 + "，无法购买");
            }
        }
    }
    void ClickGet1()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Shop)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            if (LocalData.GetInstance().GetCoin() >= 1000)
            {
                LocalData.GetInstance().SetCoin(-1000);
                LocalData.GetInstance().ChangeItemFireNum(1);
                //UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "恭喜您使用1000金币购买火力援助X1");
                //更新显示文本
                textItemNum1.text = LocalData.GetInstance().GetItemFireNum() + "";
                textCoin.text = LocalData.GetInstance().GetCoin() + "";
                GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundReward);

                //刷新游戏UI
                if (nextStep == UIManager.UIStep.Game)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().textItemNum1.text = LocalData.GetInstance().GetItemFireNum() + "";
                    UIManager.GetInstance().game.GetComponent<Game>().textUICoin.text = LocalData.GetInstance().GetCoin() + "";
                }
            }
            else
            {
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "金币不足" + 1000 + "，无法购买");
            }
        }
    }

    void ClickGet2()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Shop)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            //测试，免费获得金币
            LocalData.GetInstance().SetCoin(1000);
            //UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "恭喜您获得1000金币");
            //更新显示文本
            textCoin.text = LocalData.GetInstance().GetCoin() + "";
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundReward);

            //刷新游戏UI
            if (nextStep == UIManager.UIStep.Game)
            {
                UIManager.GetInstance().game.GetComponent<Game>().textUICoin.text = LocalData.GetInstance().GetCoin() + "";
            }
        }
    }

    void ClickBack()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Shop)
        {
            if(nextStep == UIManager.UIStep.StartMenu)
            {
                GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Shop, false);
                UIManager.GetInstance().currentUIStep = UIManager.UIStep.StartMenu;
            }
            else
            {
                GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Shop, false);
                UIManager.GetInstance().currentUIStep = UIManager.UIStep.Game;
                UIManager.GetInstance().game.GetComponent<Game>().currentGameState = Game.GameState.GamePlay;
            }            
        }
    }


}
