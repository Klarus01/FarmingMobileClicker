using UnityEngine;

public class IdleState : State
{
    public IdleState(FarmerAI farmer) : base(farmer) { }

    public override void Enter() { }

    public override void Execute()
    {
        var ripeSoil = farmer.GetRipeSoil();
        var seed = farmer.CanPlantCrops();
        if (ripeSoil)
        {
            farmer.ChangeState(new MoveToSoilState(farmer, ripeSoil, new HarvestState(farmer, ripeSoil)));
        }
        else if (seed)
        {
            Soil emptySoil = farmer.GetEmptySoil();
            if (emptySoil != null)
            {
                farmer.ChangeState(new MoveToSoilState(farmer, emptySoil, new PlantState(farmer, emptySoil, seed as SeedsData)));
            }
        }
    }

    public override void Exit() { }
}