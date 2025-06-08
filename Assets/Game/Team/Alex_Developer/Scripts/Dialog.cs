using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using ModestTree;
using System.Collections;
using DG.Tweening;

public class Dialog : MonoBehaviour
{
    public List<DialogData> dialogSequence;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI speakerName;
    public Image speakerImage;
    public string playerName = "Говард";
    public float clickCooldown = 0.2f;
    public float fadeDuration = 0.5f; // Duration for fade in/out effects

    private Dictionary<string, int> characterTextIndices = new Dictionary<string, int>();
    private bool isDialogActive = true;
    private float lastClickTime;
    private Sequence currentSequence;

    private void OnEnable()
    {
        // Initialize with transparent elements
        dialogText.alpha = 0f;
        speakerName.alpha = 0f;
        speakerImage.color = new Color(1, 1, 1, 0);

        StartCoroutine(ShowText());
    }

    private void OnDisable()
    {
        StopCoroutine(ShowText());
        currentSequence?.Kill(); // Kill any running tweens
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

                // Create a sequence for smooth transitions
                currentSequence = DOTween.Sequence();

                // Fade out current elements
                currentSequence.Append(dialogText.DOFade(0, fadeDuration / 2));
                currentSequence.Join(speakerName.DOFade(0, fadeDuration / 2));
                currentSequence.Join(speakerImage.DOFade(0, fadeDuration / 2));

                // Update content while invisible
                currentSequence.AppendCallback(() => {
                    dialogText.text = dialog.texts[currentIndex].text;
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
                });

                // Fade in new elements
                currentSequence.Append(dialogText.DOFade(1, fadeDuration));
                currentSequence.Join(speakerName.DOFade(1, fadeDuration));
                if (speakerImage.enabled)
                {
                    currentSequence.Join(speakerImage.DOFade(1, fadeDuration));
                }

                // Wait for sequence to complete
                yield return currentSequence.WaitForCompletion();

                // Wait for mouse click with cooldown
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && Time.time - lastClickTime > clickCooldown);
                lastClickTime = Time.time;
                yield return null; // Wait one frame

                characterTextIndices[dialog.publicName]++;
            }

            // Exit if no more text to display
            if (!anyTextRemaining)
            {
                // Fade out all elements before exiting
                currentSequence = DOTween.Sequence();
                currentSequence.Append(dialogText.DOFade(0, fadeDuration));
                currentSequence.Join(speakerName.DOFade(0, fadeDuration));
                currentSequence.Join(speakerImage.DOFade(0, fadeDuration));
                yield return currentSequence.WaitForCompletion();

                isDialogActive = false;
                yield break;
            }
        }
    }
}
