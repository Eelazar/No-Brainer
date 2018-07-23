using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPageSwitch : MonoBehaviour {

    public MenuManager.MenuPage newPage;

	void Start ()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SwitchToMenuPage);
	}

    void SwitchToMenuPage()
    {
        GameObject.Find("MenuManager").GetComponent<MenuManager>().SwitchMenu(newPage);
    }
}
