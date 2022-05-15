using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VitesseSlider : MonoBehaviour
{
    [SerializeField]
    private Slider SliderAcceleration;

    [SerializeField]
    private TMP_Text NumberText;

    void Start()
    {
        this.OnValueChanged();
    }

    public void OnValueChanged()
    {
        if (this.NumberText != null)
        {
            this.NumberText.text = GetValue().ToString();
        }
    }

    public float GetValue()
    {
        float value = this.SliderAcceleration.value;
        return 0.5f + (value / 10);
    }
}
