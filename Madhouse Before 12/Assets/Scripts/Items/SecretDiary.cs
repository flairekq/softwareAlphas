using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SecretDiary : MonoBehaviour
{
    public static SecretDiary instance;
    [SerializeField] private string[] sentences;
    [SerializeField] private List<string> availableWords;
    [SerializeField] private Text displayText;

    private List<List<Word>> convertedSentences = new List<List<Word>>();

    private PhotonView PV;
    void Awake()
    {
        instance = this;
        instance.PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ConvertSetences();
        RefreshText();
    }

    void ConvertSetences()
    {
        foreach (string s in sentences)
        {
            List<Word> words = new List<Word>();
            string[] plainWords = s.Split(' ');
            foreach (string w in plainWords)
            {
                words.Add(new Word() { word = w, encryptedWord = EncryptWord(w) });
            }
            convertedSentences.Add(words);
        }
    }

    string EncryptWord(string word)
    {
        string encryptedWord = "";
        for (int i = 0; i < word.Length; i++)
        {
            encryptedWord += " ";
        }
        return encryptedWord;
    }

    void RefreshText()
    {
        string toDisplay = "";
        foreach (List<Word> s in SecretDiary.instance.convertedSentences)
        {
            foreach (Word w in s)
            {
                if (SecretDiary.instance.availableWords.FindIndex(x => x.Equals(w.word)) != -1)
                {
                    toDisplay += (w.word + " ");
                }
                else
                {
                    toDisplay += (w.encryptedWord + " ");
                }
            }
            toDisplay += ".\n\n";
        }
        displayText.text = toDisplay;
    }

    public void AddWord(string word)
    {
        SecretDiary.instance.PV.RPC("RPC_HandleWord", RpcTarget.All, word);
    }

    [PunRPC]
    private void RPC_HandleWord(string word)
    {
        SecretDiary.instance.availableWords.Add(word);
        RefreshText();
    }
}
