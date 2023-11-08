using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class BattleObject : MonoBehaviour
{
    public static BattleObject _instance;
    public event Action<int> OnLevelStart;
    public event Action<int> OnLevelEnd;
    public int currentLevel;
    public ChapterConfigRoot chapterDataRoot;
    private void Awake()
    {
        if(_instance==null)
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
        chapterDataRoot = new ChapterConfigRoot();
        currentLevel = 1;
        StartLevel(currentLevel);
    }

    //循环关卡  一共配了三关 一关近战，一关远程，一关远近混合
    public void StartLevel(int levelID)
    {
        OnLevelStart?.Invoke(currentLevel);
        int index = currentLevel % chapterDataRoot.chapterDatas.Count;
        index = index == 0 ? chapterDataRoot.chapterDatas.Count : index;
        Debug.Log("第" + currentLevel + "关开始了");
        if (chapterDataRoot.chapterDatas.ContainsKey(index))
        {
            chapterDataRoot.chapterDatas[index].WaveStart();
            chapterDataRoot.chapterDatas[index].onChapterFinish += StartLevel;
        }
        currentLevel++;
    }
}
