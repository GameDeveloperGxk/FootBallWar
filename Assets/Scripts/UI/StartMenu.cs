using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public Button buttonStart;
    public Button buttonSound;
    public Button buttonExit;
    public Button buttonShop;
    public Image ImageSound;
    public Sprite[] spriteSound;

    private void Awake()
    {
        LocalData.GetInstance().LoadLocalData();
    }

    // Use this for initialization
    void Start ()
    {
        buttonStart.onClick.AddListener(StartClick);
        buttonSound.onClick.AddListener(SoundClick);
        buttonExit.onClick.AddListener(ExitClick);
        buttonShop.onClick.AddListener(ShopClick);
        GameController.GetInstance().soundState = 1;
        ImageSound.sprite = spriteSound[GameController.GetInstance().soundState];  
    }

    void StartClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.StartMenu)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.StartMenu, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, true);
        }
    }
    void SoundClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.StartMenu)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            GameController.GetInstance().soundState = GameController.GetInstance().soundState == 1 ? 0 : 1;
            ImageSound.sprite = spriteSound[GameController.GetInstance().soundState];
        }
    }
    void ExitClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.StartMenu)
        {
            LocalData.GetInstance().SaveLocalData();
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.ExitPanel, true);
        }
    }

    void ShopClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.StartMenu)
        {
            GameController.GetInstance().audioMgr.PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().shopPanel.GetComponent<ShopPanel>().nextStep = UIManager.UIStep.StartMenu;
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Shop, true);
        }
    }

}
