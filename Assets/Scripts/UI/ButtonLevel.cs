using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 关卡选择按钮
/// </summary>
public class ButtonLevel : MonoBehaviour
{
    /// <summary>
    /// 关卡
    /// </summary>
    public int levelNum = 0;
    /// <summary>
    /// 关卡星级数量
    /// </summary>
    public int starNum = 0;
    /// <summary>
    /// 按钮背景图[未打开，打开，满星]
    /// </summary>
    public Sprite[] btnImageBack;
    /// <summary>
    /// 关卡数字文本
    /// </summary>
    public Text textLevelNum;
    /// <summary>
    /// 星级
    /// </summary>
    public Image starImg0;
    public Image starImg1;
    public Image starImg2;
    /// <summary>
    /// 按钮背景图
    /// </summary>
    public Image imageBack;

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// 初始化按钮数据状态
    /// </summary>
    /// <param name="_levelNum">关卡值</param>
    public void InitBtnBaseData(int _levelNum)
    {
        levelNum = _levelNum;
        textLevelNum.text = "" + (levelNum + 1);
        starNum = LocalData.GetInstance().GetLevelStarsIndex(levelNum);
        if (starNum == 3)
        {
            imageBack.sprite = btnImageBack[1];
        }
        else if (starNum == 2)
        {
            starImg2.color = Color.clear;
        }
        else if (starNum == 1)
        {
            starImg1.color = Color.clear;
            starImg2.color = Color.clear;
        }
        else
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() < levelNum + 1)
            {
                imageBack.sprite = btnImageBack[2];
                textLevelNum.text = "";
            }
            starImg0.color = Color.clear;
            starImg1.color = Color.clear;
            starImg2.color = Color.clear;
        }

    }

    public void Click()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.SelectLevel)
        {
            if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.SelectLevel)
            {
                GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
                if (LocalData.GetInstance().GetMaxOpenLevel() >= levelNum + 1)
                {
                    UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, false);
                    GameController.GetInstance().currentLevel = levelNum;
                    
                    UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, true);
                }
                else
                {
                    UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "当前关卡未开启");
                }
            }
        }
    }

}
