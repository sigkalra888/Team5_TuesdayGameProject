using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    public GameObject menuPanel;//メニュー
    public GameObject desPanel;//メニュー

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenuPanel();
        }
    }

    //メニューを表示する
    private void ShowMenuPanel()
    {
        menuPanel.SetActive(true);
    }

    //okボタン押す
    public void OnYButtonDown()
    {
        SceneManager.LoadScene(0);
    }

    //noボタン押す
    public void OnNButtonDown()
    {
        menuPanel.SetActive(false);
    }

    //
    public void ShowDesPanel(string str)
    {
        desPanel.SetActive(true);
        desPanel.transform.GetChild(0).GetComponent<Text>().text = str;
    }

    //closeボタン押す
    public void OnCloseButtonDown()
    {
        desPanel.SetActive(false);
    }
}
