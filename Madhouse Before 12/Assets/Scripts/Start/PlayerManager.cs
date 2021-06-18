using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        //  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player3"), new Vector3(0, 0.5f, 0), Quaternion.identity);
         PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player7"), new Vector3(0, 0f, 0), Quaternion.identity);

      // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player5"), new Vector3(0, 0f, 0), Quaternion.identity);
    }
}
