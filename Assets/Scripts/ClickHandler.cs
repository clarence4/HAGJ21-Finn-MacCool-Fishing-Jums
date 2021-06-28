using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private Transform castBar;
    [SerializeField] private float castChargeMax = 0.8f;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private float xMin = -2.4f;
    [SerializeField] private float xRange = 8.6f;
    [SerializeField] private Animator animator;
    [SerializeField] private float yMin = 0.0f;

    [Space]
    [SerializeField] private Transform lineCollider;

    public StudioEventEmitter castSFX;
    public StudioEventEmitter bloopSFX;

    private Fish closestFish;

    private GameManager gm;

    public bool UI = true;
    private bool lineIn;
    private bool lineOn = false;
    private bool fishOn;
    private bool casting;
    private float castCharge = 0.0f;

    private Vector3 castPos;
    private Vector3 lineCache;
    private Vector3 barCache;
    // Start is called before the first frame update
    void Start()
    {
        barCache = castBar.localScale;
        lineCache = lr.GetPosition(0);
        lineCollider.position = lr.GetPosition(0);
        gm = FindObjectOfType<GameManager>();
    }


    void Update()
    {
        
            if (lineIn)
            {
                if (lr.GetPosition(2).y > yMin)
                {
                    lr.SetPosition(2, lr.GetPosition(2) + Vector3.down * 0.004f);
                    lineCollider.position = lr.GetPosition(2);
                }
            }
            if (closestFish == null)
            {
                fishOn = lineOn = false;
            }
       
    }

    public void NotifyClosestFish(Fish fish)
    {
        if (!fishOn)
        {
            Debug.Log("Notifying closest");
            closestFish = fish;
            lineOn = true;
        }
    }

    public void NotifyExit(Fish fish)
    {
        if (!fishOn)
        {
            if (closestFish == fish)
            {
                closestFish = null;
                lineOn = false;
                Debug.Log("Notifying exit");
            }
        }
    }

    public void HandleClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (UI)
            {
                gm.Advance();
            }
            else
            {
                if (lineIn)
                {
                    if (lr.GetPosition(2).y < 3.35f - 0.1f)
                    {
                        if (!fishOn && lineOn && closestFish != null)
                        {

                            closestFish.transform.position = lineCollider.position;
                            closestFish.transform.Rotate(Vector3.forward * 90);
                            closestFish.transform.parent = lineCollider;
                            closestFish.Hook();
                            fishOn = true;

                        }
                        else
                        {
                            PullLine();
                        }
                    }
                    else
                    {
                        ResetCast();
                    }
                }
                else
                {
                    HandleCast();
                }
            }
        }
        //else if (context.performed)
        //    Debug.Log("Action was performed");
        else if (context.canceled)
        {
            Debug.Log("Action was cancelled");
            casting = false;
        }
    }

    private void PullLine()
    {
        Debug.Log("pull dat line, son!");
        lr.SetPosition(2, lr.GetPosition(2) + Vector3.up * 0.7f);
        lineCollider.position = lr.GetPosition(2);
        //bob the line whilst inneth the water
    }

    private void ResetCast()
    {
        StopCoroutine("ChargeCast");
        lr.SetPosition(1, lineCache);
        lr.SetPosition(2, lineCache);
        lineCollider.position = lr.GetPosition(2);
        lineIn = false;
        casting = false;
    }

    private void HandleCatch()
    {

    }

    private void HandleCast()
    {
        ResetCast();
        Debug.Log("Cast started");
        casting = true;
        StartCoroutine("ChargeCast");
    }

    private IEnumerator ChargeCast()
    {

        while (casting)
        {
            if (castCharge <= castChargeMax)
            {
                castCharge += 0.1f;
                castBar.localScale += Vector3.right * 0.1f;
            }
            yield return new WaitForSeconds(0.1f);
        }
        Cast(castCharge);
        castCharge = 0.0f;
        castBar.localScale = barCache;
    }

    private void Cast(float dist)
    {
        Debug.Log("Cast!");
        animator.Play("Cast");
        castSFX.Play();
        MoveLineSimple(dist);
    }

    private void MoveLineSimple(float dist)
    {
        float newX = (dist / castChargeMax * xRange)-2.4f;
        castPos = new Vector3( newX,3.4f,0.0f );
        Debug.Log(castPos);
        StartCoroutine("TweenLineSimple");
    }

    private IEnumerator TweenLineSimple()
    {
        Debug.Log("Entering Tween Line");
        float progress = 0.0f;
        float total = 10.0f;
        while (Vector3.Distance(lr.GetPosition(1), castPos) > 0.1f)
        {
            lr.SetPosition(1, Vector3.Lerp(lineCache, castPos, progress/total));
            lr.SetPosition(2, lr.GetPosition(1));
            progress += 1;
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Exiting Tween Line");
        lineCollider.position = lr.GetPosition(2);
        bloopSFX.Play();
        lineIn = true;
    }
}
