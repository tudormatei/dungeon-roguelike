using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public float health;
        public float maxHealth;

        public GameObject healthBarUI;
        public Slider slider;

        private float sliderTime = 2f;

        private void Start()
        {
            health = maxHealth;
            slider.maxValue = CalculateHealth();

            healthBarUI.SetActive(false);
        }

        private void Update()
        {
            if (health < maxHealth)
            {
                healthBarUI.SetActive(true);
            }

            if (slider.value <= 0)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }

            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public void DealDamage(float amount)
        {
            health -= amount;

            StopAllCoroutines();
            float value = CalculateHealth();
            StartCoroutine(MoveSlider(value));            
        }

        private IEnumerator MoveSlider(float value)
        {
            var startValue = slider.value;
            var endValue = value;
            float lerp = 0f;

            while (lerp < sliderTime)
            {
                lerp += Time.deltaTime;
                slider.value = Mathf.SmoothStep(startValue, endValue, lerp);
                yield return null;
            }

            slider.value = endValue;
        }

        private float CalculateHealth()
        {
            return health / maxHealth;
        }
    }
}
