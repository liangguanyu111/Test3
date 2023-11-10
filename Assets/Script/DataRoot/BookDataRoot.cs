using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDataRoot 
{
    //当前悟性值，暂时就不做保存了，要用直接加
    public int wuxingValue;
    public event Action<int> OnWuXingChange;

    public BookConfigRoot bookConfigRoot;
    public Dictionary<int, BookData> BookDataDic;
    public List<BookSlot> bookSlots;
    public BookDataRoot()
    {
        wuxingValue = 0;
        bookConfigRoot = new BookConfigRoot();
        BookDataDic = new Dictionary<int, BookData>();
        bookSlots = new List<BookSlot>();
        List<BookData> bookDataList = new List<BookData>();
        //有数据的情况
        if (JsaonMgr.LoadFromPath<BookData>("/BookData.txt", out bookDataList))
        {
            foreach (var bookData in bookDataList)
            {
                BookDataDic.Add(bookData.bookConfig.bookId, bookData);
            }
        }
        else
        {
            foreach (var bookConfig in bookConfigRoot.bookConfigDic)
            {
                BookDataDic.Add(bookConfig.Key, new BookData(bookConfig.Value));
            }
        }

    }

    public void SaveBookData()
    {
        List<BookData> bookDataList = new List<BookData>();
        foreach (var bookData in BookDataDic)
        {
            bookDataList.Add(bookData.Value);
        }
        JsaonMgr.WriteToPath<BookData>("/BookData.txt", bookDataList);
    }


    public void ResetWuxing()
    {
        //返还所有已经学习过的悟性
        wuxingValue = 0;
        OnWuXingChange?.Invoke(wuxingValue);
    }
    public void AddWuxing()
    {
        wuxingValue++;
        OnWuXingChange?.Invoke(wuxingValue);
    }

    public void CosetWuXing(int cost)
    {
        wuxingValue -=cost;
        OnWuXingChange?.Invoke(wuxingValue);
    }
}
