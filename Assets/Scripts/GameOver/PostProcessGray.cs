using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Video;
public class PostProcessGray : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume volume;
    [SerializeField]
    public float gameover;
    [SerializeField]
    private List<GameObject> goList;
    [SerializeField] 
    private List<GameObject> turtleList;
    [SerializeField] 
    private CoralManager coralManager;
    [SerializeField]
    private GameObject locomotion;
    [SerializeField]
    private GameObject gameOverRoom;
    [SerializeField]
    private VideoPlayer video;
    [SerializeField]
    private AudioSource audioS;
    [SerializeField] private GameObject wasteSpawner;
    public int aliveCorals;
    private bool _started = false;

    void Update(){
        if (aliveCorals <= 0 && !_started){
            _started = true;
            StartCoroutine(ChangeWeight(5f));
        }
    }
    
    IEnumerator ChangeWeight(float time)
    {
        float gone = 0;
        while (gone < time)
        {
            volume.weight = gone / time;
            gone += Time.deltaTime;
            yield return null;
        }
        volume.weight = 0;

        foreach (GameObject go in goList)
        {
            go.SetActive(false);
        }

        foreach (Transform waste in wasteSpawner.transform)
        {
            Destroy(waste.gameObject);
        }
        
        // deactivate waste spawner
        wasteSpawner.SetActive(false);
        
        gameOverRoom.transform.position = locomotion.transform.position;
        gameOverRoom.transform.rotation = Quaternion.Euler(gameOverRoom.transform.rotation.eulerAngles.x, locomotion.transform.eulerAngles.y, gameOverRoom.transform.rotation.eulerAngles.z);
        gameOverRoom.SetActive(true);
        video.Play();
        audioS.Play();
    }

    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }
}
