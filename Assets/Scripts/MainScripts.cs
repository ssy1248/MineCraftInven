using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScripts : MonoBehaviour
{
    [SerializeField] 
    private UI_Inventory uiInventory; //�κ��丮 ui
    [SerializeField] 
    private InventoryManager manager; //�κ��丮 ���� �Ŵ���
    [SerializeField] 
    private UI_CraftingSystem uiCraftingSystem; //����� ���� ui

    private void Start()
    {
        uiInventory.SetInventory(manager.GetInventory()); //�κ��丮 ����

        CraftingSystem craftingSystem = new CraftingSystem();

        uiCraftingSystem.SetCraftingSystem(craftingSystem); // �κ��丮 Ȱ��ȭ �� �κ��丮 ������
    }
}
