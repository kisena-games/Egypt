using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using ModestTree;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public List<DialogData> dialogSequence;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI speakerName;
    public Image speakerImage;
    public string playerName = "Говард";
    public float clickCooldown = 0.2f; // Prevents rapid click issues

    private Dictionary<string, int> characterTextIndices = new Dictionary<string, int>();
    private bool isDialogActive = true;
    private float lastClickTime;

    private void OnEnable()
    {
        StartCoroutine(ShowText());
    }
    private void OnDisable()
    {
        StopCoroutine(ShowText());
    }
    private IEnumerator ShowText()
    {
        // Initialize character indices
        foreach (var dialog in dialogSequence)
        {
            if (dialog == null || dialog.texts == null || dialog.texts.Count == 0)
            {
                Debug.LogError($"Invalid dialog data (null or empty texts). Skipping...");
                continue;
            }

            if (!characterTextIndices.ContainsKey(dialog.publicName))
            {
                characterTextIndices[dialog.publicName] = 0;
            }
        }

        while (isDialogActive)
        {
            bool anyTextRemaining = false;

            foreach (var dialog in dialogSequence)
            {
                if (dialog == null || dialog.texts == null ||
                    characterTextIndices[dialog.publicName] >= dialog.texts.Count)
                {
                    continue;
                }

                anyTextRemaining = true;
                int currentIndex = characterTextIndices[dialog.publicName];

                // Set dialog content
                dialogText.text = dialog.texts[currentIndex];
                speakerName.text = dialog.publicName;

                if (dialog.images != null && currentIndex < dialog.images.Count && dialog.images[currentIndex] != null)
                {
                    speakerImage.sprite = dialog.images[currentIndex];
                    speakerImage.enabled = true;
                }
                else
                {
                    speakerImage.enabled = false;
                }

                // Wait for mouse click with cooldown
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && Time.time - lastClickTime > clickCooldown);
                lastClickTime = Time.time;
                yield return null; // Wait one frame

                characterTextIndices[dialog.publicName]++;
            }

            // Exit if no more text to display
            if (!anyTextRemaining)
            {
                isDialogActive = false;
                yield break;
            }
        }
    }
}
