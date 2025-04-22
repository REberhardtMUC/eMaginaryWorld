using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace eMaginary.Energy
{
    public class ShowEnergyData : MonoBehaviour
    {
        public GameObject pnl_energyInfo;

        [SerializeField] private Sprite[] bonsaiImages;

        public GameObject icon_tree;
        public TextMeshProUGUI txt_eObject;
        public TextMeshProUGUI txt_giveOrTake;
        public TextMeshProUGUI txt_ePoints;

        public void OpenEnergyPanel()
        {
            pnl_energyInfo.SetActive(true);
        }

        public void CloseEnergyPanel()
        {
            pnl_energyInfo.SetActive(false);
        }

        public void SetEnergyInfo(string objectName, string giveOrTake, int pointsToGiveOrTake, int spriteIndex)
        {
            txt_eObject.text = objectName;
            txt_giveOrTake.text = giveOrTake;
            txt_ePoints.text = pointsToGiveOrTake.ToString();
            icon_tree.GetComponent<UnityEngine.UI.Image> ().sprite = bonsaiImages[spriteIndex];
        }
    }
}


