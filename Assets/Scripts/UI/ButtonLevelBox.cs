using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 选关界面  星星宝箱
/// </summary>
public class ButtonLevelBox : MonoBehaviour
{
    /// <summary>
    /// 打开宝箱需要的星星数量
    /// </summary>
    private int[] starNeedNum = new int[] { 3, 10, 25, 40, 60 };
    /// <summary>
    /// 打开宝箱获得的金币数量
    /// </summary>
    private int[] openBoxGetCoinNum = new int[] { 300, 1000, 2500, 4000, 6000 };
    /// <summary>
    /// 宝箱图( 未打开  打开)
    /// </summary>
    public Sprite[] spriteImgBack;
    /// <summary>
    /// 宝箱ID
    /// </summary>
    private int boxID = 0;

    public Image imageBox;

    public Text textStarNum;

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// 初始化宝箱数据
    /// </summary>
    /// <param name="_id"></param>
    public void InitBaseData(int _id)
    {
        boxID = _id;
        int _state = LocalData.GetInstance().GetLevelBoxStarsStateIndex(boxID);//宝箱状态
        imageBox.sprite = spriteImgBack[_state];
        textStarNum.text = GetAllLevelStarsNum() + "/" + starNeedNum[boxID];
    }

    private int GetAllLevelStarsNum()
    {
        int _totalStarsNum = 0;
        for (int i = 0; i < LocalData.MAXLEVELNUM; i++)
        {
            _totalStarsNum += LocalData.GetInstance().GetLevelStarsIndex(i);
        }
        return _totalStarsNum;
    }

    public void Click()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.SelectLevel)
        {
            if (LocalData.GetInstance().GetLevelBoxStarsStateIndex(boxID) == 0)
            {
                if (GetAllLevelStarsNum() >= starNeedNum[boxID])
                {
					GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundReward);
                    LocalData.GetInstance().SetCoin(openBoxGetCoinNum[boxID]);
                    UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "恭喜获得金币" + openBoxGetCoinNum[boxID]);
                    LocalData.GetInstance().SetLevelBoxStarsStateIndex(boxID, 1);
                    UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().RefurshTextCoin();
                    InitBaseData(boxID);
                }
                else
                {
                    UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "星星数量不足，无法开启宝箱");
                }
            }
            else
            {
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "宝箱已打开");
            }
        }
    }

}
