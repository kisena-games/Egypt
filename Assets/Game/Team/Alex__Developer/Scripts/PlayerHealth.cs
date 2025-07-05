
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public static int healthCount = 3;
    public int HealthCount;
    [SerializeField] private int _sceneIndex;
    [SerializeField] private Image _imageDark;
    [SerializeField] private Image _blood;
    [SerializeField] private Image _bloodBack;
    private int _preCount;
    private void Start()
    {
        healthCount = 3;
        _preCount= healthCount;
        _imageDark.DOColor(new Color(0, 0, 0,0), 1f);
    }
    private void Update()
    {
        if (_preCount> healthCount)
        {
            _blood.DOColor(new Color(1, 1, 1, 0.5f), 0.5f)
      .OnComplete(() => {
          _blood.DOColor(new Color(1, 1, 1, 0), 1f);
      });
            _bloodBack.DOColor(new Color(1, 0, 0, 0.5f), 0.5f)
      .OnComplete(() => {
          _bloodBack.DOColor(new Color(1, 0, 0, 0), 1f);
      });
            _preCount = healthCount;
           
           
        }
        
            

        
        HealthCount=healthCount;
        if (healthCount <= 0)
        {
            StopAllCoroutines();
            _imageDark.DOColor(new Color(0, 0, 0, 1), 1f)
                .OnComplete(()=>SceneManager.LoadScene(_sceneIndex));
            
        }
    }
}
