using System;
using UnityEngine;

public enum MultimeterModeType
{
    DCVoltage,
    ACVoltage,
    DCElectricCurrent,
    ACElectricCurrent,
    Resistance,
    none
}

public struct MultimeterModeMultiplier
{
    public MultimeterModeType ModeType;
    public float MaxValue;

    public MultimeterModeMultiplier(MultimeterModeType modeType, float maxValue)
    {
        ModeType = modeType;
        MaxValue = maxValue;
    }
}

public class Multimeter
{
    private float resistance;
    private float power;

    public event Action<float> OnCalculated;
    public event Action<float> OnDCVoltageCalculated;
    public event Action<float> OnDCElectricCurrentCalculated;
    public event Action<float> OnACElectricCurrentCalculated;
    public event Action<float> OnResistanceCalculated;

    private MultimeterModeMultiplier[] modeMultipliers = new MultimeterModeMultiplier[]
    {
        new MultimeterModeMultiplier(MultimeterModeType.none, 0f),

        new MultimeterModeMultiplier(MultimeterModeType.DCVoltage, 0.2f),
        new MultimeterModeMultiplier(MultimeterModeType.DCVoltage, 2f),
        new MultimeterModeMultiplier(MultimeterModeType.DCVoltage, 20f),
        new MultimeterModeMultiplier(MultimeterModeType.DCVoltage, 200f),
        new MultimeterModeMultiplier(MultimeterModeType.DCVoltage, 600f),

        new MultimeterModeMultiplier(MultimeterModeType.ACVoltage, 600f),
        new MultimeterModeMultiplier(MultimeterModeType.ACVoltage, 200f),
        new MultimeterModeMultiplier(MultimeterModeType.ACVoltage, 20f),
        new MultimeterModeMultiplier(MultimeterModeType.ACVoltage, 2f),

        new MultimeterModeMultiplier(MultimeterModeType.DCElectricCurrent, 0.002f),
        new MultimeterModeMultiplier(MultimeterModeType.DCElectricCurrent, 0.02f),

        new MultimeterModeMultiplier(MultimeterModeType.ACElectricCurrent, 0.02f),

        new MultimeterModeMultiplier(MultimeterModeType.none, 0f),
        new MultimeterModeMultiplier(MultimeterModeType.none, 0f),
        new MultimeterModeMultiplier(MultimeterModeType.none, 0f),
        new MultimeterModeMultiplier(MultimeterModeType.none, 0f),
        new MultimeterModeMultiplier(MultimeterModeType.none, 0f),

        new MultimeterModeMultiplier(MultimeterModeType.Resistance, 200f),
        new MultimeterModeMultiplier(MultimeterModeType.Resistance, 2000f),
        new MultimeterModeMultiplier(MultimeterModeType.Resistance, 20000f),
        new MultimeterModeMultiplier(MultimeterModeType.Resistance, 200000f),
        new MultimeterModeMultiplier(MultimeterModeType.Resistance, 2000000f),
        new MultimeterModeMultiplier(MultimeterModeType.Resistance, 20000000f)
    };

    public Multimeter(float resistance, float power)
    {
        this.resistance = resistance;
        this.power = power;
    }

    public void Calculate(int modeIndex)
    {
        var mode = modeMultipliers[modeIndex];
        float value = 0f;
        float normalizedValue = 0f;

        switch (mode.ModeType)
        {
            case MultimeterModeType.DCVoltage:
                value = Mathf.Sqrt(power * resistance);
                normalizedValue = Mathf.Clamp(value, 0f, mode.MaxValue);

                OnDCVoltageCalculated?.Invoke(value);

                break;

            case MultimeterModeType.ACVoltage:
                normalizedValue = 0.01f;

                break;

            case MultimeterModeType.DCElectricCurrent:
                value = Mathf.Sqrt(power / resistance);
                normalizedValue = Mathf.Clamp(value, 0f, mode.MaxValue);

                OnDCElectricCurrentCalculated?.Invoke(value);

                break;

            case MultimeterModeType.ACElectricCurrent:
                value = Mathf.Sqrt(power / resistance);
                normalizedValue = Mathf.Clamp(value, 0f, mode.MaxValue);

                OnACElectricCurrentCalculated?.Invoke(value);

                break;

            case MultimeterModeType.Resistance:
                value = resistance;
                normalizedValue = Mathf.Clamp(value, 0f, mode.MaxValue);

                OnResistanceCalculated?.Invoke(value);

                break;

            default:
                normalizedValue = 0;

                break;
        }

        OnCalculated?.Invoke(normalizedValue);
    }
}
