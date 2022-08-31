using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [SerializeField] private int energy;

    [SerializeField] private TextMeshProUGUI energyText;

    public void AddItem(Collectable.Type item)
    {
        switch (item)
        {
            case Collectable.Type.Energy:
                energy++;
                break;
            default:
                break;
        }
        UpdateInventoryView();
    }

    public void TakeItem(Collectable.Type item, int count)
    {
        
        switch (item)
        {
            case Collectable.Type.Energy:
                energy -= count;
                break;
            default:
                break;
        }
        UpdateInventoryView();
    }

    public bool CanTakeItem(Collectable.Type item, int count)
    {
        int countInInventory = 0;
        switch (item)
        {
            case Collectable.Type.Energy:
                countInInventory = energy;
                break;
            default:
                break;
        }

        return countInInventory >= count;
    }

    private void UpdateInventoryView()
    {
        energyText.text = energy.ToString();
    }

}
