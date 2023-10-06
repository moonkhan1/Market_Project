using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
    public static TradeManager Instance { get; private set; }
    [SerializeField] public TMP_Text tempSpendMoney;
    [SerializeField] private MoneySO _money;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _diceButton;
    [SerializeField] private MoneySO _tempMoneySO;
    [SerializeField] private GameObject GambleTableObject;

    public event Action isItemsPurchased;
    public event Action isGambleSelected;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        _buyButton.onClick.AddListener(ButItems);
        _diceButton.onClick.AddListener(GambleSelection);
    }

    private void Update()
    {
        if (ShopManager.Instance.isSomethingPurchased)
        {
            _diceButton.interactable = true;
        }
        else
        {
            _diceButton.interactable = false;
        }
        CanPurchase();
    }

    private void ButItems()
    {
        _money.Gold = _money.Gold - _tempMoneySO.Gold;
        tempSpendMoney.text = 0.ToString();
        ShopManager.Instance.ClearTable();
        _tempMoneySO.Gold = 0;
        GambleTableObject.SetActive(false);
        isItemsPurchased?.Invoke();
    }

    private void CanPurchase()
    {
        _buyButton.interactable = _money.Gold >= _tempMoneySO.Gold;
    }
    private void GambleSelection()
    {
        GambleTableObject.SetActive(true);
        isGambleSelected?.Invoke();
    }


}
