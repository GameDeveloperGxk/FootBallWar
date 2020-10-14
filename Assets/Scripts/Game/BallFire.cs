using UnityEngine;
using System.Collections;

/// <summary>
/// 道具足球
/// </summary>
public class BallFire : MonoBehaviour
{

    public int speed = 20;

    // Use this for initialization
    void Start()
    {

    }

    public void InitData(Vector3 _startPosition)
    {
        transform.position = _startPosition;
    }


    // Update is called once per frame
    void Update()
    {
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState == Game.GameState.GamePlay)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed, Space.Self);

            if (transform.position.y > 452)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "jiangshi")
        {
            GameObject.Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "cartnt")
        {
            GameObject.Destroy(gameObject);
            UIManager.GetInstance().game.GetComponent<Game>().enemyCar.GetComponent<EnemyCar>().ChangeState(EnemyCar.State.baozha);
        }
    }
}
