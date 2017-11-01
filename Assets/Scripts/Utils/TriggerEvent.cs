using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// TriggerEvent opens CollisionEnter/Stay/Exit in UnityEvent.
/// You can have restriction on wich colliders are allowed and wich ones are refused using allowedColliders and deniedColliders
/// </summary>
public class TriggerEvent : MonoBehaviour {
    [Header("Trigger collision events")]
    public UnityEvent OnEnter;
    public UnityEvent OnStay;
    public UnityEvent OnExit;

    [Header("Interval between events of the same type")]
    /// <summary>
    /// Interval between events of the same type
    /// </summary>
    public float interval = 0.1f;
    private float lastEnter;
    private float lastStay;
    private float lastExit;

    [Header("Colliders")]
    [Tooltip("If allowedColliders is empty all colliders exepct denied one fire the event")]
    /// <summary>
    /// List of collider allowed to trigger events
    /// </summary>
    public Collider[] allowedColliders;
    
    /// <summary>
    /// List of collider to ignore trigger events
    /// </summary>
    public Collider[] deniedColliders;

    private void Start()
    {
        lastEnter = lastStay = lastExit = Time.realtimeSinceStartup;
    }

    void OnTriggerEnter(Collider col) {
        if (Time.realtimeSinceStartup - lastEnter < interval)
            return;

        foreach (Collider c in deniedColliders)
        {
            if (c.Equals(col))
            {
                return;
            }
        }

        if(allowedColliders.Length == 0)
        {
            OnEnter.Invoke();
            return;
        }

        foreach (Collider c in allowedColliders)
        {
            if (c.Equals(col))
            {
                OnEnter.Invoke();
                return;
            }
        }
    }

    void OnTriggerStay(Collider col) {
        if (Time.realtimeSinceStartup - lastStay < interval)
            return;

        foreach (Collider c in deniedColliders)
        {
            if (c && c.Equals(col))
            {
                return;
            }
        }

        if (allowedColliders.Length == 0)
        {
            OnStay.Invoke();
            return;
        }

        foreach (Collider c in allowedColliders)
        {
            if (c && c.Equals(col))
            {
                OnStay.Invoke();
                return;
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (Time.realtimeSinceStartup - lastExit < interval)
            return;

        foreach (Collider c in deniedColliders)
        {
            if (c.Equals(col))
            {
                return;
            }
        }

        if (allowedColliders.Length == 0)
        {
            OnExit.Invoke();
            return;
        }

        foreach (Collider c in allowedColliders)
        {
            if (c && c.Equals(col))
            {
                OnExit.Invoke();
                return;
            }
        }
    }
}
