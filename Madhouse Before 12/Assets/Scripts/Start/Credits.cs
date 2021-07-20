using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] Menu mainMenu;
    // Start is called before the first frame update
    public void GoToMainMenu()
    {
        MenuManager.Instance.OpenMenu(mainMenu);
    }
}
