using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public abstract class BaseEnemyStateMachine<TEnum> : StateManager<TEnum>, IDamageable
    where TEnum : Enum
{
    public static event Action<int> OnEnemyDefeated;

    [Header("Estadísticas Base Enemigo")]
    [SerializeField] private float _initialHealth = 10f;
    [SerializeField] public int scorePoints = 100;

    [Header("Estadísticas de Comportamiento")]
    [SerializeField] public float velocity = 3f;
    [SerializeField] public float attackRadius = 2f;
    [SerializeField] public float attackCooldown = 2.0f;
    [SerializeField] public float attackDamage = 10f;

    [Header("Comportamiento Errático (Wander)")]
    [Tooltip("Cada cuántos segundos el enemigo recalcula su ruta")]
    [SerializeField] public float wanderInterval = 2.0f;
    [Tooltip("Qué tan lejos del jugador (o de su ruta) puede desviarse al recalcular")]
    [SerializeField] public float wanderRadius = 5.0f;

    [Header("Feedback Base (Audio)")]
    [SerializeField] public AudioClip sfxSpawn;
    [SerializeField] public AudioClip sfxMoving;
    [SerializeField] public AudioClip sfxAttack;
    [SerializeField] public AudioClip sfxDamageTaken;
    [SerializeField] public AudioClip sfxPlayerDamage;

    [Header("Feedback Base (Visual)")]
    [SerializeField] public GameObject vfxSpawn;
    [SerializeField] public GameObject vfxPlayerDamage;

    public Transform target { get; protected set; }
    public float currentLife { get; protected set; }
    public AudioSource audioSource { get; protected set; }


    // --- Métodos de Unity (Virtuales) ---
    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if(target == null)
        {
            Debug.LogError("Player not found in the scene. Please ensure there is a GameObject with the 'Player' tag.");
        }
    }

    // Start virtual para que las clases hijas lo llamen
    protected virtual void Start()
    {
        currentLife = _initialHealth;
    }

    // --- Implementación de la Interfaz ---
    public virtual void TakeDamage(float quantity)
    {
        TEnum deathState = GetDeathStateEnum();

        // Corrección: Comparamos con la 'stateKey' del estado actual
        if (currentState.stateKey.Equals(deathState)) return;

        currentLife -= quantity;
        PlaySound(sfxDamageTaken);

        if (currentLife <= 0)
        {
            TransitionToState(deathState);
        }
    }

    protected abstract TEnum GetDeathStateEnum();

    public void ReportScore()
    {
        OnEnemyDefeated?.Invoke(scorePoints);
    }

    // --- Métodos Helper ---
    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void InstantiateVFX(GameObject vfx, Vector3 position)
    {
        if (vfx != null)
        {
            Instantiate(vfx, position, Quaternion.identity);
        }
    }
}