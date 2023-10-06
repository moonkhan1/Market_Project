using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] TMP_Text _gold;
    [SerializeField] MoneySO _data;

    private void Update()
    {
        SetData();
    }

    private void SetData()
    {
        _gold.text = _data.Gold.ToString();
    }
}
