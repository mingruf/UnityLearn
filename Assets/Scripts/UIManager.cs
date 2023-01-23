using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;
    private void Awake()
    {
        gameCanvas= FindObjectOfType<Canvas>();
        
    }
    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }
    public void CharacterTookDamage(GameObject gameObject, int damage)
    {
        Vector3 spawPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        TMP_Text tMPText = Instantiate(damageTextPrefab, spawPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tMPText.text = damage.ToString();
    }

    public void CharacterHealed(GameObject gameObject, int health)
    {
        Vector3 spawPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        TMP_Text tMPText = Instantiate(healthTextPrefab, spawPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tMPText.text = health.ToString();
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if(context.started)
        {

            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;//如果是在unity编译器中
            #else
                    Application.Quit();//否则在打包文件中
            #endif

        }
    }
}
