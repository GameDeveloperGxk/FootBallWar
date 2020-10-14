using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 敌人
/// </summary>
public class EnemyCar : MonoBehaviour
{
    public Animator enemyCarAnimator;

    private int createTime = 5;

    private int currentTime = 0;
    public enum State
    {
        baozha,//
        zou,//
        wait,
    }
    
    public State currentState;

    private float speed = 250;

    void Start()
    {        
       
    }

    public void InitPosition()
    {
        float _y = Random.Range(-150, -10);
        transform.position = new Vector3(-900,_y,_y);
        transform.localScale = new Vector3(0.8f+Mathf.Abs(transform.position.y)/40*0.2f, 0.8f + Mathf.Abs(transform.position.y) / 40 * 0.2f,1);
        currentState = State.wait;
        enemyCarAnimator.speed = 0.4f;
        createTime = Random.Range(5, 10);
        currentTime = GameController.GetInstance().currentLevelTime;
    }
   

    /// <summary>
    /// 状态切换
    /// </summary>
    /// <param name="_state"></param>
    public void ChangeState(State _state)
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
                case State.baozha:
                    enemyCarAnimator.Play("baozha");
                    GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundBoom);
                    break;
                case State.zou:
                    enemyCarAnimator.Play("zou");
                    break;
                case State.wait:
                    
                    break;
                default:
                    break;
            }
        }
    }

    void Update()
    {
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState == Game.GameState.GamePlay)
        {
            switch (currentState)
            {
                case State.baozha:
                    if (enemyCarAnimator.GetCurrentAnimatorStateInfo(0).IsName("baozha")
                        && enemyCarAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                    {
                        UIManager.GetInstance().game.GetComponent<Game>().EnemyHurtAll(60);
                        InitPosition();
                    }
                    break;
                case State.zou:
                    transform.Translate(speed*Time.deltaTime, 0, 0);
                    if(transform.position.x >= 710)
                    {
                        InitPosition();
                    }
                    break;
                case State.wait:
                    if(GameController.GetInstance().currentLevelTime-currentTime>=createTime)
                    {
                        ChangeState(State.zou);
                    }
                    break;               
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 碰撞
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "football")
        {
            ChangeState(State.baozha);
        }
        else if(collision.gameObject.tag == "ballfire")
        {
            ChangeState(State.baozha);
        }
    }
}