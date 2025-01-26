using UnityEngine;
using UnityEngine.Video; // For VideoPlayer
using UnityEngine.SceneManagement; // For scene management

public class VideoSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer
    public string nextSceneName = "fi"; // Name of the scene to load after the video finishes

    private void Start()
    {
        // Ensure the video player is assigned
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer is not assigned!");
            return;
        }

        // Subscribe to the loopPointReached event (triggers when video finishes)
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Load the specified next scene when the video ends
        Debug.Log("Video finished. Loading next scene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}
