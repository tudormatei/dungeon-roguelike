using Dungeon.UI;
using UnityEngine;
using Core.ItemManagement;

namespace Core.Combat
{
    public class Sway : MonoBehaviour
    {
        private Controls controls;

        [SerializeField] private float amount;
        [SerializeField] private float maxAmount;
        [SerializeField] private float smoothAmount;

        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private bool on = false;

        private void Awake()
        {
            controls = new Controls();
        }

        private void Start()
        {
            ActivateSway();
        }

        public void ActivateSway()
        {
            if (!on) { on = true; }

            initialPosition = transform.localPosition;
            initialRotation = transform.localRotation;
        }

        private void Update()
        {
            if(!on) { return; }
            if(Interract.panelOpen) { return; }
            if (PauseMenu.isPaused) { return; }

            Vector2 mouseInput = controls.Gameplay.Mouse.ReadValue<Vector2>();
            float movementX = -mouseInput.x * amount;
            float movementY = -mouseInput.y * amount;

            movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
            movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

            Vector3 finalPosition = new Vector3(movementX, movementY, 0f);
            movementY = movementY / 150f;
            Vector3 finalRotation = new Vector3(movementY, movementX, 0f) * 200f;

            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(finalRotation) * initialRotation, Time.deltaTime * smoothAmount);
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
    }
}
