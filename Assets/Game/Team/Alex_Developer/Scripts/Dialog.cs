using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    private Dictionary<DialogData, int> dialogTextIndices = new Dictionary<DialogData, int>();

    public List<DialogData> dialogSequence;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public Image leftImage;
    public Image rightImage;

    private int currentDialogIndex = 0;
    private int currentTextIndex = 0;

    void Start()
    {
        StartDialog();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextText();
        }
    }

    void StartDialog()
    {
        if (dialogSequence.Count == 0) return;
        currentDialogIndex = 0;
        currentTextIndex = 0;
        LoadCurrentDialog();
    }

    void LoadCurrentDialog()
    {
        if (currentDialogIndex >= dialogSequence.Count)
        {
            Debug.Log("Dialog finished!");
            return;
        }

        DialogData currentDialog = dialogSequence[currentDialogIndex];
        bool isLeft = currentDialog.publicName != "Говард"; // Example condition

        leftText.text = "";
        rightText.text = "";
        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);

        if (currentDialog.images.Count > 0)
        {
            if (isLeft)
            {
                leftImage.sprite = currentDialog.images[0];
                leftImage.gameObject.SetActive(true);
            }
            else
            {
                rightImage.sprite = currentDialog.images[0];
                rightImage.gameObject.SetActive(true);
            }
        }

        ShowNextText();
    }

    private void ShowNextText()
    {
        if (currentDialogIndex >= dialogSequence.Count) return;

        DialogData currentDialog = dialogSequence[currentDialogIndex];

        if (!dialogTextIndices.ContainsKey(currentDialog))
        {
            dialogTextIndices[currentDialog] = 0;
        }

        int textIndex = dialogTextIndices[currentDialog];

        if (textIndex >= currentDialog.texts.Count)
        {
            dialogTextIndices[currentDialog] = 0;
            currentDialogIndex++;
            if (currentDialogIndex < dialogSequence.Count)
            {
                LoadCurrentDialog();
            }
            else
            {
                Debug.Log("Dialog finished!");
            }
            return;
        }

        string text = currentDialog.texts[textIndex];

        if (currentDialog.publicName != "Говард")
        {
            leftText.text = text;
            rightText.text = "";
        }
        else
        {
            rightText.text = text;
            leftText.text = "";
        }

        dialogTextIndices[currentDialog] = textIndex + 1;
    }



}
