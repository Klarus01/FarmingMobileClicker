public abstract class State
{
    protected FarmerAI farmer;

    public State(FarmerAI farmer)
    {
        this.farmer = farmer;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}