using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    public ShopItemSO[] _shopItemSO;
    public ShopItem[] _shopItem;
    public Button[] _items;
    private List<PurchasedItem> _purchasedItemsList;
    [SerializeField] private PurchasedItem purchasedItem;
    [SerializeField] MoneySO _moneyData;
    [SerializeField] private GameObject tradePanelObject;
    [SerializeField] private MoneySO _tempSpendMoney;
    public bool isSomethingPurchased => _purchasedItemsList.Count > 0;
    [Serializable]
    public enum ItemType
    {
        Sword = 0,
        Shield = 1,
        MagicBook = 2,
        Hammer = 3,
        PurpleCrystal = 4,
        GreenCrystal = 5,
        BlueCrystal = 6,
        Egg = 7,
        Bomb = 8
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _purchasedItemsList = new List<PurchasedItem>();
        SetStats();
    }

    private void Update()
    {
        CanPurchase();
    }

    private void SetStats()
    {
        _tempSpendMoney.Gold = 0;
        for (int i = 0; i < _shopItemSO.Length; i++)
        {
            _shopItem[i].Price.text = _shopItemSO[i].Price.ToString();
            _shopItem[i].Name.text = _shopItemSO[i].Name;
            _shopItem[i].ProductImage.sprite = _shopItemSO[i].ProductImage;
            _shopItem[i].ItemCount.text =  "x" + _shopItemSO[i].ItemCount;
        }
    }
    public void CanPurchase()
    {
        for (int i = 0; i < _shopItemSO.Length; i++)
        {
            if (_moneyData.Gold >= _shopItemSO[i].Price && _shopItemSO[i].ItemCount > 0)
            {
                _items[i].interactable = true;
            }
            else
            {
                _items[i].interactable = false;
            }
        }
    }
    
    public void BuyItem(int itemType)
    {
        ItemType currentType = (ItemType)itemType;
        int insertIndex = Mathf.Max(0, _purchasedItemsList.Count);
        
        for (int i = 0; i < _shopItemSO.Length; i++)
        {
            if (_shopItem[i].CompareTag(currentType.ToString()))
            {
                ShopItemSO currentItem = _shopItemSO[i];
                int currentPrice = currentItem.Price;
                int currentItemCount = currentItem.ItemCount;

                if (_moneyData.Gold >= currentPrice && currentItemCount > 0)
                {
                    TMP_Text spendMoney = TradeManager.Instance.tempSpendMoney;
                    spendMoney.text = (int.Parse(spendMoney.text) + currentPrice).ToString();
                    _tempSpendMoney.Gold = int.Parse(spendMoney.text);

                    currentItemCount--;
                    currentItem.ItemCount = currentItemCount;
                    _shopItem[i].ItemCount.text = "x" + currentItemCount;

                    PurchasedItem existingPurchasedItem =
                        _purchasedItemsList.Find(item => item.CompareTag(currentType.ToString()));
                    if (existingPurchasedItem != null)
                    {
                        int purchasedItemCount = int.Parse(existingPurchasedItem.ItemCount.text);
                        purchasedItemCount++;
                        existingPurchasedItem.ItemCount.text = purchasedItemCount.ToString();
                    }
                    else
                    {
                        float spacing = 40.0f; // Spacing between each object
                        int objectsPerRow = 3;
                        
                        float x = insertIndex % objectsPerRow * spacing;
                        float y = -(insertIndex / objectsPerRow * spacing);
                        Vector2 position = new Vector2(x, y);

                        var newPurchasedItem = Instantiate(purchasedItem, tradePanelObject.transform);
                        Vector3 defaultPos = position;
                        newPurchasedItem.transform.localPosition = defaultPos;
                        newPurchasedItem.ItemImage.sprite = _shopItem[i].ProductImage.sprite;
                        newPurchasedItem.ItemName.text = _shopItem[i].Name.text;
                        newPurchasedItem.tag = _shopItem[i].tag;
                        _purchasedItemsList.Insert(insertIndex, newPurchasedItem);
                    }
                }
            }
        }
    }
    public void ClearTable()
    {
        foreach (Transform child in tradePanelObject.transform)
        {
            GameObject.Destroy(child.gameObject);
            _purchasedItemsList.Clear();
        }
    }
    
}



