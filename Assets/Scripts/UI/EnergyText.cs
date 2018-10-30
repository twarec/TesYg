using UnityEngine;

public class EnergyText : MonoBehaviour
{
    public UnityEngine.UI.Text text;
    private void Start()
    {
        Game.ResoursesManager.obj.EnergyAction += UpdateText;
        UpdateText(Game.ResoursesManager.obj.Energy);
    }
    private void OnDestroy()
    {
        Game.ResoursesManager.obj.EnergyAction -= UpdateText;
    }
    private void UpdateText(int value)
    {
        text.text = value.ToString();
    }
}
