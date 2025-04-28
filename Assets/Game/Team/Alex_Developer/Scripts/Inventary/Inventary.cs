
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Core.Parsing;

public class Inventary:MonoBehaviour 
{

    [SerializeField] private List<RectTransform> _slots;
    [SerializeField] private RectTransform _selector;
    [SerializeField] private float _lerpSpeed = 5f;

    public static int currentIndex { get; private set; }
    
    private void Update()
    {
        Vector2 targetPosition = _slots[currentIndex].anchoredPosition;
        float scrollInput = Input.mouseScrollDelta.y;

        if (_slots.Count == 0)
            return;

        currentIndex -= (int)scrollInput;//


        currentIndex = currentIndex >= _slots.Count ? 0 :
            (currentIndex < 0 ? _slots.Count - 1 : currentIndex);

        _selector.anchoredPosition = currentIndex > 0 && currentIndex < _slots.Count-1 ?
            Vector2.Lerp(_selector.anchoredPosition,targetPosition, _lerpSpeed * Time.deltaTime) :
            _slots[currentIndex].anchoredPosition;
    }

}
