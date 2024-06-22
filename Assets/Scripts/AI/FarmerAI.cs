using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FarmerAI : MonoBehaviour
{
    private State currentState;
    public List<Soil> soils = new();
    private Rigidbody2D rb;
    private float moveSpeed = 10f;
    private Vector2 targetPosition;
    private float minDistance = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }

        MoveToTarget();
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    private void MoveToTarget()
    {
        if (targetPosition != Vector2.zero)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
    }

    public void SetTargetPosition(Vector2 position)
    {
        targetPosition = position;
    }

    public bool AtTargetPosition()
    {
        return Vector2.Distance(transform.position, targetPosition) < minDistance;
    }

    public void AddSoil(Soil soil)
    {
        soils.Add(soil);
    }

    public Soil GetNearestSoil(System.Predicate<Soil> condition)
    {
        Soil nearestSoil = null;
        float nearestDistance = float.MaxValue;

        foreach (var soil in soils.Where(soil => condition(soil)))
        {
            float distance = Vector2.Distance(transform.position, soil.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestSoil = soil;
            }
        }

        return nearestSoil;
    }

    public Soil GetRipeSoil()
    {
        return GetNearestSoil(soil => soil.IsPlantGrowUp);
    }

    public Soil GetEmptySoil()
    {
        return GetNearestSoil(soil => soil.IsSoilEmpty());
    }

    public void HarvestCrops(Soil soil)
    {
        if (soil != null && soil.IsPlantGrowUp)
        {
            soil.HarvestPlant();
            Debug.Log("harvested");
        }
    }

    public bool CanPlantCrops()
    {
        return soils.Any(soil => soil.IsSoilEmpty());
    }

    public void PlantCrops(Soil soil)
    {
        if (soil != null && soil.IsSoilEmpty())
        {
            var keys = Player.Instance.Inventory.CountByItem.Keys.ToList();

            for (int i = keys.Count - 1; i >= 0; i--)
            {
                var item = keys[i];
                if (item is SeedsData)
                {
                    soil.PlantSeed(item as SeedsData);
                    Debug.Log("planted");
                    break;
                }
            }
        }
    }
}
