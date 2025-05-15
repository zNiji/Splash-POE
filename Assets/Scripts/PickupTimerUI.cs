using UnityEngine;
using UnityEngine.UI;

public class PickupTimerUI : MonoBehaviour
{
    [SerializeField] private Image waterGunTimerImage; // UI Image for water gun timer
    [SerializeField] private Image umbrellaTimerImage; // UI Image for umbrella timer
    [SerializeField] private WaterGunShooting waterGunShooting; // WaterGunShooting component
    [SerializeField] private PlayerHealth playerHealth; // PlayerHealth component
    private CanvasGroup canvasGroup; // For showing/hiding the UI

    void Awake()
    {
        // Get or add CanvasGroup for visibility control
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        // Hide UI initially
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void Start()
    {
        // Validate assignments
        if (waterGunShooting == null || playerHealth == null || waterGunTimerImage == null || umbrellaTimerImage == null)
        {
            Debug.LogError("Missing assignments in PickupTimerUI: Check WaterGunShooting, PlayerHealth, WaterGunTimerImage, or UmbrellaTimerImage!");
            return;
        }

        // Subscribe to timer update events
        waterGunShooting.OnShootingTimerUpdate += UpdateWaterGunTimerUI;
        playerHealth.OnPauseTimerUpdate += UpdateUmbrellaTimerUI;
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (waterGunShooting != null)
        {
            waterGunShooting.OnShootingTimerUpdate -= UpdateWaterGunTimerUI;
        }
        if (playerHealth != null)
        {
            playerHealth.OnPauseTimerUpdate -= UpdateUmbrellaTimerUI;
        }
    }

    void UpdateWaterGunTimerUI(float remainingTime, float totalDuration)
    {
        UpdateTimerUI(waterGunTimerImage, remainingTime, totalDuration);
    }

    void UpdateUmbrellaTimerUI(float remainingTime, float totalDuration)
    {
        UpdateTimerUI(umbrellaTimerImage, remainingTime, totalDuration);
    }

    void UpdateTimerUI(Image timerImage, float remainingTime, float totalDuration)
    {
        if (remainingTime > 0f)
        {
            // Show UI
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            // Update fill amount (normalized between 0 and 1)
            timerImage.fillAmount = remainingTime / totalDuration;
        }
        else
        {
            // Hide timer image when expired
            timerImage.fillAmount = 0f;
            // Only hide CanvasGroup if both timers are inactive
            if (waterGunTimerImage.fillAmount == 0f && umbrellaTimerImage.fillAmount == 0f)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }
    }
}
