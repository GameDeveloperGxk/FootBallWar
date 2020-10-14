using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    /// <summary>
    /// 是否是测试版本
    /// </summary>
    public static bool DEBUG = false;
    /// <summary>
    /// 是否是手机版本
    /// </summary>
    public static bool PHONE_VERSION = true;

    /// <summary>
    /// 音频管理
    /// </summary>
    public AudioManager audioMgr;   
    /// <summary>
    /// 全局音量音效开关 1打开 0关闭
    /// </summary>
    public int soundState { get; set; }
    /// <summary>
    /// 当前关卡
    /// </summary>
    /// <returns></returns>    
    public int currentLevel = 0;
    /// <summary>
    /// 当前殺怪數量
    /// </summary>
    public int currentLevelKillNum = 0;
    /// <summary>
    /// 当前关卡时间
    /// </summary>
    public int currentLevelTime = 0;
    /// <summary>
    /// 当前关卡得分
    /// </summary>
    public int currentLevelScore = 0;
    /// <summary>
    /// 当前关卡获得金币
    /// </summary>
    public int currentLevelCoinNum = 0;
    /// <summary>
    /// 当前关卡进球数
    /// </summary>
    public int currentLevelGetBallNum = 0;

    //================================================================游戏关卡数据部分===============================================================
    /// <summary>
    /// 游戏胜利需要的得分
    /// </summary>
    public int[] gameWinScore;




    /// <summary>
    /// 单例
    /// </summary>
    /// <returns></returns>
    public static GameController GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    /// <summary>
    /// 初始化游戏数据
    /// </summary>
    public void InitGameData()
    {
        currentLevelKillNum = 0;
        currentLevelTime = 0;
        currentLevelScore = 0;
        currentLevelCoinNum = 0;
        currentLevelGetBallNum = 0;
        gameWinScore = new int[] { 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 550, 600, 650, 700, 750, 800, 850, 900, 900, 900 };
    }

}