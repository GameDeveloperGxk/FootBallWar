using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 玩家
/// </summary>
public class Player : MonoBehaviour
{
    public Animator playerAnimator;
    /// <summary>
    /// 玩家状态
    /// </summary>
    public enum playerState
    {
        Stand,//站立
        Move,//移动
        Play,//踢球
        Wait,//等待
        Die,//死亡
    }
    /// <summary>
    /// 玩家当前状态
    /// </summary>
    public playerState currentState;
    /// <summary>
    /// 玩家死亡时间1秒
    /// </summary>
    private int dieTime = 30;
    /// <summary>
    /// 保护罩图
    /// </summary>
    public Image imageSave;
    /// <summary>
    /// 保护时间
    /// </summary>
    public int playerSaveTime;

    public Image imageBall;

    public GameObject myFootball;

    private int moveSpeed = 400;

    public GameTouch touch;
    
    void Start()
    {
        
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData()
    {
        playerAnimator.speed = 2f;
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        playerSaveTime = -1;
        //保护罩
        imageSave.color = Color.clear;
        //状态
        currentState = playerState.Stand;
        if (playerAnimator.gameObject.activeSelf)
        {
            playerAnimator.Play("yidong");
        }
        //足球图
        imageBall.color = Color.white;
    }

    /// <summary>
    /// 状态切换
    /// </summary>
    /// <param name="_state"></param>
    public void ChangePlayerState(playerState _state)
    {
        if (currentState == _state)
        {
            return;
        }
        else
        {
            currentState = _state;
            switch (currentState)
            {
                case playerState.Stand:
                    playerAnimator.Play("yidong");
                    imageBall.color = Color.white;
                    break;
                case playerState.Move:
                    playerAnimator.Play("yidong");                    
                    break;
                case playerState.Play:
                    playerAnimator.Play("tiqiu");
                    UIManager.GetInstance().game.GetComponent<Game>().ShowOrHidePowerUI(false);
                    break;
                case playerState.Wait:
                    playerAnimator.Play("yidong");
                    break;
                case playerState.Die:
                    dieTime = 150;
                    playerAnimator.Play("shouji");
                    //UIManager.GetInstance().game.GetComponent<Game>().ResetPower();
                    UIManager.GetInstance().game.GetComponent<Game>().ShowOrHidePowerUI(false);
                    GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundDie);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 启动保护
    /// </summary>
    public void SavePlayerStart()
    {
        playerSaveTime = 600;
        imageSave.color = Color.white;
    }

    /// <summary>
    /// 更新保护罩
    /// </summary>
    public void UpdateSave()
    {
        if (playerSaveTime > 0)
        {
            playerSaveTime--;
            imageSave.transform.Rotate(new Vector3(0, 0, -10));
        }
        else if (playerSaveTime == 0)
        {
            playerSaveTime = -1;
            imageSave.color = Color.clear;
        }
        else
        {

        }
    }

    void Update()
    {
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState == Game.GameState.GamePlay)
        {
            switch (currentState)
            {
                case playerState.Stand:
                    Move();
                    break;
                case playerState.Move:
                    Move();
                    break;
                case playerState.Play:
                    Move();
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("tiqiu") == true
                        && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("yidong") == false
                        && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                    {
                        imageBall.color = Color.clear;
                        CreateFootball();
                        //UIManager.GetInstance().game.GetComponent<Game>().ResetPower();
                        UIManager.GetInstance().game.GetComponent<Game>().ShowOrHidePowerUI(false);
                        ChangePlayerState(playerState.Wait);
                    }
                    break;
                case playerState.Wait:
                    Move();
                    break;
                case playerState.Die:
                    dieTime--;
                    if (dieTime % 10 == 0)
                    {
                        playerAnimator.gameObject.SetActive(true);
                        GetComponent<CanvasGroup>().alpha = 1;
                        GetComponent<CanvasGroup>().interactable = true;
                        GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                    else
                    {
                        playerAnimator.gameObject.SetActive(false);
                        GetComponent<CanvasGroup>().alpha = 0;
                        GetComponent<CanvasGroup>().interactable = false;
                        GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }
                    if (dieTime <= 0)
                    {
                        UIManager.GetInstance().game.GetComponent<Game>().LoseGame();
                    }
                    break;
                default:
                    break;
            }
            UpdateSave();
        }

    }

    /// <summary>
    /// 玩家移动
    /// </summary>
    public void Move()
    {
        Vector3 offset = touch.GetComponent<GameTouch>().GetoffsetVector3();
        if (offset.x > 0.4)
        {
            if (transform.position.x < 400)
            {
                transform.Translate(moveSpeed*Time.deltaTime,0,0);
            }
        }
        else if (offset.x < -0.4)
        {
            if (transform.position.x > -400)
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            }            
        }
        else
        {
        }
    }

    /// <summary>
    /// 生成足球
    /// </summary>
    public void CreateFootball()
    {
        GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundTi);
        float endX = (float)(-270 + (transform.position.x + 400) * 0.675f);//540除800=0.675
        endX = Mathf.Clamp(endX, -270, 270);
        myFootball.GetComponent<FootBall>().StartMove(imageBall.transform.position, new Vector3(endX, 114, 0));
    }

    /// <summary>
    /// 玩家受到攻击
    /// </summary>
    public void Hurt()
    {
        if (playerSaveTime > 0)
        {
            return;
        }
        else
        {
            ChangePlayerState(playerState.Die);
        }
    }
    
}
