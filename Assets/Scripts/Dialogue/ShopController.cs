using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private List<Button> buttons = new List<Button>();
    [SerializeField] private GameObject itemPrefab;
    public static ShopController Instance { get; private set; }
    [SerializeField] private GameObject shopPanel;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        shopPanel.SetActive(false);
    }

    public void InstantiateItems(ItemData[] items)
    {
        shopPanel.SetActive(true);
        foreach(ItemData item in items)
        {
            Debug.Log(item.ItemName);
            Button button = Instantiate(itemPrefab, transform).GetComponent<Button>();
            button.GetComponentInChildren<TMP_Text>().text = item.ItemName;
            buttons.Add(button);
            button.onClick.AddListener(() =>
            {
                Bag.AddItem(item);
            });
        }
    }

    public void Exit()
    {
        foreach (Button button in buttons)
        {
            if(button != null)
                Destroy(button.gameObject);
        }
        buttons.Clear(); // IMPORTANT - Garbage collection
        shopPanel.SetActive(false);
    }
}