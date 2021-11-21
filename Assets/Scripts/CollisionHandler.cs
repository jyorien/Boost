using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delaySeconds = 1f;
    [SerializeField] AudioClip deathAudio;
    [SerializeField] AudioClip successAudio;

    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource audioSource;
    bool isTransitioning = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (isTransitioning) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                StartFinishSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }
    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        if (deathParticle.isStopped)
        {
            deathParticle.Play();
            Debug.Log("death particles playt");
        }

        audioSource.Stop();
        audioSource.PlayOneShot(deathAudio);
        Debug.Log("death is like wind");
        isTransitioning = true;
        Invoke("ReloadLevel", delaySeconds);
    }
    void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }

    void StartFinishSequence()
    {
        GetComponent<Movement>().enabled = false;
        successParticle.Play();

        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        isTransitioning = true;
        Invoke("LoadNextLevel", delaySeconds);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


}
