using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager1 : MonoBehaviour
{

    PhotonView PV;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
    [SerializeField] float yPos;

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
        float xPos = Random.Range(minX, maxX);
        float zPos = Random.Range(minZ, maxZ);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "1"), new Vector3(xPos, yPos, zPos), Quaternion.identity);

       // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player13"), new Vector3(xPos, yPos, zPos), Quaternion.identity);
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player12"), new Vector3(0, 0f, 0), Quaternion.identity);
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player7"), new Vector3(0, 0f, 0), Quaternion.identity);

        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player5"), new Vector3(0, 0f, 0), Quaternion.identity);
    }
}
