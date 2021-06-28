using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    //type
    //base speed
    //base delay
    //base range
    //base weight
    //base ttl
    //base prevalence
    //scalar value
    //maxX
    //minY
    public float _ttl;


    private bool _facingRight = true;
    private bool _isSwimming;
    private bool _isWaiting;
    private bool _isHooked = false;
    private bool _isCaught = false;

    [SerializeField]
    private float _speed = 2;

    private Animator _swimAC;
    private float _waitTime;
    private Vector2 _lastGoal;
    private Vector2 _goal = Vector2.zero;
    private Vector3 _lastPosition = Vector3.zero;

    private ClickHandler ch;
    // Start is called before the first frame update
    void Awake()
    {
        _swimAC = GetComponent<Animator>();
        ch = FindObjectOfType<ClickHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isWaiting && !_isSwimming && !_isHooked)
        {
            GenerateWait();
            GenerateGoal();
        }
        //if(transform.position.y > 5)
        //{
        //    _isCaught = true;
        //    Debug.Log("Fish Caught!");
        //    Destroy(gameObject, 1);
        //}
    }

    public void Hook()
    {
        Debug.Log("Hooked Fish!");
        _isHooked = true;
        StopCoroutine("Move");
        StopCoroutine("Wait");
        StopCoroutine("DestroyAfterTTL");
    }

    public void DestroyFish(float ttl)
    {
        StartCoroutine(DestroyAfterTTL(ttl));
    }

    private IEnumerator DestroyAfterTTL(float ttl)
    {
        
            while (ttl > 0)
            {
            //if (!_isHooked)
            //{
                ttl -= 1;
                _ttl = ttl;
                yield return new WaitForSeconds(1);
            //}
            }
            if(!_isHooked)
            Destroy(gameObject);
        
    }

    private void OnTriggerEnter()
    {
        Debug.Log("Fish on lure!");
        ch.NotifyClosestFish(this);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Fish off lure.");
        ch.NotifyExit(this);
    }

    void GenerateWait()
    {
        //nab these magic numbers
        _waitTime = Random.Range(0.0f, 4.0f);
        _isWaiting = true;
        StartCoroutine("Wait");
    }

    void GenerateGoal()
    {
        _lastGoal = _goal;
        _goal = new Vector2(Random.Range(-2.4f, 5.5f), Random.Range(-0.4f, 3.0f));
    }

    private IEnumerator Wait()
    {

        //magic numbers for swim speed and wrong rotation algo in wrong spot
        yield return new WaitForSeconds(_waitTime);
        _isWaiting = false;
        if (_goal.x - _lastGoal.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = Vector3.up * 180;
        }
        _swimAC.SetFloat("SwimSpeed", 1.5f);
        StartCoroutine("Move");
        _isSwimming = true;
    }

    private IEnumerator Move()
    {
        while (Vector3.Distance(transform.position,_goal) > 0.01f)
        {
            _lastPosition = transform.position;
            transform.position = Vector3.Lerp(transform.position, _goal, _speed * Time.deltaTime);
            
            yield return null;
        }
        _isSwimming = false;
        _swimAC.SetFloat("SwimSpeed", 1);
    }


    //Method that enables/disables collider when entering/exiting "foreground"
}
