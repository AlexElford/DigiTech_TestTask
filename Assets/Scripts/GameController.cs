using UnityEngine;

public class GameController : MonoBehaviour
{
    static public GameController instance;

    [SerializeField] private MultimeterView view;
    [SerializeField] private MultimeterRegulator regulator;

    private MultimeterController controller;
    private Multimeter model;

    [Header("InputData")]
    [SerializeField] private float resistance = 1000; // Îì
    [SerializeField] private float power = 400; // A

    private void Awake()
    {
        instance = this;

        model = new Multimeter(resistance, power);
        controller = new MultimeterController(model, view, regulator);
    }
}
