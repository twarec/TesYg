using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldText : MonoBehaviour
{
    public UnityEngine.UI.Text text;
    private void Start()
    {
        Game.ResoursesManager.obj.GoldAction += UpdateText;
        UpdateText(Game.ResoursesManager.obj.Gold);
    }
    private void OnDestroy()
    {
        Game.ResoursesManager.obj.GoldAction -= UpdateText;
    }
    private void UpdateText(int value)
    {
        text.text = value.ToString();
    }
}
