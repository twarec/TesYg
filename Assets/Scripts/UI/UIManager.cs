using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class UIManager : MonoBehaviour {
    public static UIManager obj;
    private void Awake()
    {
        obj = this;
        RemoveAllChildrenUnitPanel();
    }
    #region Панель построик
    /// <summary>
    /// Панель списка зданий на постройку
    /// </summary>
    [SerializeField]
    private GameObject buidsPanel;
    public GameObject BuidsPanel
    {
        get
        {
            return buidsPanel;
        }
    }
    [SerializeField]
    private Button BuildUiButton;
    private Tweener tweenerBuild;
    private BuildProcessUI processUI = BuildProcessUI.Disactive;
    public void AddBuildRange(Build[] builds)
    {
        int count = buidsPanel.transform.childCount;
        for (int i = 0; i < count; i++)
            Destroy(buidsPanel.transform.GetChild(i).gameObject);
        count = builds.Length;
        for (int i = 0; i < count; i++)
        {
            Button button = Instantiate(BuildUiButton, buidsPanel.transform);
            Build build = builds[i];
            button.onClick.AddListener(() => UnitStart(build, button));
            Text BtText = button.GetComponentInChildren<Text>();
            if (BtText)
                BtText.text = build.name;
        }
    }
    private void UnitStart(Build build, Button button)
    {
        if (processUI != BuildProcessUI.Disactive)
            return;
        if (Game.ResoursesManager.obj.Gold < build.PriceGold)
        {
            print("недостаточно золота");
            return;
        }
        Game.ResoursesManager.obj.Gold -= build.PriceGold;
        Image image = button.GetComponentInChildren<Image>();
        tweenerBuild = DOTween.To(() => 0f, (x) => image.fillAmount = x, 1f, build.Timer).SetEase(Ease.Linear)
            .OnStart(() =>
            {
                processUI = BuildProcessUI.Process;
                SetInterectiveButton(false);
                button.interactable = true;

            })
            .OnComplete(() =>
            {
                processUI = BuildProcessUI.Active;
            });
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            switch (processUI)
            {
                case BuildProcessUI.Process:
                    if (tweenerBuild != null)
                        tweenerBuild.Kill();
                    tweenerBuild = DOTween.To(() => image.fillAmount, (x) => image.fillAmount = x, 1f, .125f)
                    .OnComplete(() =>
                    {
                        SetInterectiveButton(true);
                        button.onClick.RemoveAllListeners();
                        button.onClick.AddListener(() => UnitStart(build, button));
                        processUI = BuildProcessUI.Disactive;
                        Game.ResoursesManager.obj.Gold += build.PriceGold;
                    });
                    break;
                case BuildProcessUI.Active:
                    BuildManager.obj.BuildSelect = build;
                    break;
            }
            BuildManager.obj.BuildComplite = (b) =>
            {
                SetInterectiveButton(true);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => UnitStart(build, button));
                processUI = BuildProcessUI.Disactive;
            };
        });
    }
    public void SetVisibleBuildePanel(bool visible)
    {
        buidsPanel.SetActive(visible);
    }
    public void SetInterectiveButton(bool active)
    {
            BuidsPanel.GetComponentsInChildren<Button>().ForeAche((v) =>
            {
                v.interactable = active;
            });

    }
    #endregion
    #region Панель юнитов
    [SerializeField]
    private Transform SelecteblObjectPanel;
    [SerializeField]
    private UnitPanel UnitImage;
    public void SetActiveUnitPanel(bool active)
    {
        SelecteblObjectPanel.gameObject.SetActive(active);
    }
    public void RemoveAllChildrenUnitPanel()
    {
        int count = SelecteblObjectPanel.childCount;
        for (int i = 0; i < count; i++)
            Destroy(SelecteblObjectPanel.GetChild(i).gameObject);
    }
    public void SetUnit(SelectebleObject selectebleObject)
    {
        int count = SelecteblObjectPanel.childCount;
        for (int i = 0; i < count; i++)
        {
            var v = SelecteblObjectPanel.GetChild(i).GetComponent<UnitPanel>();
            if (selectebleObject.Name == v.unit.Name)
            {
                v.Count++;
                v.Hp = -1;
                return;
            }

        }
            UnitPanel unit = Instantiate(UnitImage, SelecteblObjectPanel);
            var destroyd = selectebleObject.GetComponent<DestroydObject>();
            if (destroyd != null)
            {
                unit.Hp = destroyd.Hp / destroyd.MaxHp;
            }
            else
            {
                unit.Hp = -1;
            }
            unit.Icon = selectebleObject.sprite;
            unit.unit = selectebleObject;
        unit.Count = 1;
    }
    public void RemoveUnit(SelectebleObject selectebleObject)
    {
        int count = SelecteblObjectPanel.childCount;
        for(int i = 0; i < count; i++)
        {
            var v = SelecteblObjectPanel.GetChild(i).GetComponent<UnitPanel>();
            if (selectebleObject.Name == v.unit.Name)
            {
                if (--v.Count <= 0)
                    Destroy(SelecteblObjectPanel.GetChild(i).gameObject);
                else if (v.Count == 1)
                {
                    var dest = selectebleObject.GetComponent<DestroydObject>();
                    if(dest != null)
                        v.Hp = dest.Hp / dest.MaxHp;
                }
                break;
            }
        }
    }
    #endregion
}
public enum BuildProcessUI
{
    Disactive,
    Process,
    Active
}
