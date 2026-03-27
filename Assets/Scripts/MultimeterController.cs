public class MultimeterController
{
    private Multimeter model;

    public MultimeterController(Multimeter model, MultimeterView view, MultimeterRegulator regulator)
    {
        this.model = model;

        regulator.OnModeChanged += OnModeChanged;

        model.OnCalculated                  += view.SetDisplayValue;
        model.OnDCVoltageCalculated         += view.SetDCVoltageValue;
        model.OnDCElectricCurrentCalculated += view.SetDCElectricCurrentValue;
        model.OnACElectricCurrentCalculated += view.SetACElectricCurrentValue;
        model.OnResistanceCalculated        += view.SetResistanceValue;
    }

    public void OnModeChanged(int modeIndex) => model.Calculate(modeIndex);
}
