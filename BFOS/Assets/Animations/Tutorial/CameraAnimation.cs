using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private Animator canimatror;

    // Start is called before the first frame update
    void Start()
    {
        canimatror = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AgentIn()
    {
        if (canimatror != null)
        {
            canimatror.SetTrigger("TrAgentIn");
        }
    }
    public void AgentOut()
    {
        if ( canimatror != null)
        {
            canimatror.SetTrigger("TrAgentOut");
        }
    }

    public void AgentToPlayer()
    {
        if (canimatror != null)
        {
            canimatror.SetTrigger("TrAtp");
        }
    }

    public void PlayerToAgent()
    {
        if (canimatror != null)
        {
            canimatror.SetTrigger("TrPta");
        }
    }
}
