using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookConfigRoot 
{
    public Dictionary<int, BookConfig> bookConfigDic;
    public BookConfigRoot()
    {
        bookConfigDic = new Dictionary<int, BookConfig>();
        List<BookConfig> bookConfigs = new List<BookConfig>();
        JsaonMgr.LoadFromPath<BookConfig>("/bookCfg.txt", out bookConfigs);
        foreach (var BookConfig in bookConfigs)
        {
            bookConfigDic.Add(BookConfig.bookId, BookConfig);
        }
    }
}
