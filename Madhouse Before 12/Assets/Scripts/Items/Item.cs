using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] new private string name = "";
    [SerializeField] public Sprite icon = null;
    [SerializeField] public string content = "";
    [SerializeField] private Sprite onExpandIcon;
    private Text noteText = null;
    private Text displayNameText = null;
    private Image focusPanelImage = null;

    public void Examine(Text nt, Text dnt, Image fpi)
    {
        noteText = nt;
        displayNameText = dnt;
        focusPanelImage = fpi;

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

    public void OffExamine(Text nt, Text dnt, Image fpi) {
        noteText = nt;
        displayNameText = dnt;
        focusPanelImage = fpi;
        
        noteText.enabled = false;
        focusPanelImage.enabled = false;
        displayNameText.enabled = false;
    }

    public Sprite GetExpandIcon() {
        return onExpandIcon;
    }
}
