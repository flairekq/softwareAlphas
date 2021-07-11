using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EscMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    [SerializeField] private GameObject menu;
    private TogglePlayerCursor togglePlayerCursor;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        togglePlayerCursor = GetComponent<TogglePlayerCursor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                togglePlayerCursor.ChangeToPlayer();
                menu.SetActive(false);
            }
            else
            {
                togglePlayerCursor.ChangeToCursor();
                menu.SetActive(true);
            }
        }
    }
}
