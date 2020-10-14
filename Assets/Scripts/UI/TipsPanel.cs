using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 系统提示弹框
/// </summary>
public class TipsPanel : MonoBehaviour
{

    /// <summary>
    /// 提示信息
    /// </summary>
    public Text textInfo;

    /// <summary>
    /// 关闭时间
    /// </summary>
    public int timeClose = 0;
    /// <summary>
    /// 提示页面下边的页面,提示页面显示关闭后需要回到的页面
    /// </summary>
    public UIManager.UIStep currentUpStep;

    // Use this for initialization
    void Start()
    {

    }

    public void InitBaseData(string _textInfo, UIManager.UIStep _upStep)
    {
        textInfo.text = _textInfo;
        timeClose = 100;
        currentUpStep = _upStep;
    }

    private void ClosePanel()
    {
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, false, "");
        UIManager.GetInstance().currentUIStep = currentUpStep;
    }

    void Update()
    {
        timeClose--;
        if (timeClose <= 0)
        {
            ClosePanel();
        }
        //点击任意位置关闭本界面
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            ClosePanel();
        }
    }


}
