using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.Video; // For VideoPlayer
using UnityEngine.SceneManagement; // For scene management

public class VideoTextSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer
    public TextMeshProUGUI overlayText; // Reference to the TextMeshPro text
    public float textDisplayTime = 5f; // Time in seconds to display text (relative to video start)
    public string nextSceneName = "NextScene"; // Name of the scene to load when text is clicked

    private void Start()
    {
        // Ensure the text starts hidden
        if (overlayText != null)
        {
            overlayText.gameObject.SetActive(false);
        }

        // Start checking for the appropriate time to display the text
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd; // Handle when the video ends
            Invoke(nameof(ShowText), textDisplayTime); // Show the text after the set time
        }
    }

    private void ShowText()
    {
        if (overlayText != null)
        {
            overlayText.gameObject.SetActive(true); // Show the text
        }
    }

    private void Update()
    {
        // Detect if the text is clicked
        if (overlayText != null && overlayText.gameObject.activeSelf && Input.GetMouseButtonDown(0))
        {
            // Check if the click is on the text
            if (RectTransformUtility.RectangleContainsScreenPoint(
                overlayText.rectTransform,
                Input.mousePosition,
                Camera.main))
            {
                LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Optionally, hide the text when the video ends
        if (overlayText != null)
        {
            overlayText.gameObject.SetActive(false);
        }
    }
}
