using Unity.Entities;

[GenerateAuthoringComponent]
public struct CharacterComponent : IComponentData
{
    public int health;
    public int score;
    public float hitCD;
    public CharacterState characterState;
}

public enum CharacterState
{
    alive = 0,
    dead
}