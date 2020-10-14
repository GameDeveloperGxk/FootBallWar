using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class GamePlayerButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    // 延迟时间
    private float delay = 0.2f;

    // 按钮是否是按下状态
    private bool isDown = false;

    // 按钮最后一次是被按住状态时候的时间
    private float lastIsDownTime;


    void Update()
    {
        // 如果按钮是被按下状态
        if (isDown)
        {
            // 当前时间 -  按钮最后一次被按下的时间 > 延迟时间0.2秒
            if (Time.time - lastIsDownTime > delay)
            {
                // 触发长按方法
                // 记录按钮最后一次被按下的时间
                lastIsDownTime = Time.time;
                if (UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().currentState == Player.playerState.Move
                    || UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().currentState == Player.playerState.Stand)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().UpdatePower();
                }
            }
        }

    }

    // 当按钮被按下后系统自动调用此方法
    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        lastIsDownTime = Time.time;
    }

    // 当按钮抬起的时候自动调用此方法
    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        if (UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().currentState == Player.playerState.Move
                    || UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().currentState == Player.playerState.Stand)
        {
            UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().ChangePlayerState(Player.playerState.Play);
        }
    }

    // 当鼠标从按钮上离开的时候自动调用此方法
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDown)
        {
            if (UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().currentState == Player.playerState.Move
                    || UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().currentState == Player.playerState.Stand)
            {
                UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().ChangePlayerState(Player.playerState.Play);
            }
        }  
        isDown = false;
    }
}

