using UnityEngine;

public class FairyContext
{
    // Guardamos una referencia al "dueño" de los datos
    private readonly FairyStateMachine _machine;

    // --- Constructor ---
    public FairyContext(FairyStateMachine machine)
    {
        _machine = machine;
    }

    // --- Referencias de Componentes ---
    public Transform Transform => _machine.transform;
    public Transform Target => _machine.target;
    // No hay NavMeshAgent

    // --- Estadísticas ---
    public float Velocity => _machine.velocity;
    public float AttackRadius => _machine.attackRadius;
    public float AttackCooldown => _machine.attackCooldown;
    public float AttackDamage => _machine.attackDamage;

    // --- Variables de Wander ---
    public float WanderInterval => _machine.wanderInterval;
    public float WanderRadius => _machine.wanderRadius;

    // --- Clips de Audio (SFX) ---
    public AudioClip SfxSpawn => _machine.sfxSpawn;
    public AudioClip SfxMoving => _machine.sfxMoving;
    public AudioClip SfxAttack => _machine.sfxAttack;
    public AudioClip SfxDamageTaken => _machine.sfxDamageTaken;
    public AudioClip SfxPlayerDamage => _machine.sfxPlayerDamage;

    // --- Efectos Visuales (VFX) ---
    public GameObject VfxSpawn => _machine.vfxSpawn;
    public GameObject VfxPlayerDamage => _machine.vfxPlayerDamage;

    // --- Métodos Helper ---
    public void PlaySound(AudioClip clip) => _machine.PlaySound(clip);
    public void InstantiateVFX(GameObject vfx, Vector3 position) => _machine.InstantiateVFX(vfx, position);
    public void ReportScore() => _machine.ReportScore();
    public void TakeDamage(float q) => _machine.TakeDamage(q);
}