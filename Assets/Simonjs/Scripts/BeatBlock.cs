using UnityEngine;
using System.Collections;

public class BeatBlock : MonoBehaviour
{
    private Vector3 origin;
    public Vector3 destination;
    public float destinationBeat;
    public float travelTime = 5;
    private void Start()
    {
        origin = transform.position;
        
        
    }
    private bool hitBottom = false;
    private void LateUpdate()
    {
        if (hitBottom) return;
        float secPerBeat = BeatManager.instance.secPerBeat;
        float currentTime = BeatManager.instance.songCurrentBeat * secPerBeat;

        float hitTime = destinationBeat * secPerBeat;
        float startTime = hitTime - travelTime;
        transform.position = Vector3.Lerp(origin, destination, (currentTime-startTime)/travelTime);
        if ((currentTime - startTime) / travelTime >= 1) 
        {
            hitBottom = true;
            StartCoroutine(EndBlock());

        }
        

    }
    public IEnumerator EndBlock()
    {
        bool playerHasClicked = false;
        float blockDeathTime = Time.time+0.25f;
        while (!playerHasClicked&&Time.time<blockDeathTime)
        {
            Debug.Log(Time.time);
            playerHasClicked = playerHasClicked || Input.GetKey(KeyCode.Mouse0);
            if (playerHasClicked) GetComponent<MeshRenderer>().material.color = Color.green;
            yield return new WaitForSeconds(0);
        }
        GetComponent<MeshRenderer>().material.color = playerHasClicked ? Color.green:Color.red;
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
