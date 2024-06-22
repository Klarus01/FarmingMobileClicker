public class PlantState : State
{
    private Soil targetSoil;

    public PlantState(FarmerAI farmer, Soil soil) : base(farmer)
    {
        targetSoil = soil;
    }

    public override void Enter() { }

    public override void Execute()
    {
        farmer.PlantCrops(targetSoil);
        farmer.ChangeState(new CheckFieldsState(farmer));
    }

    public override void Exit() { }
}