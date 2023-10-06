using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    private TMP_Text _dice;
    public int diceNumber { get; private set; }

    private void Start()
    {
        _dice = GetComponentInChildren<TMP_Text>();
        _dice.text = (Random.Range(0, 21)).ToString();
        diceNumber = int.Parse(_dice.text);
    }
}
