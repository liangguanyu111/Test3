using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterConfigRoot
{
    public Dictionary<int,ChapterData> chapterDatas;

    public ChapterConfigRoot()
    {
        chapterDatas = new Dictionary<int, ChapterData>();
        List<ChapterConfig> chapterConfigs = new List<ChapterConfig>();
        JsaonMgr.LoadFromPath<ChapterConfig>("/chapterCfg.txt", out chapterConfigs);
        foreach (var chapterConfig in chapterConfigs)
        {
            ChapterData newChapterData = new ChapterData(chapterConfig);
            chapterDatas.Add(newChapterData.chapterCfg.chapterID,newChapterData);
        }
    }

   public void NextWave(ChapterWaveConfig nextWaveConfig)
   {

   }
}
