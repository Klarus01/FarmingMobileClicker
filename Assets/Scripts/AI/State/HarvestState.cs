public class HarvestState : State
{
    private Soil targetSoil;

    public HarvestState(FarmerAI farmer, Soil soil) : base(farmer)
    {
        targetSoil = soil;
    }

    public override void Enter() { }

    public override void Execute()
    {
        farmer.HarvestCrops(targetSoil);
        farmer.ChangeState(new IdleState(farmer));
    }

    public override void Exit() { }
}