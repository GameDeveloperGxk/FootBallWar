using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTask : MonoBehaviour {

    public Text textTaskInfo;
    public Text textStar0;
    public Text textStar1;
    public Text textStar2;
    
    // Use this for initialization
    void Start () {
        InitData();
	}

    public void InitData()
    {
        textTaskInfo.text = "" + GameController.GetInstance().gameWinScore[GameController.GetInstance().currentLevel];
        textStar0.text = "<=" + (30 + GameController.GetInstance().currentLevel * 5);
        textStar1.text = "<=" + (30 + GameController.GetInstance().currentLevel * 5) * 2;
        textStar2.text = ">" + (30 + GameController.GetInstance().currentLevel * 5) * 2;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
