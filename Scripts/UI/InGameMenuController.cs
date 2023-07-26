using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuController : Singleton<InGameMenuController>
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private Movement _movementScript;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Button _firstSelectedButton;

    [SerializeField] List<MenuPage> pages;

    public void Toggle()
    {
        if (_menuCanvas.activeInHierarchy)
        {
            HideAll();
        }
        else
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        _menuCanvas.SetActive(true);

        _movementScript.enabled = false;
        _playerAnimator.enabled = false;

        _firstSelectedButton.Select();

        ShowPage(typeof(InventoryPage));
    }

    public void ShowMenu()
    {
        ShowPage(typeof(MenuSettingsPage));
    }

    public void HideAll()
    {
        foreach (MenuPage item in pages)
        {
            item.PageObject.gameObject.SetActive(false);
        }

        _menuCanvas.SetActive(false);

        _movementScript.enabled = true;
        _playerAnimator.enabled = true;
    }

    public void ShowPage(Type page)
    {
        foreach (MenuPage item in pages)
        {
            if (page.Equals(item.GetType()))
            {
                item.PageObject.gameObject.SetActive(true);
            }
            else
            {
                item.PageObject.gameObject.SetActive(false);
            }
        }
    }

    #region From Buttons

    public void Click_Resume()
    {
        HideAll();
    }

    public void Click_Settings()
    {
        Debug.Log("Going to settings");
    }

    public void Click_Menu()
    {
        Debug.Log("Going to menu");
    }

    #endregion
}
