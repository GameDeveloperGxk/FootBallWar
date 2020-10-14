using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 游戏页面
/// </summary>
public class Game : MonoBehaviour
{
    public enum GameState
    {
        GameTask,
        GamePlay,
        GamePause,
        GameWin,
        GameLose,
    }

    public GameState currentGameState = GameState.GamePlay;

    public Button btnItem0;
    public Button btnItem1;
    public Button btnBall;
    public Button btnPause;

    public Text textUITime;
    public Text textUIScore;
    public Text textUICoin;

    /// <summary>
    /// 能量条图
    /// </summary>
    public Image imagePower;
    /// <summary>
    /// 玩家能量
    /// </summary>
    public int playerPower = -308;
    /// <summary>
    /// 玩家
    /// </summary>
    public GameObject player;
    /// <summary>
    /// 系统提示信息
    /// </summary>
    public Text textSysTips;
    public int showTextSysTipsTime;

    public GameObject ballFirePrefab;
    public GameObject ballFirePanel;

    public GameObject enemyPrefab;
    public GameObject enemyPanel;

    private int enemyTotalNum = 5;
    private int enemyCurrentNum = 0;
    private int enemyCreateTime = 30;

    private int enemyCreateTotalNum = 0;

    private int taskShowTime;
    public GameObject gameTask;
    // Use this for initialization

    public Image imgMap;
    public Image imgUIScore;
    public Sprite[] imageMap;
    public Sprite[] imageUIScoreBack;

    public GameObject enemyCar;

    public Text textItemNum0;
    public Text textItemNum1;

    private float m_Timer = 0;

    public GameObject RewardPanel;
    public GameObject rewardCoinPrefab;

    public Image ImagePowerBack;
    public Image ImageMark;

    public GameTouch touch;

    public Image imageCoinItem0;
    public Image imageCoinItem1;

    private

    void Start()
    {
        btnItem0.onClick.AddListener(Item0Click);
        btnItem1.onClick.AddListener(Item1Click);
        btnPause.onClick.AddListener(PauseGame);
    }

    /// <summary>
    /// 初始化游戏数据
    /// </summary>
    public void InitGame()
    {
        int[] idMap = new int[] { 1, 0, 2, 1, 0, 2, 1, 0, 2, 1, 0, 2, 1, 0, 2, 1, 0, 2, 1, 2 };
        imgMap.sprite = imageMap[idMap[GameController.GetInstance().currentLevel]];
        imgUIScore.sprite = imageUIScoreBack[idMap[GameController.GetInstance().currentLevel]];

        currentGameState = GameState.GameTask;
        textUITime.text = GameController.GetInstance().currentLevelTime + "";
        textUIScore.text = GameController.GetInstance().currentLevelScore + "";
        textUICoin.text = LocalData.GetInstance().GetCoin() + "";

        ResetPower();

        textSysTips.color = Color.clear;
        showTextSysTipsTime = -1;

        //enemyTotalNum = 5+GameController.GetInstance().currentLevel/5;
        enemyTotalNum = new int[] { 5, 5, 5, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15 }[GameController.GetInstance().currentLevel];
        enemyCurrentNum = 0;
        enemyCreateTime = 400;
        enemyCreateTotalNum = 0;

        textItemNum0.text = LocalData.GetInstance().GetItemSaveNum() + "";
        textItemNum1.text = LocalData.GetInstance().GetItemFireNum() + "";

        m_Timer = 0;

        taskShowTime = 0;
        gameTask.GetComponent<GameTask>().InitData();
        gameTask.SetActive(true);

        player.GetComponent<Player>().InitData();
        enemyCar.GetComponent<EnemyCar>().InitPosition();

        ShowOrHidePowerUI(false);
        ShowOrHideCoinItem();
        GameController.GetInstance().audioMgr.PlayMusic(AudioManager.MusicGame);
    }

    /// <summary>
    /// 显示或者隐藏道具金币图
    /// </summary>
    public void ShowOrHideCoinItem()
    {
        imageCoinItem0.transform.gameObject.SetActive(LocalData.GetInstance().GetItemSaveNum() <= 0 ? true : false);
        imageCoinItem1.transform.gameObject.SetActive(LocalData.GetInstance().GetItemFireNum() <= 0 ? true : false);
    }


