using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public static BeatManager instance;
    public GameObject block;
    [Header("song setup")]
    [SerializeField] float songBPM = 110;
    [SerializeField] float firstBeatOffset = 6.591198f;
    [Header("output  for debugging")]
    [SerializeField] private float songPosition;
    [SerializeField] public float songCurrentBeat;
    //private
    public float secPerBeat;
    private AudioSource audioSource;
    bool foundStart = false;

    float songStart;
    private void Awake()
    {
        if (instance != null) Debug.LogError("instance of beatmanager already exists");
        instance = this;
        audioSource = GetComponent<AudioSource>();
        secPerBeat = 60 / songBPM;
        songStart = (float)AudioSettings.dspTime;
        audioSource.Play();
        
    }
    private void Start()
    {
        float x = 4;
        for (int i = 1; i <= 100; i++)
        {
            GameObject newblock = Instantiate(block);
            newblock.GetComponent<BeatBlock>().destinationBeat = i;
            newblock.transform.position = new Vector3(x, 9.5f, 0);
            newblock.GetComponent<BeatBlock>().destination = new Vector3(x, -9.5f, 0);
            x -= 1;
            if (x < -4) x = 4;
        }
    }

    private void Update()
    {
        songPosition = (float)AudioSettings.dspTime - songStart - firstBeatOffset;
        songCurrentBeat = songPosition / secPerBeat+1;
        


    }
   
    public float CurrentLoudness()
    {
        const int sampleDataLength = 1024;
        float[] clipSampleData = new float[sampleDataLength];


        audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
        float clipLoudness = 0f;
        foreach (var sample in clipSampleData)
        {
            clipLoudness += Mathf.Abs(sample);
        }
        clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
        return clipLoudness;

    }
}
