using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GambleController : MonoBehaviour
{
    [SerializeField] private Button _up;
    [SerializeField] private Button _down;
    [SerializeField] private Dice _dice;
    [SerializeField] private MoneySO _tempMoneySO;
    [SerializeField] private Transform _playerDiceTransform;
    [SerializeField] private Transform _traderDiceTransform;
    [SerializeField] private TMP_Text _newPrice;
    [SerializeField] public TMP_Text tempSpendMoney;
    private Dice _playerDice;
    private Dice _traderDice;

    private bool isChose;
    private enum UpOrDown
    {
        Up,
        Down
    }

    private UpOrDown _upOrDown;

    private void Start()
    {
        gameObject.SetActive(false);
        _up.onClick.AddListener(UpButton);
        _down.onClick.AddListener(DownButton);

        TradeManager.Instance.isItemsPurchased += ClearDices;
        TradeManager.Instance.isGambleSelected += GambleScreenOpen;
    }

    private void GambleScreenOpen()
    {
        EnableButtons();
        _playerDice = Instantiate(_dice, transform);
        _playerDice.transform.position = _playerDiceTransform.position;
        _newPrice.text = 0.ToString();
    }

    private void Update()
    {
        if (isChose)
        {
            _tempMoneySO.Gold = int.Parse(tempSpendMoney.text);
            _traderDice = Instantiate(_dice, transform);
            _traderDice.transform.position = _traderDiceTransform.position;
            switch (_upOrDown)
            {
                case UpOrDown.Up:
                {
                    if (_traderDice.diceNumber > _playerDice.diceNumber)
                    {
                        _tempMoneySO.Gold = _tempMoneySO.Gold - Convert.ToInt32(_tempMoneySO.Gold * 0.1);
                        _newPrice.text = _tempMoneySO.Gold.ToString();
                    }
                    else if(_traderDice.diceNumber < _playerDice.diceNumber )
                    {
                        _tempMoneySO.Gold = _tempMoneySO.Gold + Convert.ToInt32(_tempMoneySO.Gold * 0.1);
                        _newPrice.text = _tempMoneySO.Gold.ToString();
                    }

                    else
                    {
                        _newPrice.text = _tempMoneySO.Gold.ToString();
                    }
                    break;
                }
                case UpOrDown.Down:
                {
                    if (_traderDice.diceNumber < _playerDice.diceNumber)
                    {
                        _tempMoneySO.Gold = _tempMoneySO.Gold - Convert.ToInt32(_tempMoneySO.Gold * 0.1);
                        _newPrice.text = _tempMoneySO.Gold.ToString();
                    }
                    else if(_traderDice.diceNumber > _playerDice.diceNumber)
                    {
                        _tempMoneySO.Gold = _tempMoneySO.Gold + Convert.ToInt32(_tempMoneySO.Gold * 0.1);
                        _newPrice.text = _tempMoneySO.Gold.ToString();
                    }
                    else
                    {
                        _newPrice.text = _tempMoneySO.Gold.ToString();
                    }
                    break;
                }
            }
            isChose = false;
        }
    }

    private void UpButton()
    {
        _upOrDown = UpOrDown.Up;
        isChose = true;
        DisableButtons();
    }
    
    private void DownButton()
    {
        _upOrDown = UpOrDown.Down;
        isChose = true;
        DisableButtons();
    }

    private void DisableButtons()
    {
        _up.interactable = false;
        _down.interactable = false;
    }
    private void EnableButtons()
    {
        _up.interactable = true;
        _down.interactable = true;
    }

    private void ClearDices()
    {
        GameObject.Destroy(_playerDice.gameObject);
        GameObject.Destroy(_traderDice.gameObject);
        
    }
}
