using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 关卡选择页面
/// </summary>
public class SelectLevel : MonoBehaviour {

    public Button ButtonBack;
    public Button ButtonPageDown;
    public Button ButtonPageUp;

    public GameObject panelLevelBack;
    /// <summary>
    /// 关卡按钮预制件
    /// </summary>
    public GameObject btnLevelPrefab;
           
    public GameObject panelBoxBack;

    /// <summary>
    /// 宝箱按钮预制件
    /// </summary>
    public GameObject btnBoxPrefab;

    /// <summary>
    /// 当前页面
    /// </summary>
    private int currentPage = 0;

    /// <summary>
    /// 游戏金币数量
    /// </summary>
    public Text textCoin;

	// Use this for initialization
	void Start () {
        ButtonBack.onClick.AddListener(BackClick);
        ButtonPageDown.onClick.AddListener(PageChangeClick);
        ButtonPageUp.onClick.AddListener(PageChangeClick);       
    }

    /// <summary>
    /// 初始化关卡数据
    /// </summary>
    public void InitData()
    {
        if (LocalData.GetInstance().GetMaxOpenLevel() > 10)
        {
            currentPage = 1;
        }
        else
        {
            currentPage = 0;
        }

        InitButton();
        InitButton_Box();
        RefurshTextCoin();
    }

    public void RefurshTextCoin()
    {
        textCoin.text = "" + LocalData.GetInstance().GetCoin();
    }

    /// <summary>
    /// 初始化当前关卡按钮
    /// </summary>
    public void InitButton()
    {
        Tools.RemoveAllChildren(panelLevelBack);
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject _btnLevel = GameObject.Instantiate(btnLevelPrefab, new Vector3(-383 + j * 190, 106 - i * 186, 0), Quaternion.identity) as GameObject;
                _btnLevel.transform.SetParent(panelLevelBack.transform);
                _btnLevel.GetComponent<ButtonLevel>().InitBtnBaseData(i*5+j + currentPage * 10);
            }
        }
    }

    /// <summary>
    /// 初始化宝箱
    /// </summary>
    public void InitButton_Box()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject _btnBox = GameObject.Instantiate(btnBoxPrefab, new Vector3(-244 + i * 123, -260, 0), Quaternion.identity) as GameObject;
            _btnBox.transform.SetParent(panelBoxBack.transform);
            _btnBox.GetComponent<ButtonLevelBox>().InitBaseData(i);
        }
    }

    public void BackClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.SelectLevel)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.StartMenu, true);
        }
    }

    public void PageChangeClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.SelectLevel)
        {
            currentPage = currentPage == 0 ? 1 : 0;
            InitButton();
        }
    }

   
    // Update is called once per frame
    void Update () {
	
	}
}
