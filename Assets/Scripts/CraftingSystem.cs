using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CraftingSystem : IITemHolder
{
    public const int GRID_SIZE = 3;

    public event EventHandler onGridChanged;

    private Dictionary<Item.ItemType, Item.ItemType[,]> recipeDictionary;

    private Item[,] itemArray;
    private Item outputItem;

    public CraftingSystem()
    {
        itemArray = new Item[GRID_SIZE, GRID_SIZE];

        recipeDictionary = new Dictionary<Item.ItemType, Item.ItemType[,]>();

        // Log
        Item.ItemType[,] recipe = new Item.ItemType[GRID_SIZE, GRID_SIZE];
        recipe[0, 2] = Item.ItemType.NONE; recipe[1, 2] = Item.ItemType.NONE; recipe[2, 2] = Item.ItemType.NONE;
        recipe[0, 1] = Item.ItemType.NONE; recipe[1, 1] = Item.ItemType.NONE; recipe[2, 1] = Item.ItemType.NONE;
        recipe[0, 0] = Item.ItemType.NONE; recipe[1, 0] = Item.ItemType.Wood; recipe[2, 0] = Item.ItemType.NONE;
        recipeDictionary[Item.ItemType.Log] = recipe;

        // Wooden Stick
        recipe = new Item.ItemType[GRID_SIZE, GRID_SIZE];
        recipe[0, 2] = Item.ItemType.NONE; recipe[1, 2] = Item.ItemType.NONE; recipe[2, 2] = Item.ItemType.NONE;
        recipe[0, 1] = Item.ItemType.NONE; recipe[1, 1] = Item.ItemType.Log; recipe[2, 1] = Item.ItemType.NONE;
        recipe[0, 0] = Item.ItemType.NONE; recipe[1, 0] = Item.ItemType.Log; recipe[2, 0] = Item.ItemType.NONE;
        recipeDictionary[Item.ItemType.WoodStick] = recipe;

        // Wooden Pickax
        recipe = new Item.ItemType[GRID_SIZE, GRID_SIZE];
        recipe[0, 2] = Item.ItemType.Log; recipe[1, 2] = Item.ItemType.Log; recipe[2, 2] = Item.ItemType.Log;
        recipe[0, 1] = Item.ItemType.NONE; recipe[1, 1] = Item.ItemType.WoodStick; recipe[2, 1] = Item.ItemType.NONE;
        recipe[0, 0] = Item.ItemType.NONE; recipe[1, 0] = Item.ItemType.WoodStick; recipe[2, 0] = Item.ItemType.NONE;
        recipeDictionary[Item.ItemType.WoodPickax] = recipe;

        // Stone Pickax
        recipe = new Item.ItemType[GRID_SIZE, GRID_SIZE];
        recipe[0, 2] = Item.ItemType.CobbleStone; recipe[1, 2] = Item.ItemType.CobbleStone; recipe[2, 2] = Item.ItemType.CobbleStone;
        recipe[0, 1] = Item.ItemType.NONE; recipe[1, 1] = Item.ItemType.WoodStick; recipe[2, 1] = Item.ItemType.NONE;
        recipe[0, 0] = Item.ItemType.NONE; recipe[1, 0] = Item.ItemType.WoodStick; recipe[2, 0] = Item.ItemType.NONE;
        recipeDictionary[Item.ItemType.StonePickax] = recipe;

        // Fishing Rod
        recipe = new Item.ItemType[GRID_SIZE, GRID_SIZE];
        recipe[0, 2] = Item.ItemType.NONE; recipe[1, 2] = Item.ItemType.NONE; recipe[2, 2] = Item.ItemType.WoodStick;
        recipe[0, 1] = Item.ItemType.NONE; recipe[1, 1] = Item.ItemType.WoodStick; recipe[2, 1] = Item.ItemType.Web;
        recipe[0, 0] = Item.ItemType.WoodStick; recipe[1, 0] = Item.ItemType.NONE; recipe[2, 0] = Item.ItemType.Web;
        recipeDictionary[Item.ItemType.FishingRod] = recipe;

        // Fishing Rod
        recipe = new Item.ItemType[GRID_SIZE, GRID_SIZE];
        recipe[0, 2] = Item.ItemType.NONE; recipe[1, 2] = Item.ItemType.NONE; recipe[2, 2] = Item.ItemType.NONE;
        recipe[0, 1] = Item.ItemType.NONE; recipe[1, 1] = Item.ItemType.FishingRod; recipe[2, 1] = Item.ItemType.NONE;
        recipe[0, 0] = Item.ItemType.NONE; recipe[1, 0] = Item.ItemType.NONE; recipe[2, 0] = Item.ItemType.Carrot;
        recipeDictionary[Item.ItemType.CarrotFishingRod] = recipe;
    }

    public bool IsEmpty(int x, int y)
    {
        return itemArray[x, y] == null;
    }

    public Item GetItem(int x, int y)
    {
        return itemArray[x, y];
    }

    public void SetItem(Item item, int x, int y)
    {
        if (item != null)
        {
            item.RemoveFromItemHolder();
            item.SetItemHolder(this);
        }
        itemArray[x, y] = item;
        CreateOutput();
        onGridChanged?.Invoke(this, EventArgs.Empty);
    }

    public void IncreaseItemAmount(int x, int y)
    {
        GetItem(x, y).amount++;
        onGridChanged?.Invoke(this, EventArgs.Empty);
    }

    public void DecreaseItemAmount(int x, int y)
    {
        if (GetItem(x, y) != null)
        {
            GetItem(x, y).amount--;
            if (GetItem(x, y).amount == 0)
            {
                RemoveItem(x, y);
            }
            onGridChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void RemoveItem(int x, int y)
    {
        SetItem(null, x, y);
    }

    public bool TryAddItem(Item item, int x, int y)
    {
        if (IsEmpty(x, y))
        {
            SetItem(item, x, y);
            return true;
        }
        else
        {
            if (item.itemType == GetItem(x, y).itemType)
            {
                IncreaseItemAmount(x, y);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        if (item == outputItem)
        {
            // Removed output item
            ConsumeRecipeItems();
            CreateOutput();
            onGridChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // Removed item from grid
            for (int x = 0; x < GRID_SIZE; x++)
            {
                for (int y = 0; y < GRID_SIZE; y++)
                {
                    if (GetItem(x, y) == item)
                    {
                        // Removed this one
                        RemoveItem(x, y);
                    }
                }
            }
        }
    }

    public void AddItem(Item item) { }

    public bool CanAddItem() { return false; }


    private Item.ItemType GetRecipeOutput()
    {
        foreach (Item.ItemType recipeItemType in recipeDictionary.Keys)
        {
            Item.ItemType[,] recipe = recipeDictionary[recipeItemType];

            bool completeRecipe = true;
            for (int x = 0; x < GRID_SIZE; x++)
            {
                for (int y = 0; y < GRID_SIZE; y++)
                {
                    if (recipe[x, y] != Item.ItemType.NONE)
                    {
                        // Recipe has Item in this position
                        if (IsEmpty(x, y) || GetItem(x, y).itemType != recipe[x, y])
                        {
                            // Empty position or different itemType
                            completeRecipe = false;
                        }
                    }
                }
            }

            if (completeRecipe)
            {
                return recipeItemType;
            }
        }
        return Item.ItemType.NONE;
    }

    private void CreateOutput()
    {
        Item.ItemType recipeOutput = GetRecipeOutput();
        if (recipeOutput == Item.ItemType.NONE)
        {
            outputItem = null;
        }
        else
        {
            outputItem = new Item { itemType = recipeOutput };
            outputItem.SetItemHolder(this);
        }
    }

    public Item GetOutputItem()
    {
        return outputItem;
    }

    public void ConsumeRecipeItems()
    {
        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                DecreaseItemAmount(x, y);
            }
        }
    }
}
