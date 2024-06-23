using UnityEngine;

public class PlantState : State
{
    private Soil targetSoil;
    private SeedsData seed;

    public PlantState(FarmerAI farmer, Soil soil, SeedsData seed) : base(farmer)
    {
        targetSoil = soil;
        this.seed = seed;
    }

    public override void Enter() { }

    public override void Execute()
    {
        farmer.PlantCrops(targetSoil, seed);
        farmer.ChangeState(new IdleState(farmer));
    }

    public override void Exit() { }
}