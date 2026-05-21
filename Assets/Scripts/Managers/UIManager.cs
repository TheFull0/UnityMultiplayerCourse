using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private List<UIHandlerBase> uiHandlers;
    
    private UIHandlerBase currentMenu;
    
    private Dictionary<MenuType, UIHandlerBase> uiHandlersDictionary;
    
    private void Awake()
    {
        // Ensure only one instance of UIManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        InstantiateDictionary();
        
        HideAllMenus();
        SetCurrentMenu(MenuType.MainMenu);
    }

    private void HideAllMenus()
    {
        foreach (var handler in uiHandlers)
        {
            handler.HideMenu();
        }
    }

    public void SwapMenu(MenuType menuType) 
    {
        if (uiHandlersDictionary.TryGetValue(menuType, out var newMenu))
        {
            if (currentMenu == null) return;
            
            currentMenu.HideMenu();
            newMenu.ShowMenu();
            currentMenu = newMenu;
            
        }
        else
        {
            Debug.LogError($"No UI Handler found for MenuType: {menuType}. Please ensure it is added to the UIManager.");
        }
    }

    public void SetCurrentMenu(MenuType newMenu)
    {
        if (currentMenu != null)
        {
            currentMenu.HideMenu();
        }
        
        if (uiHandlersDictionary.TryGetValue(newMenu, out var menuHandler))
        {
            currentMenu = menuHandler;
            currentMenu.ShowMenu();
        }
        else
        {
            Debug.LogError($"No UI Handler found for MenuType: {newMenu}. Please ensure it is added to the UIManager.");
        }
    }

    private void InstantiateDictionary()
    {
        uiHandlersDictionary = new Dictionary<MenuType, UIHandlerBase>();
        foreach (var handler in uiHandlers)
        {
            if (!uiHandlersDictionary.ContainsKey(handler.menuType))
            {
                uiHandlersDictionary.Add(handler.menuType, handler);
            }
            else
            {
                Debug.LogWarning($"Duplicate MenuType {handler.menuType} found in UI Handlers. Please ensure each MenuType is unique.");
            }
        }
    }
}

public enum MenuType
{
    MainMenu,
    MakeSessionMenu,
    JoinSessionMenu,
    JoinLobbyMenu
}
