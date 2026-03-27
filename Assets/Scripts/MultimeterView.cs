using TMPro;
using UnityEngine;

public class MultimeterView : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private TMP_Text displayValue;

    [Header("UI")]
    [SerializeField] private TMP_Text dcVoltage;
    [SerializeField] private TMP_Text dcElectricCurrent;
    [SerializeField] private TMP_Text acElectricCurrent;
    [SerializeField] private TMP_Text resistance;

    public void SetDisplayValue(float value) => displayValue.text = value.ToString("F2");
    public void SetDCVoltageValue(float value) => dcVoltage.text = "Напряжение (постоянное), V = " + value.ToString("F2");
    public void SetDCElectricCurrentValue(float value) => dcElectricCurrent.text = "Сила тока (постоянная), А = " + value.ToString("F2");
    public void SetACElectricCurrentValue(float value) => acElectricCurrent.text = "Сила тока (переменная), А~ = " + value.ToString("F2");
    public void SetResistanceValue(float value) => resistance.text = "Сопротивление, Ω = " + value.ToString("F2");
}
