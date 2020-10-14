using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏金币奖励
/// </summary>
public class CoinReward : MonoBehaviour {

    private float speed = 700f;

    private float delta = 1.0f;
    
	// Use this for initialization
	void Start () {
	
	}

    public void InitData(float _delta)
    {
        delta = _delta;
        transform.localScale = new Vector3(delta, delta, 1);
    }    
	
	// Update is called once per frame
	void Update () {	
        if( UIManager.GetInstance().game.GetComponent<Game>().currentGameState == Game.GameState.GamePlay)
        {
            //缩放
            delta -= 0.01f;
            delta = Mathf.Clamp(delta, 0.4f, 1.0f);
            transform.localScale = new Vector3(delta*100, delta*100, 1);
            //移动
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(512,306,-50)
                        , speed * Time.deltaTime);
            if (transform.position.x >=505)
            {
                GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundCoin);
                GameObject.Destroy(gameObject);
                LocalData.GetInstance().SetCoin(Random.Range(1, 3));
                UIManager.GetInstance().game.GetComponent<Game>().textUICoin.text = LocalData.GetInstance().GetCoin() + "";
            }
        }
	}
}
