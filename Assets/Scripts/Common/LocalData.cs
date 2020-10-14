using UnityEngine;
using System.Collections;

/// <summary>
/// 数据本地永久存储类
/// </summary>
public class LocalData
{
    private static LocalData instance = null;
    /// <summary>
    /// 单例
    /// </summary>
    /// <returns></returns>
    public static LocalData GetInstance()
    {
        if (instance == null)
        {
            instance = new LocalData();
        }
        return instance;
    }

    private LocalData()
    {

    }


    //[SerializeField]
    /// <summary>
    /// 金币,初始值1000
    /// </summary>
    private int coin = 2000;
    public int GetCoin()
    {

        return coin;
    }
    public void SetCoin(int _value)
    {
        coin += _value;
        if (coin < 0)
        {
            coin = 0;
        }
    }

    /// <summary>
    /// 最大关卡数
    /// </summary>
    public const int MAXLEVELNUM = 20;
    /// <summary>
    /// 最大开启关卡数，默认1
    /// </summary>
    private int maxOpenLevel = 1;

    public int GetMaxOpenLevel()
    {
        return maxOpenLevel;
    }
    public void SetMaxOpenLevel(int _value)
    {
        maxOpenLevel = _value;
        if (maxOpenLevel < 0)
        {
            maxOpenLevel = 0;
        }
        else if (maxOpenLevel > MAXLEVELNUM)
        {
            maxOpenLevel = MAXLEVELNUM;
        }
    }


    /// <summary>
    /// 关卡星级，取值0，1，2, 3
    /// </summary>
    private int[] levelStars = new int[MAXLEVELNUM] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    /// <summary>
    /// 获取关卡星级
    /// </summary>
    /// <param name="index">关卡</param>
    /// <returns></returns>
    public int GetLevelStarsIndex(int _level)
    {
        return levelStars[_level];
    }

    /// <summary>
    /// 设置关卡星级
    /// </summary>
    /// <param name="_level">关卡</param>
    /// <param name="_starNum">星级</param>
    public void SetLevelStarsIndex(int _level, int _starNum)
    {
        levelStars[_level] = _starNum;
    }

    /// <summary>
    /// 关卡宝箱状态，取值0：未开启；1：开启；
    /// </summary>
    private int[] levelBoxStarsState = new int[] { 0, 0, 0, 0, 0 };

    /// <summary>
    /// 获得关卡宝箱状态 0：未开启；1：开启；
    /// </summary>
    /// <param name="index">宝箱索引</param>
    /// <returns></returns>
    public int GetLevelBoxStarsStateIndex(int index)
    {
        return levelBoxStarsState[index];
    }

    /// <summary>
    /// 设置关卡宝箱状态 0：未开启；1：开启；
    /// </summary>
    /// <param name="index">宝箱索引</param>
    /// <param name="value">状态0：未开启；1：开启；</param>
    public void SetLevelBoxStarsStateIndex(int index, int value)
    {
        levelBoxStarsState[index] = value;
    }

    private int itemSaveNum = 1;
    private int itemFireNum = 1;

    public int GetItemSaveNum()
    {
        return itemSaveNum;
    }

    public void ChangeItemSaveNum(int _value)
    {
        itemSaveNum += _value;
        UIManager.GetInstance().game.GetComponent<Game>().ShowOrHideCoinItem();
    }

    public int GetItemFireNum()
    {
        return itemFireNum;
    }
    public void ChangeItemFireNum(int _value)
    {
        itemFireNum += _value;
        UIManager.GetInstance().game.GetComponent<Game>().ShowOrHideCoinItem();
    }


