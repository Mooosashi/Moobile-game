using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject interactionMenu;

    public void ShowInteractionMenu()
    {
        interactionMenu.SetActive(true);
    }
}
