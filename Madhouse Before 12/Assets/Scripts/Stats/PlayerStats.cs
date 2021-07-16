using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class PlayerStats : CharacterStats, IOnEventCallback
{
    // public override void Die()
    // {
    //     base.Die();
    //     // Kill the player
    //     PlayerManagerOld.instance.KillPlayer();
    // }
    [SerializeField] private GameObject damageCanvas;
    [SerializeField] private Image bloodImage;
    public float fadeRate = 1f;
    private PhotonView PV;
    private bool isShow = false;

    public const byte UpdateIsShowEventCode = 1;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        if (isShow)
        {
            // Debug.Log("showing canvas for player at: " + gameObject.transform.position);
            damageCanvas.SetActive(true);
            StartCoroutine(FadeIn());
            isShow = false;
        }
    }

    public void Show(PhotonView pv)
    {
        // damageCanvas.SetActive(true);
        // StartCoroutine(FadeIn());
        object[] content = new object[] { true }; // Array contains the target position and the IDs of the selected units
        PhotonNetwork.RaiseEvent(UpdateIsShowEventCode, content, new RaiseEventOptions { TargetActors = new int[] { pv.CreatorActorNr } }, SendOptions.SendReliable);
        // isShow = true;
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == UpdateIsShowEventCode)
        {
            // Debug.Log("sent event");
            isShow = true;
        }
    }

    IEnumerator FadeIn()
    {
        float targetAlpha = 1.0f;
        Color curColor = bloodImage.color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, fadeRate * Time.deltaTime);
            bloodImage.color = curColor;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        damageCanvas.SetActive(false);
    }
}
