using TMPro;
using UnityEngine;

public class AmountDisplay : MonoBehaviour
{
    [SerializeField] private Canvas _container;
    [SerializeField] private TextMeshProUGUI _amountText;

    public void UpdateAmount(int amount)
    {
        if (amount > 1)
        {
            _amountText.text = $"{amount}";
            _container.gameObject.SetActive(true);
            _container.enabled = true;
        }
        else
        {
            _container.gameObject.SetActive(false);
        }
    }
}
