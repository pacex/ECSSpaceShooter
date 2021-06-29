using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct ScreenEdgeBehaviourComponent : IComponentData
{
    //What to do after leaving screen
    public HandleScreenEdgeSystem.Behaviour Behaviour;

    //Set to false initially; prevents action being taken if entity was spawned outside of screen
    public bool wasOnScreen;
}
