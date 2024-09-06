using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel_n : MonoBehaviour
{
    private ItemSlot_n itemSlot;
    private Plataberu myChar = CharacterData._Plataberu;

    [SerializeField]
    private GameObject items;

    private void Start()
    {
        itemSlot = FindObjectOfType<ItemSlot_n>();
    }

    private void Update()
    {
        for (int i = 1; i <= 12; i++) 
        {
            bool hasItem = false;
            string itemNum = "";

            if (PlayerData._Items.Length > i)
            {
                hasItem = PlayerData._Items[i] > 0;
                itemNum = $"x{PlayerData._Items[i]}";
            }
            else
            {
                hasItem = false;
                itemNum = "-";
            }
            GameObject itemButton = items.transform.Find($"ID{i}").gameObject;
            itemButton.GetComponent<Button>().interactable = hasItem;
            itemButton.transform.Find("num").GetComponent<Text>().text = itemNum;
        }
    }

    public void SetItem(int id)
    {
        if (PlayerData._Items[id] > 0)
        {
            if (myChar.ItemSlot[itemSlot.ItemIndex] != null)
                PlayerData._Items[myChar.ItemSlot[itemSlot.ItemIndex].ID] += 1;

            PlayerData._Items[id] -= 1;
            Item item;
            switch (id)
            {
                case 1:
                    item = new PiggyBank();
                    break;
                case 2:
                    item = new Glasses();
                    break;
                case 3:
                    item = new Spray();
                    break;
                case 4:
                    item = new Unison();
                    break;
                default:
                    item = null;
                    break;
            }
            myChar.ItemSlot[itemSlot.ItemIndex] = item;
            this.gameObject.SetActive(false);
        }
    }
    public void RemoveItem()
    {
        if (myChar.ItemSlot[itemSlot.ItemIndex] != null)
        {
            PlayerData._Items[myChar.ItemSlot[itemSlot.ItemIndex].ID] += 1;
            myChar.ItemSlot[itemSlot.ItemIndex] = null;
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
