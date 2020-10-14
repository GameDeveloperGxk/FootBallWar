using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

//public class Test_1 : MonoBehaviour
//{
//    string m_Mins = "0";
//    string m_Sec = "0";
//    /// <summary>
//    /// 秒
//    /// </summary>
//    float m_TempMins = 0;
//    /// <summary>
//    /// 毫秒
//    /// </summary>
//    float m_TempSec = 0;
//    /// <summary>
//    /// 是否是倒计时
//    /// </summary>
//    bool m_IsTimed = true;
//    /// <summary>
//    /// 是否是计时器
//    /// </summary>
//    bool m_IsCountDown = true;

//    private void Awake()
//    {
//        //计时器
//        SetTimed("00:00");

//        //倒计时
//        SetTimed("30:00");
//        string TimeStr = transform.GetComponent<Text>().text;
//        string[] TimeStrSplit = TimeStr.Split(':');
//        m_TempMins = float.Parse(TimeStrSplit[0]);
//        m_TempSec = float.Parse(TimeStrSplit[1]);
//        m_IsCountDown = false;
//    }

//    private void FixedUpdate()
//    {
//        //if (m_IsCountDown)//计时器
//        //{
//        //    if (m_IsTimed)
//        //    {
//        //        if (transform.GetComponent<Text>().text == "09:59")
//        //        {
//        //            transform.GetComponent<Text>().text = "10:00";
//        //            m_Mins = "00";
//        //            m_Sec = "00";
//        //            m_TempMins = 0;
//        //            m_TempSec = 0;
//        //            m_IsTimed = false;
//        //        }
//        //        else
//        //        {
//        //            if (m_TempSec >= 9)
//        //            {
//        //                m_Sec = (m_TempSec += 1).ToString();
//        //                if (m_TempSec == 60)
//        //                {
//        //                    m_Sec = "00";
//        //                    m_TempSec = 0;
//        //                    m_TempMins += 1;
//        //                }
//        //            }
//        //            else
//        //            {
//        //                m_Sec = "0" + (m_TempSec += 1).ToString();
//        //            }
//        //            m_Mins = "0" + m_TempMins.ToString();
//        //            transform.GetComponent<Text>().text = m_Mins + ":" + m_Sec;
//        //        }
//        //    }
//        //}
//        //else//倒计时
//        //{
//        //    if (m_IsTimed)
//        //    {
//        //        if (m_TempSec <= 10)
//        //        {
//        //            if (m_TempSec == 0)
//        //            {
//        //                if (m_TempMins == 0)
//        //                {
//        //                    transform.GetComponent<Text>().text = "00:00";
//        //                    m_IsTimed = false;
//        //                }
//        //                else
//        //                {
//        //                    m_TempSec = 60;
//        //                    m_TempMins -= 1;
//        //                    if (m_TempMins <= 10)
//        //                    {

//        //                        m_Mins = "0" + m_TempMins.ToString();
//        //                    }
//        //                    else
//        //                    {
//        //                        m_Mins = m_TempMins.ToString();
//        //                    }
//        //                }
//        //                m_Sec = m_TempSec.ToString();
//        //            }
//        //            else
//        //            {
//        //                m_Sec = "0" + (m_TempSec -= 1).ToString();
//        //            }
//        //        }
//        //        else
//        //        {
//        //            m_Sec = (m_TempSec -= 1).ToString();
//        //        }
//        //        transform.GetComponent<Text>().text = m_Mins + ":" + m_Sec;
//        //    }
//        //}
//        SetTimed("");
//    }

//    public void SetTimed(string time)
//    {

//        transform.GetComponent<Text>().text = DateTime.Now.ToLocalTime().ToString("yyyy - MM - dd HH: mm:ss");


//        //transform.GetComponent<Text>().text = time;
//    }
//}

public class Test_1 : MonoBehaviour
{
    public Text m_ClockText;
    private float m_Timer;
    private int m_Hour;//时
    private int m_Minute;//分
    private int m_Second;//秒


    // Use this for initialization
    void Start()
    {
        m_ClockText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.deltaTime+">>>>>>>>>>>>>>>>>>>deltaTime");
        m_Timer += Time.deltaTime;
        m_Second = (int)m_Timer;
        if (m_Second > 59.0f)
        {
            m_Second = (int)(m_Timer - (m_Minute * 60));
        }
        m_Minute = (int)(m_Timer / 60);
        if (m_Minute > 59.0f)
        {
            m_Minute = (int)(m_Minute - (m_Hour * 60));
        }
        m_Hour = m_Minute / 60;
        if (m_Hour >= 24.0f)
        {
            m_Timer = 0;
        }
        m_ClockText.text = string.Format("{0:d2}:{1:d2}:{2:d2}", m_Hour, m_Minute, m_Second);
    }
}