    /// <summary>
    /// 刷新能量条
    /// </summary>
    /// <param name="_value"></param>
    public void UpdatePower()
    {
        if (imagePower.gameObject.transform.localPosition.y < 0)
        {
            ShowOrHidePowerUI(true);
            playerPower += 60;
            playerPower = Mathf.Clamp(playerPower, -308, 0);
            imagePower.gameObject.transform.localPosition = new Vector3(0, playerPower, 0);
        }
    }
    /// <summary>
    /// 重置能量条
    /// </summary>
    public void ResetPower()
    {
        playerPower = -308;
        imagePower.gameObject.transform.localPosition = new Vector3(0, playerPower, 0);
        ShowOrHidePowerUI(false);
    }

    /// <summary>
    /// 显示或者隐藏能量条
    /// </summary>
    public void ShowOrHidePowerUI(bool _show)
    {
        ImagePowerBack.color = _show ? Color.white : Color.clear;
        ImageMark.color = _show ? Color.white : Color.clear;
    }

    /// <summary>
    /// 获得力量转血值
    /// </summary>
    /// <returns></returns>
    public int GetPowerToHP()
    {
        int _hp = (int)(308 + playerPower) / 150 * 20 + 20;
        _hp = Mathf.Clamp(_hp, 20, 60);
        if (player.GetComponent<Player>().playerSaveTime > 0)//护盾存在时间内攻击力为最高
        {
            _hp = 120;
        }
        return _hp;
    }

    /// <summary>
    /// 显示系统提示文字
    /// </summary>
    /// <param name="_text"></param>
    public void ShowSystemTipsText(string _text)
    {
        textSysTips.color = Color.white;
        textSysTips.text = _text;
        showTextSysTipsTime = 100;
    }
    /// <summary>
    /// 更新系提示
    /// </summary>
    public void UpdateShowSystemTips()
    {
        if (showTextSysTipsTime > 0)
        {

        }
        else if (showTextSysTipsTime == 0)
        {
            textSysTips.color = Color.clear;
            showTextSysTipsTime = -1;
        }
        else
        {

        }
    }

