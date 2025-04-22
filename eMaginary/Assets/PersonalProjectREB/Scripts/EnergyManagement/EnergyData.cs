using UnityEngine;

[CreateAssetMenu(fileName ="energyData", menuName = "energy Data", order = 49)]
public class EnergyData : ScriptableObject
{
    public enum COST_OR_GAIN {cost, gain};
    [SerializeField] private string objectName;
    [SerializeField] private int pointsCostOrGain;

    [SerializeField] private COST_OR_GAIN costOrGainEnergy;

    public string ObjectName { get { return objectName; } }
    public COST_OR_GAIN CostOrGainEnergy { get { return costOrGainEnergy; } }
    public int PointsCostOrGain { get {return pointsCostOrGain; } }
}
