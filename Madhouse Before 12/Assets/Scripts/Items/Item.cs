using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] new private string name = "";
    [SerializeField] public Sprite icon = null;
    [SerializeField] public string content = "";
    [SerializeField] private Sprite onExpandIcon;
    [SerializeField] private Text noteText = null;
    [SerializeField] private Text displayNameText = null;
    [SerializeField] private Image focusPanelImage = null;

    public void Examine()
    {
        if (content != "")
        {
            noteText.text = content;
            noteText.enabled = true;
        }
        else
        {
            noteText.enabled = false;
        }

        focusPanelImage.sprite = onExpandIcon;
        focusPanelImage.enabled = true;
        displayNameText.text = name;
        displayNameText.enabled = true;
    }

    public void OffExamine() {
        noteText.enabled = false;
        focusPanelImage.enabled = false;
        displayNameText.enabled = false;
    }
}