    /// <summary>
    /// 保存数据到本地
    /// </summary>
    public void SaveLocalData()
    {
        Debug.Log("保存游戏数据");
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("max level", maxOpenLevel);
        PlayerPrefs.SetInt("level stars 0", levelStars[0]);
        PlayerPrefs.SetInt("level stars 1", levelStars[1]);
        PlayerPrefs.SetInt("level stars 2", levelStars[2]);
        PlayerPrefs.SetInt("level stars 3", levelStars[3]);
        PlayerPrefs.SetInt("level stars 4", levelStars[4]);
        PlayerPrefs.SetInt("level stars 5", levelStars[5]);
        PlayerPrefs.SetInt("level stars 6", levelStars[6]);
        PlayerPrefs.SetInt("level stars 7", levelStars[7]);
        PlayerPrefs.SetInt("level stars 8", levelStars[8]);
        PlayerPrefs.SetInt("level stars 9", levelStars[9]);
        PlayerPrefs.SetInt("level stars 10", levelStars[10]);
        PlayerPrefs.SetInt("level stars 11", levelStars[11]);
        PlayerPrefs.SetInt("level stars 12", levelStars[12]);
        PlayerPrefs.SetInt("level stars 13", levelStars[13]);
        PlayerPrefs.SetInt("level stars 14", levelStars[14]);
        PlayerPrefs.SetInt("level stars 15", levelStars[15]);
        PlayerPrefs.SetInt("level stars 16", levelStars[16]);
        PlayerPrefs.SetInt("level stars 17", levelStars[17]);
        PlayerPrefs.SetInt("level stars 18", levelStars[18]);
        PlayerPrefs.SetInt("level stars 19", levelStars[19]);
        PlayerPrefs.SetInt("level box stars state 0", levelBoxStarsState[0]);
        PlayerPrefs.SetInt("level box stars state 1", levelBoxStarsState[1]);
        PlayerPrefs.SetInt("level box stars state 2", levelBoxStarsState[2]);
        PlayerPrefs.SetInt("level box stars state 3", levelBoxStarsState[3]);
        PlayerPrefs.SetInt("level box stars state 4", levelBoxStarsState[4]);
        PlayerPrefs.SetInt("item save num", itemSaveNum);
        PlayerPrefs.SetInt("item fire num", itemFireNum);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 加载本地数据
    /// 
    /// </summary>
    public void LoadLocalData()
    {
        Debug.Log("加载游戏数据");
        coin = PlayerPrefs.GetInt("coin", coin);
        maxOpenLevel = PlayerPrefs.GetInt("max level", maxOpenLevel);
        levelStars[0] = PlayerPrefs.GetInt("level stars 0", levelStars[0]);
        levelStars[1] = PlayerPrefs.GetInt("level stars 1", levelStars[1]);
        levelStars[2] = PlayerPrefs.GetInt("level stars 2", levelStars[2]);
        levelStars[3] = PlayerPrefs.GetInt("level stars 3", levelStars[3]);
        levelStars[4] = PlayerPrefs.GetInt("level stars 4", levelStars[4]);
        levelStars[5] = PlayerPrefs.GetInt("level stars 5", levelStars[5]);
        levelStars[6] = PlayerPrefs.GetInt("level stars 6", levelStars[6]);
        levelStars[7] = PlayerPrefs.GetInt("level stars 7", levelStars[7]);
        levelStars[8] = PlayerPrefs.GetInt("level stars 8", levelStars[8]);
        levelStars[9] = PlayerPrefs.GetInt("level stars 9", levelStars[9]);
        levelStars[10] = PlayerPrefs.GetInt("level stars 10", levelStars[10]);
        levelStars[11] = PlayerPrefs.GetInt("level stars 11", levelStars[11]);
        levelStars[12] = PlayerPrefs.GetInt("level stars 12", levelStars[12]);
        levelStars[13] = PlayerPrefs.GetInt("level stars 13", levelStars[13]);
        levelStars[14] = PlayerPrefs.GetInt("level stars 14", levelStars[14]);
        levelStars[15] = PlayerPrefs.GetInt("level stars 15", levelStars[15]);
        levelStars[16] = PlayerPrefs.GetInt("level stars 16", levelStars[16]);
        levelStars[17] = PlayerPrefs.GetInt("level stars 17", levelStars[17]);
        levelStars[18] = PlayerPrefs.GetInt("level stars 18", levelStars[18]);
        levelStars[19] = PlayerPrefs.GetInt("level stars 19", levelStars[19]);
        levelBoxStarsState[0] = PlayerPrefs.GetInt("level box stars state 0", levelBoxStarsState[0]);
        levelBoxStarsState[1] = PlayerPrefs.GetInt("level box stars state 1", levelBoxStarsState[1]);
        levelBoxStarsState[2] = PlayerPrefs.GetInt("level box stars state 2", levelBoxStarsState[2]);
        levelBoxStarsState[3] = PlayerPrefs.GetInt("level box stars state 3", levelBoxStarsState[3]);
        levelBoxStarsState[4] = PlayerPrefs.GetInt("level box stars state 4", levelBoxStarsState[4]);
        itemSaveNum = PlayerPrefs.GetInt("item save num", itemSaveNum);
        itemFireNum = PlayerPrefs.GetInt("item fire num", itemFireNum);
    }
}
