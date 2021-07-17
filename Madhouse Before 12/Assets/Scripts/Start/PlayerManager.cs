using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    private int avatarNumber;
    PhotonView PV;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
    [SerializeField] float yPos;

    public CharacterManager characterManager;
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
        avatarNumber = characterManager.chooseAvatar();
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", avatarNumber.ToString()), new Vector3(xPos, yPos, zPos), Quaternion.identity);

       // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player13"), new Vector3(xPos, yPos, zPos), Quaternion.identity);
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player12"), new Vector3(0, 0f, 0), Quaternion.identity);
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player7"), new Vector3(0, 0f, 0), Quaternion.identity);

        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player5"), new Vector3(0, 0f, 0), Quaternion.identity);
    }
}
