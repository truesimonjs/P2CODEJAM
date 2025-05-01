using UnityEngine;
using System.Collections;

public class BeatBlock : MonoBehaviour
{
    private Vector3 origin;
    public Vector3 destination;
    public float destinationBeat;
    public float travelTime = 5;
    public float buttonTimeoutTime = 20f;
    private float lastPress;
    private bool playerPressedButton 
    { get
        {
            Debug.Log(lastPress >= Time.time - buttonTimeoutTime);
            return lastPress >= Time.time - buttonTimeoutTime;
           
        } 
    }
    private void Start()
    {
        origin = transform.position;
        
        
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            lastPress = Time.time;
            
        }
        
    }
    private bool doLateUpdate = true;
    private void LateUpdate()
    {
        if (!doLateUpdate) return;
        float secPerBeat = BeatManager.instance.secPerBeat;
        float currentTime = BeatManager.instance.songCurrentBeat * secPerBeat;

        float hitTime = destinationBeat * secPerBeat;
        float startTime = hitTime - travelTime;
        transform.position = Vector3.Lerp(origin, destination, (currentTime-startTime)/travelTime);
        if ((currentTime - startTime) / travelTime >= 1) 
        {
           
            StartCoroutine(EndBlock());
            doLateUpdate = false;
        }
        

    }

   
    public IEnumerator EndBlock()
    {
        
        float blockDeathTime = Time.time+0.1f;
        while (!playerPressedButton&&Time.time<blockDeathTime)
        {
            
            
            if (playerPressedButton) GetComponent<MeshRenderer>().material.color = Color.green;
            yield return new WaitForSeconds(0);
        }
        GetComponent<MeshRenderer>().material.color = playerPressedButton ? Color.green:Color.red;
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
