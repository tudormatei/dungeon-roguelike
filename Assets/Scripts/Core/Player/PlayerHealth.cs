using Core.ItemManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Core.Enemy;

namespace Core.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float outOfCombatTimeForRegen;
        [SerializeField] private float timeBetweenRegen;
        private float timeSinceLastAttack = 0f;
        private float timeSinceLastRegen = 0f;

        public float health;
        public float maxHealth;

        public TextMeshProUGUI healthText;
        public Slider slider;
        private PlayerAttributes playerAttributes;

        private bool alive = true;
        float sliderTime;

        private void Start()
        {
            playerAttributes = GetComponent<PlayerAttributes>();
            maxHealth = playerAttributes.defaultAttributes[Attributes.Max_Health.ToString()];

            health = maxHealth;
            slider.maxValue = 1f;
        }

        private void Update()
        {
            RefreshMaxHealth();

            if (health <= 0 && alive)
            {
                health = 0f;
                alive = false;
                print("player died :(");
            }

            if (health >= maxHealth)
            {
                health = maxHealth;
            }

            timeSinceLastAttack += Time.deltaTime;
            timeSinceLastRegen += Time.deltaTime;

            CheckRegen();
        }

        public void DealDamage(float amount, EnemyAttributes attacker)
        {
            if(health <= 0f) { return; }

            if(attacker != null) { sliderTime = attacker.AttackSpeed - 1f; }
            else { sliderTime = 2f; }
            

            timeSinceLastAttack = 0f;

            float defense;
            defense = playerAttributes.defaultAttributes[Attributes.Defense.ToString()] + playerAttributes.GetAttributeModifiedValue(Attributes.Defense);
            float healthDamage = amount / defense;
            health -= healthDamage;

            StopAllCoroutines();
            float value = CalculateHealth();
            StartCoroutine(MoveSlider(value));
        }

        private IEnumerator MoveSlider(float value)
        {
            var startValue = slider.value;
            var endValue = value;
            float lerp = 0f;

            healthText.text = ((int)(slider.value * maxHealth)).ToString() + " / " + ((int)maxHealth).ToString();

            while (lerp < sliderTime)
            {
                lerp += Time.deltaTime;
                slider.value = Mathf.SmoothStep(startValue, endValue, lerp);
                healthText.text = ((int)(slider.value * maxHealth)).ToString() + " / " + ((int)maxHealth).ToString();
                yield return null;
            }

            slider.value = endValue;
            healthText.text = ((int)(slider.value * maxHealth)).ToString() + " / " + ((int)maxHealth).ToString();
        }

        private float CalculateHealth()
        {
            maxHealth = playerAttributes.defaultAttributes[Attributes.Max_Health.ToString()] + playerAttributes.GetAttributeModifiedValue(Attributes.Max_Health);
            return health / maxHealth;
        }

        private void RefreshMaxHealth()
        {
            maxHealth = playerAttributes.defaultAttributes[Attributes.Max_Health.ToString()] + playerAttributes.GetAttributeModifiedValue(Attributes.Max_Health);
        }

        private void CheckRegen()
        {
            if(timeSinceLastAttack >= outOfCombatTimeForRegen && health < maxHealth && timeSinceLastRegen >= timeBetweenRegen)
            {
                RegenHealth();
            }
        }

        private void RegenHealth()
        {
            timeSinceLastRegen = 0f;

            float percent = playerAttributes.defaultAttributes[Attributes.Health_Regeneration.ToString()] + playerAttributes.GetAttributeModifiedValue(Attributes.Health_Regeneration);
            percent /= 100f;
            float healthToAdd = maxHealth * percent;
            health = health + healthToAdd;

            StopAllCoroutines();
            sliderTime = timeBetweenRegen - 1f;
            float value = CalculateHealth();
            StartCoroutine(MoveSlider(value));
        }
    }
}
