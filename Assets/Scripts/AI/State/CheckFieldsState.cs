public class CheckFieldsState : State
{
    public CheckFieldsState(FarmerAI farmer) : base(farmer) { }

    public override void Enter() { }

    public override void Execute()
    {
        Soil ripeSoil = farmer.GetRipeSoil();
        if (ripeSoil != null)
        {
            farmer.ChangeState(new MoveToSoilState(farmer, ripeSoil, new HarvestState(farmer, ripeSoil)));
        }
        else if (farmer.CanPlantCrops())
        {
            Soil emptySoil = farmer.GetEmptySoil();
            if (emptySoil != null)
            {
                farmer.ChangeState(new MoveToSoilState(farmer, emptySoil, new PlantState(farmer, emptySoil)));
            }
        }
        else
        {
            farmer.ChangeState(new IdleState(farmer));
        }
    }

    public override void Exit() { }
}