using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 足球
/// </summary>
public class FootBall : MonoBehaviour
{
    public int speed;
            
    public Vector3 startPosition;
    public Vector3 endPosition;

    public bool isMove = false;

    int rotate = 0;

    private Vector2 orignSize; //缓存原始尺寸

    public RectTransform targetRect; //被控制的 图片 

    public GameObject player;

    
    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
        orignSize = targetRect.sizeDelta;
    }

    public void InitData()
    {
        isMove = false;
        gameObject.SetActive(false);
    }

    public void StartMove(Vector3 _startPosition, Vector3 _endPosition)
    {
        gameObject.SetActive(true);
        targetRect.sizeDelta = orignSize;

        transform.position = _startPosition;
        startPosition = _startPosition;
        endPosition = _endPosition;

        isMove = true;

        speed = (308+UIManager.GetInstance().game.GetComponent<Game>().playerPower)/2+50;
        speed = Mathf.Clamp(speed, 50, 200);
    }

   
    public void Hide()
    {
        isMove = false;
        gameObject.SetActive(false);
        player.GetComponent<Player>().ChangePlayerState(Player.playerState.Stand);
        UIManager.GetInstance().game.GetComponent<Game>().ResetPower();
    }   
    

    // Update is called once per frame
    void Update()
    {
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState == Game.GameState.GamePlay)
        {
            if (isMove)
            {
                //旋转
                rotate -= 10;
                transform.localEulerAngles = new Vector3(0, 0, rotate);
                //缩放
                float delta = 1 - (transform.position.y + 300) / 1000;
                delta = Mathf.Clamp(delta, 1.0f, 1.3f);
                transform.localScale = new Vector3(delta, delta, 1);
                //移动
                transform.position = Vector3.MoveTowards(transform.position, endPosition, 4 * speed * Time.deltaTime);
                if (transform.position.y >= 100)
                {
                    Hide();
                    //if(transform.position.x>=-100&& transform.position.x <= 100)
                    //{
                    //    UIManager.GetInstance().game.GetComponent<Game>().ChangeGameScore(15);
                    //}
                }
            }
        }           
    }

    /// <summary>
    /// 碰撞
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string _tag = collision.gameObject.tag;
        switch (_tag) {
            case "jiangshi":
            case "cartnt":
            case "wang0":
                Hide();
                break;
            case "wang":
                Hide();
                UIManager.GetInstance().game.GetComponent<Game>().ChangeGameScore(15);
                break;
            default:
                break;
        }
    }
}
