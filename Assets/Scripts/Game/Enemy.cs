using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 敌人
/// </summary>
public class Enemy : MonoBehaviour
{
    public int ID;

    public DragonBone.DragonBoneArmature enemyArmature0;
    public DragonBone.DragonBoneArmature enemyArmature1;
    public DragonBone.DragonBoneArmature enemyArmature2;
    public DragonBone.DragonBoneArmature enemyArmature3;
    public DragonBone.DragonBoneArmature enemyArmature4;
    public DragonBone.DragonBoneArmature enemyArmature5;
    public DragonBone.DragonBoneArmature enemyArmature6;
    public DragonBone.DragonBoneArmature enemyArmature7;
    public DragonBone.DragonBoneArmature enemyArmature8;
    public DragonBone.DragonBoneArmature enemyArmature9;

    public Transform enemyFather;

    /// <summary>
    /// 玩家状态
    /// </summary>
    public enum enemyState
    {
        gongji,//
        shouji,//
        siwang,//
        xuanyun,//
        zou,//
    }
    /// <summary>
    /// 玩家当前状态
    /// </summary>
    public enemyState currentState;

    private float speed;

    public Image imageHP;

    private float HP;//取值40-120

    private float zz;

    public bool hitPlayer;


    private float delta = 1.0f;
    /// <summary>
    /// 移动状态 0左侧1右侧2朝向玩家
    /// </summary>
    private int moveState = 2;

    private bool isHit = false;

    private int xuanyunTime = 0;

    // Use this for initialization
    void Start()
    {

    }
    public void InitData(int _id, float _hp, float _z,int _moveState)
    {
        ID = _id;
        InitMovieclipEnable();

        HP = _hp;
        imageHP.GetComponent<RectTransform>().sizeDelta = new Vector2(HP, imageHP.GetComponent<RectTransform>().rect.height);

        zz = _z;
        transform.position = new Vector3(transform.position.x, transform.position.y, zz);
        transform.localScale = new Vector3(0.5f, 0.5f, 1);

        moveState = _moveState;

        currentState = enemyState.zou;
        GetEnemyArmature().GetComponent<Animator>().Play("zou");

        speed = Random.Range(25f,50f);
        hitPlayer = false;
        
        isHit = false;
    }

    public void InitMovieclipEnable()
    {
        enemyArmature0.GetComponent<Animator>().gameObject.SetActive(false);
        enemyArmature1.GetComponent<Animator>().gameObject.SetActive(false);
        enemyArmature2.GetComponent<Animator>().gameObject.SetActive(false);
        enemyArmature3.GetComponent<Animator>().gameObject.SetActive(false);
        enemyArmature4.GetComponent<Animator>().gameObject.SetActive(false);
        enemyArmature5.GetComponent<Animator>().gameObject.SetActive(false);
        enemyArmature6.GetComponent<Animator>().gameObject.SetActive(false);
        enemyArmature7.GetComponent<Animator>().gameObject.SetActive(false);
        enemyArmature8.GetComponent<Animator>().gameObject.SetActive(false);
        enemyArmature9.GetComponent<Animator>().gameObject.SetActive(false);
        switch (ID)
        {
            case 0:
                enemyArmature0.GetComponent<Animator>().gameObject.SetActive(true);
                break;
            case 1:
                enemyArmature1.GetComponent<Animator>().gameObject.SetActive(true);
                break;
            case 2:
                enemyArmature2.GetComponent<Animator>().gameObject.SetActive(true);
                break;
            case 3:
                enemyArmature3.GetComponent<Animator>().gameObject.SetActive(true);
                break;
            case 4:
                enemyArmature4.GetComponent<Animator>().gameObject.SetActive(true);
                break;
            case 5:
                enemyArmature5.GetComponent<Animator>().gameObject.SetActive(true);
                break;
            case 6:
                enemyArmature6.GetComponent<Animator>().gameObject.SetActive(true);
                break;
            case 7:
                enemyArmature7.GetComponent<Animator>().gameObject.SetActive(true);
                break;
            case 8:
                enemyArmature8.GetComponent<Animator>().gameObject.SetActive(true);
                break;
            case 9:
            default:
                enemyArmature9.GetComponent<Animator>().gameObject.SetActive(true);
                break;
        }
    }

    public DragonBone.DragonBoneArmature GetEnemyArmature()
    {
        switch (ID)
        {
            case 0: return enemyArmature0;
            case 1: return enemyArmature1;
            case 2: return enemyArmature2;
            case 3: return enemyArmature3;
            case 4: return enemyArmature4;
            case 5: return enemyArmature5;
            case 6: return enemyArmature6;
            case 7: return enemyArmature7;
            case 8: return enemyArmature8;
            case 9:
            default: return enemyArmature9;
        }
    }

