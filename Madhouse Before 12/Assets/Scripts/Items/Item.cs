using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] new public string name = "";
    [SerializeField] public Sprite icon = null;
    [SerializeField] public string content = "";
    [SerializeField] private Sprite onExpandIcon;
    private Text noteText = null;
    private Text clueText = null;
    private Text displayNameText = null;
    private Image focusPanelImage = null;

    public void Examine(Text nt, Text ct, Text dnt, Image fpi, string type)
    {
        noteText = nt;
        clueText = ct;
        displayNameText = dnt;
        focusPanelImage = fpi;

        if (content != "")
        {
            // noteText.text = content;
            // noteText.enabled = true;
            if (type.Equals("DiaryClue"))
            {
                noteText.enabled = false;
                clueText.text = content;
                clueText.enabled = true;
            } else if (type.Equals("Note")) {
                noteText.text = content;
                noteText.enabled = true;
                clueText.enabled = false;
            }
        }
        else
        {
            noteText.enabled = false;
            clueText.enabled = false;
        }

        focusPanelImage.sprite = onExpandIcon;
        focusPanelImage.enabled = true;
        displayNameText.text = name;
        displayNameText.enabled = true;
    }

    public void OffExamine(Text nt, Text ct, Text dnt, Image fpi)
    {
        noteText = nt;
        clueText = ct;
        displayNameText = dnt;
        focusPanelImage = fpi;

        noteText.enabled = false;
        clueText.enabled = false;
        focusPanelImage.enabled = false;
        displayNameText.enabled = false;
    }

    public Sprite GetExpandIcon()
    {
        return onExpandIcon;
    }
}
