// Author:  Joseph Crump
// Date:    06/10/22

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Custom physics and input handler for the skipping rock.
/// </summary>
public class RockController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    [Tooltip("The plane on the Y-axis that is considered the surface level of the water.")]
    private float groundLevel = 0f;

    [Tooltip("The distance above ground level that skip inputs are permitted.")]
    public Stat ThresholdSize = 1f;

    [Tooltip("How high above the ground the rock starts.")]
    public Stat StartHeight = 8f;

    [Tooltip("Multiplier applied to Bounce Force on every subsequent skip.")]
    public Stat BounceDegradationMultiplier = 0.5f;

    [Tooltip("Multiplier applied to Bounce Force on missed skips.")]
    public Stat FaultyDegradationMultiplier = 0.25f;

    [Tooltip("How rapidly the object accelerates towards the ground.")]
    public Stat Gravity = -9.8f;

    [SerializeField]
    [Tooltip("The minimum velocity that the rock can reach.")]
    private float minVelocity = -30f;

    [SerializeField]
    [Tooltip("The maximum velocity that the rock can reach.")]
    private float maxVelocity = 30f;

    [SerializeField]
    [Tooltip("When the bounce force drops below this value, the rock sinks.")]
    private float sinkThreshold = 0.2f;

    [Header("Events")]
    [SerializeField]
    [Tooltip("Event callback for when the player successfully skips.")]
    private UnityEvent onSuccessfulSkip = new UnityEvent();

    [SerializeField]
    [Tooltip("Event callback for when the player attempts to skip before reaching the threshold area.")]
    private UnityEvent onPrematureSkip = new UnityEvent();

    [SerializeField]
    [Tooltip("Event callback for when the player doesn't press the skip input before reaching ground level.")]
    private UnityEvent onFaultySkip = new UnityEvent();

    [SerializeField]
    [Tooltip("Event callback for when the rock sinks.")]
    private UnityEvent onSink = new UnityEvent();

    [Header("References")]
    [SerializeField]
    private InputHandler inputHandler;

    /// <summary>
    /// Enumerator over every Stat belonging to the controller.
    /// </summary>
    public IEnumerable<Stat> AllStats
    {
        get
        {
            yield return ThresholdSize;
            yield return StartHeight;
            yield return bounceForce;
            yield return BounceDegradationMultiplier;
            yield return FaultyDegradationMultiplier;
            yield return Gravity;
        }
    }

    /// <summary>
    /// Returns true if the rock has stopped skipping and sank.
    /// </summary>
    public bool HasSunk { get; private set; } = false;

    private Stat bounceForce = 8f;

    private float position
    {
        get => transform.position.y;
        set
        {
            Vector3 position = transform.position;
            position.y = value;

            transform.position = position;
        }
    }
    private float velocity = 0f;
    private float acceleration = 0f;

    private void Start()
    {
        // initial bounce force based on starting height
        bounceForce = position - groundLevel;
    }

    private void OnValidate()
    {
        position = StartHeight;
    }

    private void OnEnable()
    {
        inputHandler.Skip.Pressed += OnPlayerPressedSkipButton;
    }

    private void OnDisable()
    {
        inputHandler.Skip.Pressed -= OnPlayerPressedSkipButton;
    }

    private void FixedUpdate()
    {
        acceleration += Gravity * Time.fixedDeltaTime;
        velocity += acceleration * Time.fixedDeltaTime;
        velocity = Mathf.Clamp(velocity, minVelocity, maxVelocity);

        position += velocity * Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (HasSunk)
            return;

        if (position < groundLevel && bounceForce < sinkThreshold)
        {
            Sink();
            return;
        }

        if (position < groundLevel && velocity <= 0f)
            FaultySkip();
    }

    /// <summary>
    /// Reset the internal state of the controller.
    /// </summary>
    public void ResetController()
    {
        HasSunk = false;
        position = StartHeight;
    }

    private void Skip()
    {
        DegradeBounceForce();
        Bounce();

        onSuccessfulSkip.Invoke();
    }

    private void FaultySkip()
    {
        DegradeBounceForce(faulty: true);
        Bounce();

        onFaultySkip.Invoke();
    }

    private void PrematureSkip()
    {
        onPrematureSkip.Invoke();
    }

    private void Sink()
    {
        HasSunk = true;

        ClearTemporaryModifiers();
        onSink.Invoke();
        Debug.Log("Sunk!");
    }

    private void OnPlayerPressedSkipButton()
    {
        if (position < ThresholdSize)
            Skip();
        else
            PrematureSkip();
    }

    private void Bounce()
    {
        acceleration = bounceForce;
        velocity = acceleration;
    }

    private void DegradeBounceForce(bool faulty = false)
    {
        var modifier = new StatModifier()
        {
            Value = faulty ? FaultyDegradationMultiplier : BounceDegradationMultiplier,
            Type = ModifierType.Multiply,
            Order = 99,
            Flags = ModifierFlags.Temporary
        };

        bounceForce.AddModifier(modifier);
    }

    private void ClearTemporaryModifiers()
    {
        foreach (var stat in AllStats)
        {
            stat.RemoveModifiersByFlag(ModifierFlags.Temporary);
        }
    }
}
