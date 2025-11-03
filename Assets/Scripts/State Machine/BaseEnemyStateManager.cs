using UnityEngine;
using System;


[RequireComponent(typeof(AudioSource))]
public abstract class BaseEnemyStateMachine<TEnum> : StateManager<TEnum> where TEnum : Enum
{
    public static event Action<int> OnEnemyDefeated;

    [Header("Estadísticas Base Enemigo")]
    [SerializeField] private float _initialHealth = 10f;
    [SerializeField] public int scorePoints = 100;

    [Header("Estadísticas de Comportamiento")]
    [SerializeField] public float velocity = 3f;
    [SerializeField] public float attackRadius = 2f;

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
        // Lógica de Awake del BaseEnemy (obtener componentes, etc.)
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // La clase hija (CrackStateMachine) será responsable de
        // crear su diccionario de estados y pasarlo al StateManager base.
    }

    protected virtual void Start()
    {
        // Lógica de Start del BaseEnemy (establecer vida)
        currentLife = _initialHealth;

        // La clase hija (CrackStateMachine) será responsable de
        // establecer el estado inicial.
    }

    public virtual void TakeDamage(float quantity)
    {
        // 4. Obtenemos el *enum* del estado de muerte
        TEnum deathState = GetDeathStateEnum();

        // 5. Asumimos que el StateManager<TEnum> base expone el enum del estado actual
        //    (Ej. una propiedad llamada 'CurrentStateKey').
        //    Necesitamos esta comprobación para no "morir" varias veces.
        if (currentStateKey.Equals(deathState)) return;

        currentLife -= quantity;
        PlaySound(sfxDamageTaken);

        if (currentLife <= 0)
        {
            // 6. Usamos el método 'CambiarEstado' del StateManager base,
            //    que (asumimos) acepta el TEnum.
            TransitionToState(deathState);
        }
    }

    /// <summary>
    /// 7. El método abstracto ahora obliga a las clases hijas
    /// a devolver el *valor del enum* que representa la muerte.
    /// </summary>
    protected abstract TEnum GetDeathStateEnum();

    public void ReportScore()
    {
        OnEnemyDefeated?.Invoke(scorePoints);
    }

    // 8. ¡Ya no necesitamos el método 'CambiarEstado(BaseState)'!
    //    Confiamos en que la clase base StateManager<TEnum>
    //    proporciona 'CambiarEstado(TEnum newStateKey)'.

    // --- Métodos Helper (Llamados por el Contexto) ---
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