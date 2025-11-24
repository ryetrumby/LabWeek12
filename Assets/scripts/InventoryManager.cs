using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class InventoryManager : MonoBehaviour
{
    List<InventoryItem> inventory;

    private void Start()
    {
        inventory = new List<InventoryItem>()
        {
            new InventoryItem("Sword"),
            new InventoryItem("Shield"),
            new InventoryItem("Axe"),
            new InventoryItem("Cloth Rags"),
            new InventoryItem("Steel Helmet"),
            new InventoryItem("MIRV Launcher"),
            new InventoryItem("Staff of Banishment"),
            new InventoryItem("Energy Sword"),
            new InventoryItem("Alpha Boost"),
            new InventoryItem("Suzu")
        };

        Debug.Log("Searching for 'Suzu' using Linear Search:");
        Debug.Log("Output 'Suzu' expected. If not, failed.");
        Debug.Log(LinearSearchByName("Suzu"));

        Debug.Log("Before sorting by value:");
        PrintInventory();

        QuickSortByValue();
        Debug.Log("After QuickSortByValue:");
        PrintInventory();

        int idToFind = inventory[3].ID;
        Debug.Log($"Searching for item with ID {idToFind} using Binary Search:");
        InventoryItem found = BinarySearchByID(idToFind);
        if (found != null) Debug.Log($"Found item with ID {idToFind}: {found.Name}");
        else Debug.Log($"Item with ID {idToFind} not found.");
    }



    //search by name
    InventoryItem LinearSearchByName(string itemName)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Name == itemName) return inventory[i];
        }
        return null;
    }

    // search by id, must sort items by id first
    InventoryItem BinarySearchByID(int ID)
    {
        inventory.Sort((a, b) => a.ID.CompareTo(b.ID));

        int low = 0;
        int high = inventory.Count - 1;

        // binary search
        while (low <= high)
        {
            int mid = low + ((high - low) / 2);
            int midId = inventory[mid].ID;

            if (midId == ID) return inventory[mid];
            if (midId < ID) low = mid + 1;
            else high = mid - 1;
        }

        return null;
    }

    // base quicksort function
    void QuickSortByValue()
    {
        if (inventory == null || inventory.Count < 2) return;
        QuickSortByValue(0, inventory.Count - 1);
    }

    // quicksort with passthroughs
    void QuickSortByValue(int low, int high)
    {
        if (low >= high) return;

        int p = PartitionByValue(low, high);
        QuickSortByValue(low, p - 1);
        QuickSortByValue(p + 1, high);
    }

    // partition function
    int PartitionByValue(int low, int high)
    {
        var pivot = inventory[high].Value;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (inventory[j].Value.CompareTo(pivot) <= 0)
            {
                i++;
                Swap(i, j);
            }
        }

        Swap(i + 1, high);
        return i + 1;
    }

    // swap function
    void Swap(int a, int b)
    {
        if (a == b) return;
        var temp = inventory[a];
        inventory[a] = inventory[b];
        inventory[b] = temp;
    }

    // print inventory
    void PrintInventory()
    {
        foreach (var item in inventory)
        {
            Debug.Log($"{item.Name} | ID: {item.ID} | Value: {item.Value}");
        }
    }

}