    /// <summary>
    /// 时间更新
    /// </summary>
    /// <param name="_value"></param>
    public void ChangeGameTime()
    {
        m_Timer += Time.deltaTime;
        GameController.GetInstance().currentLevelTime = (int)m_Timer;
        textUITime.text = GameController.GetInstance().currentLevelTime + "";
        if (GameController.GetInstance().currentLevelTime >= 300)
        {
            LoseGame();
        }
    }
    /// <summary>
    /// 得分
    /// </summary>
    /// <param name="_value"></param>
    public void ChangeGameScore(int _value)
    {
        if (_value == 15)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundIn);
        }
        GameController.GetInstance().currentLevelScore += _value;
        textUIScore.text = GameController.GetInstance().currentLevelScore + "";
        if (GameController.GetInstance().currentLevelScore >= GameController.GetInstance().gameWinScore[GameController.GetInstance().currentLevel])
        {
            WinGame();
        }
    }
    /// <summary>
    /// 金币
    /// </summary>
    /// <param name="_value"></param>
    public void ChangeGameCoin(int _value)
    {
        GameController.GetInstance().currentLevelCoinNum += _value;
        //textUICoin.text = GameController.GetInstance().currentLevelCoinNum + "";
    }

    /// <summary>
    /// 道具 护盾 使用
    /// </summary>
    public void Item0Click()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            if (LocalData.GetInstance().GetItemSaveNum() > 0)
            {
                if (player.GetComponent<Player>().playerSaveTime <= 0)
                {
                    LocalData.GetInstance().ChangeItemSaveNum(-1);
                    player.GetComponent<Player>().SavePlayerStart();
                    GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundItem0);
                    textItemNum0.text = LocalData.GetInstance().GetItemSaveNum() + "";
                }
            }
            else
            {
                if (LocalData.GetInstance().GetCoin() >= 400)
                {
                    if (player.GetComponent<Player>().playerSaveTime <= 0)
                    {
                        player.GetComponent<Player>().SavePlayerStart();
                        GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundItem0);
                        LocalData.GetInstance().SetCoin(-400);
                        textUICoin.text = LocalData.GetInstance().GetCoin() + "";
                    }
                }
                else
                {
                    //UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "金币不足" + 1000 + "，无法使用道具");
                    OpenShop();
                }
            }
        }
    }

    /// <summary>
    /// 道具2使用
    /// </summary>
    public void Item1Click()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            if (LocalData.GetInstance().GetItemFireNum() > 0)
            {
                if (ballFirePanel.transform.childCount <= 0)
                {
                    LocalData.GetInstance().ChangeItemFireNum(-1);
                    for (int i = 0; i < 9; i++)
                    {
                        GameObject _ballFire = GameObject.Instantiate(ballFirePrefab, new Vector3(-490 + i * 120, -425, 0), Quaternion.identity) as GameObject;
                        _ballFire.transform.SetParent(ballFirePanel.transform);
                        _ballFire.GetComponent<BallFire>().InitData(new Vector3(-490 + i * 120, -425, 0));
                    }
                    GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundItem1);
                    textItemNum1.text = LocalData.GetInstance().GetItemFireNum() + "";
                }
            }
            else
            {
                if (LocalData.GetInstance().GetCoin() >= 1000)
                {
                    if (ballFirePanel.transform.childCount <= 0)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            GameObject _ballFire = GameObject.Instantiate(ballFirePrefab, new Vector3(-490 + i * 120, -425, 0), Quaternion.identity) as GameObject;
                            _ballFire.transform.SetParent(ballFirePanel.transform);
                            _ballFire.GetComponent<BallFire>().InitData(new Vector3(-490 + i * 120, -425, 0));
                        }
                        GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundItem1);
                        LocalData.GetInstance().SetCoin(-1000);
                        textUICoin.text = LocalData.GetInstance().GetCoin() + "";
                    }
                }
                else
                {
                    //UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "金币不足" + 1000 + "，无法使用道具");
                    OpenShop();
                }
            }
        }
    }

    /// <summary>
    /// 打开商店
    /// </summary>
    private void OpenShop()
    {
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Shop, true);
        UIManager.GetInstance().shopPanel.GetComponent<ShopPanel>().nextStep = UIManager.UIStep.Game;
        UIManager.GetInstance().currentUIStep = UIManager.UIStep.Shop;
        currentGameState = GameState.GamePause;
    }

    /// <summary>
    /// 踢球
    /// </summary>
    public void BallClick()
    {
        if (player.GetComponent<Player>().currentState != Player.playerState.Die
            && player.GetComponent<Player>().currentState != Player.playerState.Wait
            && player.GetComponent<Player>().currentState != Player.playerState.Play)
        {
            player.GetComponent<Player>().ChangePlayerState(Player.playerState.Play);
        }
    }

    /// <summary>
    /// 生成奖励金币
    /// </summary>
    /// <param name="_position"></param>
    /// <param name="_delta"></param>
    public void CreateRewardCoin(Vector3 _position, float _delta)
    {
        GameObject _rewardCoin = GameObject.Instantiate(rewardCoinPrefab, _position,
                    Quaternion.identity) as GameObject;
        _rewardCoin.transform.SetParent(RewardPanel.transform);
        _rewardCoin.GetComponent<CoinReward>().InitData(_delta);
    }

    /// <summary>
    /// 生成位置 0 左侧，1 右侧
    /// </summary>
    private int createPosition = 0;
    /// <summary>
    /// 敌人生成
    /// </summary>
    private void UpdateEnemyCreate()
    {
        if (enemyCurrentNum < enemyTotalNum)
        {
            enemyCreateTime++;
            if (enemyCreateTime > 400 || enemyCurrentNum <= 0)
            {
                enemyCreateTime = 0;
                GameObject _enemy = GameObject.Instantiate(enemyPrefab, new Vector3(createPosition == 0 ? -413 : 413, 136, 0), Quaternion.identity) as GameObject;
                _enemy.transform.SetParent(enemyPanel.transform);
                createPosition = createPosition == 0 ? 1 : 0;
                int _id = 0;
                int _hp = 40;
                if (GameController.GetInstance().currentLevel >= 0 && GameController.GetInstance().currentLevel < 5)
                {
                    _id = Random.Range(0, 3);
                    _hp = 40;
                }
                else if (GameController.GetInstance().currentLevel >= 5 && GameController.GetInstance().currentLevel < 10)
                {
                    _id = Random.Range(0, 5);
                    _hp = 80;
                }
                else if (GameController.GetInstance().currentLevel >= 10 && GameController.GetInstance().currentLevel < 15)
                {
                    _id = Random.Range(0, 7);
                    _hp = 120;
                }
                else
                {
                    _id = Random.Range(0, 10);
                    _hp = 120;
                }
                _enemy.GetComponent<Enemy>().InitData(_id, _hp, enemyCreateTotalNum, _enemy.transform.position.x < 0 ? 0 : 1);
                enemyCurrentNum++;
                enemyCreateTotalNum++;
            }
        }
    }

    /// <summary>
    /// 敌人减少
    /// </summary>
    public void EnemyNumReduce()
    {
        enemyCurrentNum--;
    }



    /// <summary>
    /// 暂停游戏
    /// </summary>
    private void PauseGame()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        {
            if(player.GetComponent<Player>().currentState != Player.playerState.Die)
            {
                //处理游戏逻辑 
                UIManager.GetInstance().pausePanel.GetComponent<PausePanel>().upState = currentGameState;
                currentGameState = GameState.GamePause;
                //UI
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Pause, true);
            }           
        }
    }
    /// <summary>
    /// 游戏胜利
    /// </summary>
    public void WinGame()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        {
            //处理游戏逻辑
            Clear();
            currentGameState = GameState.GameWin;

            //UI
            GameController.GetInstance().audioMgr.StopMusic(AudioManager.MusicGame);
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundGameWin);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Win, true);
        }
    }
    /// <summary>
    /// 游戏失败
    /// </summary>
    public void LoseGame()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        {
            //处理游戏逻辑
            Clear();
            InitGame();
            currentGameState = GameState.GameLose;

            //UI跳转.
            GameController.GetInstance().audioMgr.StopMusic(AudioManager.MusicGame);
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundGameLose);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Lose, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameState)
        {
            case GameState.GameTask:
                taskShowTime++;
                if (taskShowTime >= 300 || Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    gameTask.SetActive(false);
                    currentGameState = GameState.GamePlay;
                }
                break;
            case GameState.GamePlay:
                ChangeGameTime();
                UpdateEnemyCreate();
                ////点击任意位置关闭本界面
                //if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
                //{
                //    Debug.Log("点击位置"+Input.mousePosition);
                //    if(Input.mousePosition.x<=300&& Input.mousePosition.y <= 300)
                //    {
                //        touch.transform.position = new Vector3( Input.mousePosition.x- Screen.width / 2f, Input.mousePosition.y- Screen.height / 2f,0);
                //    }
                //}
                break;
            case GameState.GamePause:

                break;
            case GameState.GameWin:

                break;
            case GameState.GameLose:

                break;
        }
    }

    /// <summary>
    /// 遊戲結束清理
    /// </summary>
    public void Clear()
    {
        Tools.RemoveAllChildren(enemyPanel);
        Tools.RemoveAllChildren(ballFirePanel);
        Tools.RemoveAllChildren(RewardPanel);
        player.GetComponent<Player>().myFootball.GetComponent<FootBall>().InitData();
    }

    /// <summary>
    /// 攻击全部Enemy
    /// </summary>
    public void EnemyHurtAll(int _value)
    {
        int _childNum = enemyPanel.transform.childCount;
        for (int i = 0; i < _childNum; i++)
        {
            enemyPanel.transform.GetChild(i).GetComponent<Enemy>().Hurt(_value);
        }
    }

    //public static void RemoveAllChildren(GameObject parent)
    //{
    //    Transform transform;
    //    for (int i = 0; i < parent.transform.childCount; i++)
    //    {
    //        transform = parent.transform.GetChild(i);
    //        GameObject.Destroy(transform.gameObject);
    //    }
    //}
}
