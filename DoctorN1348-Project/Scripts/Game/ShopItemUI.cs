using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Text itemNameText;
    public Text priceText;
    public Text ownedText;
    public Button buyButton;

    private ItemData item;
    private int price;

    public void Setup(ItemData newItem, int itemPrice)
    {
        item = newItem;
        price = itemPrice;

        itemNameText.text = item.itemName;
        priceText.text = price + " coins";

        UpdateOwnedText();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => ShopManager.Instance.BuyItem(item, price));
    }

    public void UpdateOwnedText()
    {
        if (ownedText != null)
        {
            ownedText.text = "x" + Inventory.Instance.GetQuantity(item);
        }
    }
}
