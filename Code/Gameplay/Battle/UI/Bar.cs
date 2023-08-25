using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image bar;
    
    
    private float _minValue;
    private float _maxValue;
   

    public void SetMaxValue(float newMax)
    {
        _maxValue = newMax;
        bar.fillAmount = _minValue / _maxValue;
    }

    public void SetMinValue(float newMin)
    {
        _minValue = newMin;
        bar.fillAmount = _minValue / _maxValue;
    }
}
