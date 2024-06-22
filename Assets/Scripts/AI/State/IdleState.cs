public class IdleState : State
{
    public IdleState(FarmerAI farmer) : base(farmer) { }

    public override void Enter() { }

    public override void Execute()
    {
        farmer.ChangeState(new CheckFieldsState(farmer));
    }

    public override void Exit() { }
}