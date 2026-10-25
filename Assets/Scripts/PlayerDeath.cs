
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using StarterAssets;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerDeath : MonoBehaviour
{
    //links to other scripts
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject gameOverPanel;

    //death settings
    [SerializeField] private float gameOverDelay = 2.5f;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private PlayerInput playerInput;
    private CharacterController characterController;
    private bool deathStarted;

    //setting up player components
    private void Awake()
    {
        if (playerHealth == null)
            playerHealth = GetComponent<PlayerHealth>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        thirdPersonController =
            GetComponent<ThirdPersonController>();

        starterAssetsInputs =
            GetComponent<StarterAssetsInputs>();

        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        if (playerHealth != null)
            playerHealth.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        if (deathStarted)
            return;

        deathStarted = true;

        // Stop normal player controls.
        if (thirdPersonController != null)
            thirdPersonController.enabled = false;

        if (starterAssetsInputs != null)
            starterAssetsInputs.enabled = false;

        if (playerInput != null)
            playerInput.enabled = false;

        // Play the death animation.
        if (animator != null)
            animator.SetTrigger("Die");

        StartCoroutine(ShowGameOver());
    }

    //allows pressing r to restart the level after death
    private void Update()
    {
     if (!deathStarted)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartLevel();
        }
    }


    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Freeze gameplay but keep the UI visible.
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}
