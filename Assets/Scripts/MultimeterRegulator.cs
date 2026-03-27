using System;
using UnityEngine;

public class MultimeterRegulator : MonoBehaviour
{
    [SerializeField] private Material regulatorNormalMaterial;
    [SerializeField] private Material regulatorHoverMaterial;
    [SerializeField] private float rotationStep = 15f;

    public Action<int> OnModeChanged;

    private Transform regulator => GetComponent<Transform>();
    private Renderer regulatorRenderer => GetComponent<Renderer>();
    private bool isHovered;
    private int currentMode;

    private void Update()
    {
        HandleHover();
        HandleScroll();
    }

    private void HandleHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 10))
            isHovered = hit.transform == regulator;
        else
            isHovered = false;

        regulatorRenderer.material = isHovered ? regulatorHoverMaterial : regulatorNormalMaterial;
    }

    private void HandleScroll()
    {
        if (!isHovered) return;

        float scroll = Input.mouseScrollDelta.y;

        if (Mathf.Abs(scroll) > 0.01f)
        {
            int direction = scroll > 0 ? 1 : -1;

            RotateRegulator(direction);
        }
    }

    private void RotateRegulator(int direction)
    {
        currentMode += direction;
        currentMode = NormalizeValue(currentMode, direction == 1);

        float angle = currentMode * rotationStep;
        regulator.localRotation = Quaternion.Euler(0, 0, angle);

        OnModeChanged?.Invoke(currentMode);
    }

    private int NormalizeValue(int value, bool increment)
    {
        value = (value + 24) % 24;

        if (value >= 13 && value <= 17)
            value = increment ? 18 : 12;

        return value;
    }
}
