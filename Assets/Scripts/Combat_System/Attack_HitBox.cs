using System.Collections;
using UnityEngine;

public class Attack_HitBox : MonoBehaviour
{
    private MeshRenderer _MeshRenderer;
    private Combat_Manager _Combat_Manager;

    private bool _Is_Hit_Target;
    [SerializeField] private float _Animation_Time = 1.0f;

    void Start()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
        _MeshRenderer.enabled = false;
        _Combat_Manager = Object.FindAnyObjectByType<Combat_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _Is_Hit_Target = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _Is_Hit_Target = false;
    }

    public void CallToAttack()
    {
        if (_Combat_Manager.Get_Current_State() == GameMode.Exploration)
        {
            StartCoroutine(PlayAnimation(_Animation_Time));
            // playing attack animation even it missing a target

            if (_Is_Hit_Target)
            {
                Debug.Log("Entering Combat");
                _Combat_Manager.EnterCombat();
            }
        }        
    }

    private IEnumerator PlayAnimation(float delay)
    {
        // just a sample
        _MeshRenderer.enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        _MeshRenderer.enabled = false;
    }

}
