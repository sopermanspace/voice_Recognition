using System.Collections;
using UnityEngine;
using Facebook.WitAi.TTS.Utilities;

public class IdlePlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject DanceObject;
    [SerializeField] private TTSSpeaker _speaker;

    private bool _canExecute = true;
    private Coroutine _sequenceCoroutine;

    private enum IdleSequence
    {
        Idle0,
        Idle1,
        Idle2
    }

    private enum TalkingSequence
    {
        Talking0 =0,
        Talking1 =1,
        Talking2 =2
    }

    private void Start()
    {   
        animator = GetComponent<Animator>();
        DanceObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_sequenceCoroutine == null && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle0")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle1")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle2")))
        {
              animator.SetBool("Idle", true); 
            _sequenceCoroutine = StartCoroutine(Sequence());            
        }
        else if (_sequenceCoroutine == null)
        {
            animator.SetBool("Idle", false);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
        {
            DanceObject.SetActive(true);
             animator.SetBool("Idle", false);  
        }
        else
        {
            DanceObject.SetActive(false);
            animator.SetBool("Idle", true);  
        }
          
        if (!_speaker.IsSpeaking)
        {      
            animator.SetBool("Speaking", false); 
            animator.SetBool("Idle", true);  
        }
        else
        {
            OnTelling();       
        }
    }

    public void OnDance()
    {
        animator.Play("Dance");
        animator.SetBool("Idle", false);
        animator.SetTrigger("Dance");                
    } 

  private IEnumerator Sequence()
{ 
    while (true)
    {
        int sequenceNumber = Random.Range(0, 3);
        animator.SetInteger("IdleSequence", (int)(IdleSequence)sequenceNumber);
        yield return new WaitForSeconds(5); 
    }
}


public void OnTelling() 
    {
        if (_speaker.IsSpeaking)
        { 
            animator.SetBool("Idle", false); 
            if (_canExecute)  
            {
                StartCoroutine(SetRandom());
                _canExecute = false;  
                animator.SetBool("Speaking", true);
            }                  
        } 
    }

public IEnumerator SetRandom()
{
    yield return new WaitForSeconds(4);
    int random = Random.Range(0, 3);
    animator.SetInteger("speakingState", random);

    _canExecute = true;
}

public void OnGreetings() => animator.SetTrigger("Hi");
public void OnGoodbyes() =>  animator.SetTrigger("Goodbye");
public void OnYes() => animator.SetTrigger("Yes");
public void OnPoint() => animator.SetTrigger("Point");
public void OnThumbsUp() => animator.SetTrigger("ThumbsUp");





}//class
