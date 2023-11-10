using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    [Header("阿宅操作按钮")]
    public Button leftAction;
    public Button rightAction;
    [Header("UI")]
    public GameObject levelTitle;
    public GameObject bookPanel;
    public Button bookIcon;
    [Header("悟性状态")]
    public GameObject bookStatus;
    private Text wuxingValue;
    private Button bookResetButton;
    private Button bookAddButton;
    public List<BookSlot> bookSlots =new List<BookSlot>();
    private void Awake()
    {
        if(_instance ==null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    private void Start()
    {
        leftAction.onClick.AddListener(GameManager._instance.unitMgr.Azhai.LeftAction);
        rightAction.onClick.AddListener(GameManager._instance.unitMgr.Azhai.RightAction);

        BattleObject._instance.OnLevelStart += OnLevelStart;

        InitBookPanel();
        InitBookStatus();
    }

    public void OnLevelStart(int levelIndex)
    {
        levelTitle.SetActive(true);
        levelTitle.GetComponentInChildren<Text>().text = "第" + levelIndex + "关";
        StartCoroutine(LevelTitle());
    }
    IEnumerator LevelTitle()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        levelTitle.SetActive(false);
    }

    public void InitBookPanel()
    {
        bookIcon.onClick.AddListener(()=>bookPanel.SetActive(!bookPanel.activeSelf));

        foreach (var bookData in GameManager._instance.bookDataRoot.BookDataDic)
        {
            GameObject uiObj = Resources.Load<GameObject>("BookSlot");
            BookSlot newBookSlot = new BookSlot(Instantiate(uiObj, bookPanel.transform.GetChild(0)));
            bookSlots.Add(newBookSlot);
            newBookSlot.UpdateUI(bookData.Value);
            bookData.Value.onBookUpgrade += newBookSlot.UpdateUI;
        }
    }

    public void InitBookStatus()
    {
        if(bookStatus!=null)
        {
            wuxingValue = bookStatus.transform.GetChild(1).GetComponent<Text>();
            bookResetButton = bookStatus.transform.GetChild(2).GetComponent<Button>();
            bookAddButton = bookStatus.transform.GetChild(3).GetComponent<Button>();

            bookResetButton.onClick.AddListener(GameManager._instance.bookDataRoot.ResetWuxing);
            bookAddButton.onClick.AddListener(GameManager._instance.bookDataRoot.AddWuxing);

            GameManager._instance.bookDataRoot.OnWuXingChange += (int wuxing) => { wuxingValue.text = wuxing.ToString(); };
        }
    }
}
