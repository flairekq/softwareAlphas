using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
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

    public void Show()
    {
        // bloodImage.color = bloodImage.;
        damageCanvas.SetActive(true);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float targetAlpha = 1.0f;
        Color curColor = bloodImage.color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, fadeRate * Time.deltaTime);
            bloodImage.color = curColor;
            // yield return null;
        }
        yield return new WaitForSeconds(1f);
        damageCanvas.SetActive(false);
    }
}
