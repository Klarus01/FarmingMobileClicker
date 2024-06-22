using UnityEngine;

public class MoveToSoilState : State
{
    private Soil targetSoil;
    private State nextState;

    public MoveToSoilState(FarmerAI farmer, Soil soil, State nextState) : base(farmer)
    {
        this.targetSoil = soil;
        this.nextState = nextState;
    }

    public override void Enter()
    {
        farmer.SetTargetPosition(targetSoil.transform.position);
    }

    public override void Execute()
    {
        if (farmer.AtTargetPosition())
        {
            farmer.ChangeState(nextState);
        }
    }

    public override void Exit() { }
}