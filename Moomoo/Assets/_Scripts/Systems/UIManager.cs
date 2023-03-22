using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject interactionMenu;



    public void ShowInteractionMenu()
    {
        interactionMenu.SetActive(true);
        UpdateInteractionMenuData();
    }

    public void HideInteractionMenu()
    {
        interactionMenu.SetActive(false);
    }

    private void UpdateInteractionMenuData()
    {
        // Update interaction menu contents
    }
}
