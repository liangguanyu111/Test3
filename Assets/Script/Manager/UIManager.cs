using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    [Header("Unit Transform")]
    public Transform unitTransform;
    [Header("阿宅操作按钮")]
    public Button leftAction;
    public Button rightAction;
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
        StartCoroutine(WaitForFrameEnd());
        leftAction.onClick.AddListener(GameManager._instance.unitMgr.Azhai.LeftAction);
        rightAction.onClick.AddListener(GameManager._instance.unitMgr.Azhai.RightAction);
    }

    IEnumerator WaitForFrameEnd()
    {
        yield return new WaitForFixedUpdate();
    }
}
