using System.Collections;
using System.Collections.Generic;
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
}
