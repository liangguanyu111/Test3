using Spine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BookSlot
{
    public Sprite iconSprite;
    public Text bookName;
    public Text bookDescription;
    public Button levelUpButton;
    public Text levelUpDescription;
    public Text levelUpCost;
    public Text levelText;
    public BookSlot(GameObject slotObj)
    {
        iconSprite = slotObj.transform.GetChild(0).GetComponent<Image>().sprite;
        bookName = slotObj.transform.GetChild(1).GetComponent<Text>();
        bookDescription = slotObj.transform.GetChild(2).GetComponent<Text>();
        levelUpButton = slotObj.transform.GetChild(3).GetChild(2).GetComponent<Button>();
        levelUpDescription = slotObj.transform.GetChild(3).GetChild(3).GetComponent<Text>();
        levelUpCost = slotObj.transform.GetChild(3).GetChild(4).GetChild(1).GetComponent<Text>();
        levelText = slotObj.transform.GetChild(4).GetComponent<Text>();

        GameManager._instance.bookDataRoot.OnWuXingChange += UpdateButtonStatus;
    }

    public void UpdateUI(BookData bookData)
    {
        bookName.text = bookData.bookConfig.bookName;

        string description =  bookData.bookConfig.bookDescription;
        string currentLevelValue = null;
        string nextLevelValue = null;
        //当前等级的数值说明
        if(bookData.isLearned != 0)
        {
            currentLevelValue = (bookData.bookConfig.levelEffect[bookData.currentLevel] * 100).ToString() + "%";
        }
        if(bookData.currentLevel<bookData.bookConfig.levelEffect.Count-1)
        {
            nextLevelValue = "(+"+(bookData.bookConfig.levelEffect[bookData.currentLevel] * 100).ToString() + "%)";
        }
        //下一等级数值说明
        description = description.Replace("{value}", currentLevelValue+"<color=\"#EE4000\"><size=10>" + nextLevelValue + "</size></color> ");
        bookDescription.text = description;
        levelText.text = "等级" + bookData.currentLevel.ToString() + "/" + bookData.bookConfig.levelEffect.Count;

        UpdateButtonStatus(GameManager._instance.bookDataRoot.wuxingValue);
    }

    //按钮的逻辑依靠悟性值
    public void UpdateButtonStatus(int wuxingValue)
    {
        
    }

}
