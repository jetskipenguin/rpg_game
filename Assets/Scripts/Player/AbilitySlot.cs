using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [System.NonSerialized] public bool inCooldown;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float cooldownTime;
    [SerializeField] private Slider slider;

    [System.NonSerialized] public string abilityName;
    private PlayerController player;
    private float timeLeft;
    
    void Start() {
        abilityName = gameObject.name.Replace("Slot", "");
        timeLeft = staticVariables.getTimeLeft(abilityName);
        if(timeLeft > 0)
		{
            inCooldown = true;
		}
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        slider.value = staticVariables.getCooldown(abilityName);
    }

    void FixedUpdate() {
        if(inCooldown) {
            RunCooldown();
        }
    }

    private void StartCooldown() {
        staticVariables.changeTimeLeft(abilityName, cooldownTime);
        timeLeft = cooldownTime;
        inCooldown = true;

        staticVariables.changeCooldown(abilityName, 0);
        slider.value = 0;
    }

    private void RunCooldown() 
    {
        staticVariables.changeTimeLeft(abilityName, staticVariables.getTimeLeft(abilityName) - 1f);
        timeLeft -= 1f;
        staticVariables.changeCooldown(abilityName, ((cooldownTime - timeLeft) / cooldownTime));
        slider.value = staticVariables.getCooldown(abilityName);

        if(timeLeft <= 0) {
            staticVariables.changeCooldown(abilityName, 1f);
            slider.value = 1f;
            inCooldown = false;
        }
    }

    public void Activate(Vector2 playerPosition)
    {
        Instantiate(prefab, playerPosition, Quaternion.identity);
        StartCooldown();
    }
}
