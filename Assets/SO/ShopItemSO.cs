using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "Market/ShopItemSO", order = 51)]
public class ShopItemSO : ScriptableObject 
{
    public string Name;
    public int Price;
    public Sprite ProductImage;
    public int ItemCount;



}
