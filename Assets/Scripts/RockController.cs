// Author:  Joseph Crump
// Date:    06/10/22

using System.Collections;
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
    public Stat ThresholdSize = new Stat(1f);

    [Tooltip("How high above the ground the rock starts.")]
    public Stat StartHeight = new Stat(8f);

    [Tooltip("Multiplier applied to Bounce Force on every subsequent skip.")]
    public Stat BounceDegradationMultiplier = new Stat(0.5f);

    [Tooltip("Multiplier applied to Bounce Force on missed skips.")]
    public Stat FaultyDegradationMultiplier = new Stat(0.25f);

    [Tooltip("How rapidly the object accelerates towards the ground.")]
    public Stat Gravity = new Stat(-9.8f);

    [Tooltip("How long the player has to wait to skip again after a premature skip attempt.")]
    public Stat PrematureSkipCooldown = new Stat(.5f);

    [Tooltip("How far the player travels in one second")]
    public Stat LateralSpeed = new Stat(1f);

    [SerializeField]
    [Tooltip("When the bounce force drops below this value, the player can no longer skip.")]
    private float minSkippableBounceForce = 0.3f;

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
            yield return PrematureSkipCooldown;
            yield return LateralSpeed;
        }
    }

    /// <summary>
    /// Returns true if the rock has stopped skipping and sank.
    /// </summary>
    public bool HasSunk { get; private set; } = false;

    /// <summary>
    /// Returns true if the player's 'Skip' input is not locked (due to a premature skip).
    /// </summary>
    public bool CanSkip { get; private set; } = true;

    private Stat bounceForce = new Stat(8f);

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
        bounceForce = new Stat(position - groundLevel);
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
        ClearTemporaryModifiers();

        HasSunk = false;
        position = StartHeight;

        velocity = 0f;
        acceleration = 0f;
    }

    private void Skip()
    {
        if (velocity > 0f)
            return;

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

        StartCoroutine(PrematureSkipInputCooldown());
    }

    private void Sink()
    {
        HasSunk = true;

        onSink.Invoke();
    }

    private void OnPlayerPressedSkipButton()
    {
        if (!CanSkip || bounceForce < minSkippableBounceForce)
            return;

        if (position < ThresholdSize)
            Skip();
        else
            PrematureSkip();
    }

    private void Bounce()
    {
        acceleration = bounceForce / 2f;
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

    private IEnumerator PrematureSkipInputCooldown()
    {
        CanSkip = false;
        yield return new WaitForSeconds(PrematureSkipCooldown);
        CanSkip = true;
    }
}
