using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [Header("References")]
    [Tooltip("The object containing the main menu buttons")]
    public GameObject mainMenuPage;
    [Tooltip("The object containing the level menu buttons")]
    public GameObject levelMenuPage;
    [Space]
    [Tooltip("The button that switches back to the main menu")]
    public Button backButton;


    //Private array for easy deactivating
    private List<GameObject> menuPages;    


    //Current Menu from list
    public enum MenuPage { Main, Levels }
    [HideInInspector]
    public MenuPage currentMenu;

    void Start ()
    {
        //Add pages to private array
        menuPages = new List<GameObject>();
        menuPages.Add(mainMenuPage);
        menuPages.Add(levelMenuPage);

        //Turn on the main menu
        SwitchMenu(MenuPage.Main);

        //Load the background scene
        SceneManager.LoadSceneAsync(PlayerPrefs.GetString("ScenePreview", "TutorialPreview"), LoadSceneMode.Additive);

        //Arm the back button
        backButton.onClick.AddListener(BackToMenu);
    }
	

    //Switches to a different menu
    public void SwitchMenu(MenuPage newPage)
    {
        switch (newPage)
        {
            case MenuPage.Main:

                DeactivateAll();
                mainMenuPage.SetActive(true);
                currentMenu = MenuPage.Main;
                break;

            case MenuPage.Levels:

                DeactivateAll();
                levelMenuPage.SetActive(true);
                backButton.gameObject.SetActive(true);
                currentMenu = MenuPage.Levels;
                break;

            default:
                break;
        }
    }

    //Deactivates all buttons, new menu pages have to be manually added
    void DeactivateAll()
    {
        foreach(GameObject go in menuPages)
        {
            go.SetActive(false);
        }

        backButton.gameObject.SetActive(false);
    }

    void BackToMenu()
    {
        SwitchMenu(MenuPage.Main);
    }
}
