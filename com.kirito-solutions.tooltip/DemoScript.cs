using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kirito_Solutions.tooltip;
public class DemoScript : MonoBehaviour
{
    private void OnMouseEnter()
    {
        TooltipSystem.GetInstance.Show("This IS DEMO CONTENT", "DEMO HEADER");
    }
    private void OnMouseExit()
    {
        TooltipSystem.GetInstance.Hide();
    }
}
