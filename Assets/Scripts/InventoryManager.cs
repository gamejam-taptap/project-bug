using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : BaseManager<InventoryManager>
{
    private readonly List<ItemData> ownedItems = new();
    public ItemData SelectedItem { get; private set; }
    public UIInventoryPanel uiPanel;

    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }

    public void AddItem(ItemData item)
    {
        if (!ownedItems.Contains(item))
        {
            ownedItems.Add(item);
            uiPanel.AddItemToUI(item);
        }
    }

    public void SelectItem(ItemData item)
    {
        Debug.Log(item.name + "selected");
        SelectedItem = item;
        uiPanel.HighlightItem(item);
    }

    public void UseItemOnTarget(UsableItemTarget target)
    {
        if (target.CanUseItem(SelectedItem))
        {
            target.OnUseItem(SelectedItem);
            ownedItems.Remove(SelectedItem);
            uiPanel.RemoveItemFromUI(SelectedItem);
            SelectedItem = null;
        }
    }

    public void UseItemOnDoors(DoorInteractionController door)
    {
        if(SelectedItem != null)
        {
            door.OnUseItem(SelectedItem);
        }
    }

    public void UseItemOnModel(ModelController model)
    {
        if(SelectedItem != null)
        {
            model.UseItemOnModel(SelectedItem);
        }
    }

    public void UseItemOnBar(BarController bar)
    {
        if (SelectedItem != null)
        {
            bar.OnUseItem(SelectedItem);
        }
    }

    public void RemoveItem()
    {
        if (SelectedItem != null)
        {
            ownedItems.Remove(SelectedItem);
            uiPanel.RemoveItemFromUI(SelectedItem);
            SelectedItem = null;
        }
    }

    public bool CheckIfHaveItem(ItemData Item)
    {
        return ownedItems.Contains(Item);
    }

    public void RemoveItemFromData(ItemData Item)
    {
        if (ownedItems.Contains(Item))
        {
            ownedItems.Remove(Item);
            uiPanel.RemoveItemFromUI(Item);
        }
    }
}

