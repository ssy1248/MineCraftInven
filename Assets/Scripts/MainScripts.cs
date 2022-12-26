using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScripts : MonoBehaviour
{
    [SerializeField] 
    private UI_Inventory uiInventory; //인벤토리 ui
    [SerializeField] 
    private InventoryManager manager; //인벤토리 관리 매니저
    [SerializeField] 
    private UI_CraftingSystem uiCraftingSystem; //만드는 메인 ui

    private void Start()
    {
        uiInventory.SetInventory(manager.GetInventory()); //인벤토리 셋팅

        CraftingSystem craftingSystem = new CraftingSystem();

        uiCraftingSystem.SetCraftingSystem(craftingSystem); // 인벤토리 활성화 및 인벤토리 돌리기
    }
}
