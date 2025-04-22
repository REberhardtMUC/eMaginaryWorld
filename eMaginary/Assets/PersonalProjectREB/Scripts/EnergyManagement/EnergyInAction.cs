using TMPro;
using UnityEditor;
using UnityEngine;
using static EnergyData;

namespace eMaginary.Energy 
{
    public class EnergyInAction : MonoBehaviour
    {
        [SerializeField] private EnergyData eData;
        [SerializeField] private ShowEnergyData showEnergyData;
     
        GameObject character;
        string m_objectName, m_giveOrTake;
        int m_pointsToGiveOrTake;
        int m_spriteIndex;

        private void Start()
        {
            m_objectName = eData.ObjectName;
            m_giveOrTake = eData.CostOrGainEnergy.ToString();
            m_pointsToGiveOrTake = eData.PointsCostOrGain;

            if (m_giveOrTake == "cost")
            {
                m_pointsToGiveOrTake = m_pointsToGiveOrTake * (-1);
                m_spriteIndex = 1;
            }
            else
            {
                m_spriteIndex = 0;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            character = other.gameObject;
            if (character.tag == "Player")
            {
                showEnergyData.OpenEnergyPanel();

                Debug.Log("alter Energielevel: " + EnergyManagement.energyLevel);
                EnergyManagement.energyLevel = SetNewEnergyLevel();
                Debug.Log("neuer Energielevel: " + EnergyManagement.energyLevel);

                
                showEnergyData.SetEnergyInfo(m_objectName, m_giveOrTake, m_pointsToGiveOrTake, m_spriteIndex);            
            }
        }

        private void OnTriggerExit(Collider other)
        {
            showEnergyData.CloseEnergyPanel();  
        }

        public int SetNewEnergyLevel()
        {
            return EnergyManagement.energyLevel + m_pointsToGiveOrTake;
        }
    }

}
