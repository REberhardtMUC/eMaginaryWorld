using System.Runtime.InteropServices.WindowsRuntime;
using eMaginary.Energy;
using TMPro;
using Unity.Loading;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Display_Energy: MonoBehaviour
{
    private int energyToDisplay;

    [SerializeField] MeshRenderer e_Display;
    [SerializeField] GameObject m_img_DisplayEnergy;
    [SerializeField] TextMeshProUGUI m_txt_result;

    private Material energy_ColorCode;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FetchDisplayLevel();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HideDisplay();
    }

    public int FetchDisplayLevel()
    {
        energyToDisplay = EnergyManagement.energyLevel;
        TintBall();
        DisplayResult(energyToDisplay.ToString());

        return energyToDisplay; 
    }

    private void TintBall()
    { 
        if (energyToDisplay <1)
        {
            Debug.Log("Keine Energie mehr.");
            //e_Display.material.SetColor("_BaseColor", Color.red);
            e_Display.material.SetColor("_BaseColor", new Color(1f, 0, 0, 0.7f));//rot
        }
        else if (energyToDisplay < 7)
        {
            Debug.Log("Wenig Energie. Level " + energyToDisplay);
            e_Display.material.SetColor("_BaseColor", new Color(1f, 1f, 0, 0.7f));//gelb
        }
        else
        {
            Debug.Log("Noch genug Energie. Level " + energyToDisplay);
            e_Display.material.SetColor("_BaseColor", new Color(0, 1f, 0, 0.7f));//grün
        } 
   
    }
    public void DisplayResult(string result)
    {
       m_txt_result.text = energyToDisplay.ToString();
       m_img_DisplayEnergy.SetActive(true);

    }
    public void HideDisplay()
    {
        m_img_DisplayEnergy.SetActive(false);
    }
}
