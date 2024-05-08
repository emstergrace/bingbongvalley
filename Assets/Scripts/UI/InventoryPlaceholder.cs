using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlaceholder : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject BackpackPanel;
    public GameObject WardrobePanel;
    public GameObject IngredientsPanel;
    public GameObject PotionsPanel;
    public GameObject FishPanel;
    public GameObject MapPanel;

    public void OpenWardrobe() {
        PotionsPanel.SetActive(false);
        FishPanel.SetActive(false);
        IngredientsPanel.SetActive(false);
        WardrobePanel.SetActive(true);
        BackpackPanel.SetActive(false);
    }

    public void OpenIngredients() {
        PotionsPanel.SetActive(false);
        FishPanel.SetActive(false);
        IngredientsPanel.SetActive(true);
        WardrobePanel.SetActive(false);
        BackpackPanel.SetActive(false);
    }

    public void OpenPotions() {
        PotionsPanel.SetActive(true);
        FishPanel.SetActive(false);
        IngredientsPanel.SetActive(false);
        WardrobePanel.SetActive(false);
        BackpackPanel.SetActive(false);
    }

    public void OpenFishJournal() {
        FishPanel.SetActive(true);
        PotionsPanel.SetActive(false);
        IngredientsPanel.SetActive(false);
        WardrobePanel.SetActive(false);
        BackpackPanel.SetActive(false);
	}

    public void OpenMap() {
        FishPanel.SetActive(false);
        PotionsPanel.SetActive(false);
        IngredientsPanel.SetActive(false);
        WardrobePanel.SetActive(false);
        BackpackPanel.SetActive(false);
        MapPanel.SetActive(true);
    }

    public void CloseMap() {
        MapPanel.SetActive(false);
        ReturnToBackpack();
	}

    public void ReturnToBackpack() {
        PotionsPanel.SetActive(false);
        FishPanel.SetActive(false);
        IngredientsPanel.SetActive(false);
        WardrobePanel.SetActive(false);
        BackpackPanel.SetActive(true);
    }

    public void OpenInventory() {
        InventoryPanel.SetActive(true);
        ReturnToBackpack();
	}

    public void CloseInventory() {
        InventoryPanel.SetActive(false);
	}
}