    /// <summary>
    /// 状态切换
    /// </summary>
    /// <param name="_state"></param>
    public void ChangeState(enemyState _state)
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
                case enemyState.gongji:
                    GetEnemyArmature().GetComponent<Animator>().Play("gongji");
                    break;
                case enemyState.shouji:
                    GetEnemyArmature().GetComponent<Animator>().Play("shouji");
                    break;
                case enemyState.siwang:
                    GetEnemyArmature().GetComponent<Animator>().Play("siwang");
                    transform.GetComponent<BoxCollider2D>().enabled = false;
                    UIManager.GetInstance().game.GetComponent<Game>().EnemyNumReduce();
                    break;
                case enemyState.xuanyun:
                    GetEnemyArmature().GetComponent<Animator>().Play("xuanyun");
                    float _backY = transform.position.y + 50;
                    if (_backY >= 136)
                    {
                        _backY = 136;
                    }
                    transform.position = new Vector3(transform.position.x,_backY,transform.position.z);
                    //缩放
                    float _delta = 0.5f + (float)((136 - transform.position.y) / 400);
                    _delta = Mathf.Clamp(_delta, 0.5f, 1.0f);
                    transform.localScale = new Vector3(_delta, _delta, 1);
                    delta = _delta;
                    xuanyunTime = GameController.GetInstance().currentLevelTime;
                    break;
                case enemyState.zou:
                    GetEnemyArmature().GetComponent<Animator>().Play("zou");
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
                case enemyState.gongji:
                    if (GetEnemyArmature().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("gongji")
                        && GetEnemyArmature().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                    {
                        if (isHit)
                        {
                            UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().Hurt();
                        }
                        else
                        {
                            ChangeState(enemyState.zou);
                        }                        
                    }                    
                    break;
                case enemyState.shouji:
                    if (GetEnemyArmature().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("shouji")
                        && GetEnemyArmature().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                    {
                        ChangeState(currentState == enemyState.gongji ? enemyState.gongji : enemyState.zou);
                    }
                    break;
                case enemyState.siwang:
                    if (GetEnemyArmature().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("siwang")
                       && GetEnemyArmature().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                    {
                        GameObject.Destroy(gameObject);
                        UIManager.GetInstance().game.GetComponent<Game>().CreateRewardCoin(transform.position, delta);
                        UIManager.GetInstance().game.GetComponent<Game>().ChangeGameScore(5);
                    }
                    break;
                case enemyState.xuanyun:
                    if (GetEnemyArmature().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("xuanyun")
                       && GetEnemyArmature().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 2.0f)
                    {
                        if (GameController.GetInstance().currentLevelTime - xuanyunTime >= 4)
                        {
                            ChangeState(enemyState.zou);
                        }                        
                    }
                    break;
                case enemyState.zou:
                    //缩放
                    float _delta = 0.5f + (float)((136 - transform.position.y) / 400);
                    _delta = Mathf.Clamp(_delta, 0.5f, 1.0f);
                    transform.localScale = new Vector3(_delta, _delta, 1);
                    delta = _delta;
                    //移动
                    if (moveState==0)
                    {
                        transform.position = Vector3.MoveTowards(transform.position,new Vector3(-180,0,0), speed * Time.deltaTime);
                        if(transform.position.y <= 5)
                        {
                            moveState = 2;
                        }
                    }
                    else if(moveState==1)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(180, 0, 0), speed * Time.deltaTime);
                        if (transform.position.y <= 5)
                        {
                            moveState = 2;
                        }
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position,
                        UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().transform.position
                        , speed * Time.deltaTime);
                    }                 
                    
                    if (transform.position.y <= -322)
                    {
                        //ChangeState(enemyState.gongji);
                        transform.position = new Vector3(transform.position.x, -322, -322);
                    }
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 受到攻击
    /// </summary>
    public void Hurt(int _value)
    {
        HP -= _value;
        GameController.GetInstance().audioMgr.PlaySound(
            new int[] { AudioManager.SoundHit, AudioManager.SoundHit1, AudioManager.SoundHit2, AudioManager.SoundHit3, AudioManager.SoundHit4 }[Random.Range(0, 5)]);
        if (HP <= 0)
        {
            HP = 0;
            imageHP.GetComponent<RectTransform>().sizeDelta = new Vector2(HP, imageHP.GetComponent<RectTransform>().rect.height);
            ChangeState(enemyState.siwang);
        }
        else
        {
            imageHP.GetComponent<RectTransform>().sizeDelta = new Vector2(HP, imageHP.GetComponent<RectTransform>().rect.height);
            if (_value >= 40)
            {
                ChangeState(enemyState.xuanyun);
            }
            else
            {
                ChangeState(enemyState.shouji);
            }
        }
    }


    /// <summary>
    /// 碰撞
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            isHit = true;
            if (UIManager.GetInstance().game.GetComponent<Game>().player.GetComponent<Player>().playerSaveTime > 0)
            {
                ChangeState(enemyState.siwang);
            }
            else
            {
                ChangeState(enemyState.gongji);
            }
        }
        else if (collision.gameObject.tag == "football")
        {
            Hurt(UIManager.GetInstance().game.GetComponent<Game>().GetPowerToHP());
        }
        else if (collision.gameObject.tag == "ballfire")
        {
            Hurt(60);
        }
        else
        {
            hitPlayer = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            isHit = false;
        }
    }
}