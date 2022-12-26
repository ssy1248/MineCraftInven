using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public enum ItemType
    {
        NONE,
        Wood,
        Log,
        WoodStick,
        CobbleStone,
        Carrot,
        Web,
        FishingRod,
        CarrotFishingRod,
        WoodPickax,
        StonePickax,
    }

    public ItemType itemType;
    public int amount = 1;
    private IITemHolder itemHolder;
    public void SetItemHolder(IITemHolder itemHolder)
    {
        this.itemHolder = itemHolder;
    }

    public IITemHolder GetItemHolder()
    {
        return itemHolder;
    }

    public void RemoveFromItemHolder()
    {
        if (itemHolder != null)
        {
            itemHolder.RemoveItem(this);
        }
    }
    public Sprite GetSprite()
    {
        return GetSprite(itemType);
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Wood: 
                return ItemAsset.Instance.s_Wood;
            case ItemType.Log: 
                return ItemAsset.Instance.s_Log;
            case ItemType.WoodStick: 
                return ItemAsset.Instance.s_WoodStick;
            case ItemType.CobbleStone:
                return ItemAsset.Instance.s_CobbleStone;
            case ItemType.Carrot:
                return ItemAsset.Instance.s_Carrot;
            case ItemType.Web:
                return ItemAsset.Instance.s_Web;
            case ItemType.FishingRod:
                return ItemAsset.Instance.s_FishingRod;
            case ItemType.CarrotFishingRod:
                return ItemAsset.Instance.s_CarrotFishingRod;
            case ItemType.WoodPickax:
                return ItemAsset.Instance.s_WoodPickax;
            case ItemType.StonePickax:
                return ItemAsset.Instance.s_StonePickax;

        }
    }

    public bool IsStackable()
    {
        return IsStackable(itemType);
    }


    public static bool IsStackable(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Wood:
            case ItemType.Web:
            case ItemType.CobbleStone:
            case ItemType.Carrot:
            case ItemType.Log:
            case ItemType.WoodStick:
                return true;
            case ItemType.WoodPickax:
            case ItemType.StonePickax:
            case ItemType.FishingRod:
            case ItemType.CarrotFishingRod:
                return false;
        }
    }
}
