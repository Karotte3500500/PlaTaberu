using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine.UI;
using UnityEngine;

public class ItemSlot_n : MonoBehaviour
{
    private Plataberu myChar = CharacterData._Plataberu;

    [SerializeField]
    private GameObject[] itemSlot;
    [SerializeField]
    private GameObject itemPanel;
    [SerializeField]
    private Sprite[] itemImg;
    [SerializeField]
    private Sprite voidImg;

    public int ItemIndex = 0;

    private void Start()
    {
        itemPanel.SetActive(false);
        myChar.ItemSlot[0] = new Glasses();
    }

    private void Update()
    {
        /*アイテムスロット*/
        int index = 0;
        foreach (var slot in itemSlot)
        {
            if(myChar.ItemSlot.Length > index)
            {
                slot.GetComponent<Button>().interactable = true;
                if (myChar.ItemSlot[index] == null)
                {
                    slot.transform.Find("Image").gameObject.GetComponent<Image>().sprite 
                        = itemImg[0];
                }
                else
                {
                    slot.transform.Find("Image").gameObject.GetComponent<Image>().sprite 
                        = itemImg[myChar.ItemSlot[index].ID];
                }
            }
            else
            {
                slot.GetComponent<Button>().interactable = false;
                slot.transform.Find("Image").gameObject.GetComponent<Image>().sprite
                    = voidImg;
            }
            index++;
        }
    }
    
    public void SetItem(int index)
    {
        ItemIndex = index;
        itemPanel.SetActive(true);
    }
}
