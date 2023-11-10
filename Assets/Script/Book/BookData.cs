using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookData 
{
    public int isUnLock;
    public int isLearned;
    public int currentLevel;
    public BookConfig bookConfig;

    public event Action<BookData> onBookUpgrade;
    public BookData(BookConfig bookConfig)
    {
        isUnLock = 0;
        isLearned = 0;
        currentLevel = 0;
        this.bookConfig = bookConfig;
    }

    public void Reset()
    {
        isUnLock = 0;
        isLearned = 0;
        currentLevel = 0;
        onBookUpgrade?.Invoke(this);
    }

    public void LearnBook()
    {
        isLearned = 1;
        currentLevel = 1;
        onBookUpgrade?.Invoke(this);
    }

    public void UpGrade()
    {
        onBookUpgrade?.Invoke(this);
    }
}
