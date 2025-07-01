using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class BigFishVoiceControl : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public BigFishController bigFish; // Assign via Inspector

    void Start()
    {
        keywords.Add("up", () => bigFish.MoveUp());
        keywords.Add("down", () => bigFish.MoveDown());
        keywords.Add("bite", () => bigFish.Bite());

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnCommandRecognized;
        keywordRecognizer.Start();

        Debug.Log("Voice recognition started.");
    }

    void OnCommandRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Voice Command: " + args.text);
        keywords[args.text].Invoke();
    }

    private void OnDestroy()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}
