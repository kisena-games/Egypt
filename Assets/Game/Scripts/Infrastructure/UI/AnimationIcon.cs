using UnityEngine;
using DG.Tweening;

public class AnimatorIcon : MonoBehaviour
{
    public float rotationDuration = 2f;

    public float jumpHeight = 20f;
    public float jumpDuration = 0.5f;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        AnimateRotation();
        AnimateJump();
    }

    private void AnimateRotation()
    {
        rectTransform.DORotate(new Vector3(0, 0, 360), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    private void AnimateJump()
    {
        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + jumpHeight, jumpDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}