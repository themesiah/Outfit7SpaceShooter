using UnityEngine;
using UnityEngine.InputSystem;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace SpaceShooter.Actors
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private ScriptableFloatReference fuelReference = default;

        [SerializeField]
        private Rigidbody playerBody = default;

        [SerializeField]
        private Transform modelTransform = default;

        [Header("Parameters")]
        [SerializeField]
        private ScriptableFloatReference moveSpeed = default;

        [SerializeField]
        private float acceleratedSpeedMultiplier = 2f;

        [SerializeField]
        private float accelerationFuelConsumptionRate = 1f;

        [SerializeField]
        private float maxTurningAngle = 30f;

        [SerializeField]
        private float turningSpeed = 25f;

        private bool accelerating = false;
        private bool moving = false;
        private float turningAngle = 0f;
        private float targetTurningAngle = 0f;
        private Vector2 lastMoveValue = Vector2.zero;

        public float AcceleratedMoveSpeed => moveSpeed.GetValue() * acceleratedSpeedMultiplier;

        private void OnEnable()
        {
            moveSpeed.RegisterOnChangeAction(OnSpeedChanged);
        }

        private void OnDisable()
        {
            moveSpeed.UnregisterOnChangeAction(OnSpeedChanged);
        }

        private void OnSpeedChanged(float newSpeed)
        {
            // If speed changes mid game, update the velocity without having to press again the input keys
            ChangeVelocity(lastMoveValue);
        }

        public void Move(InputAction.CallbackContext context)
        {
            // Get movement values from input system
            var values = context.ReadValue<Vector2>();
            // Check if we are moving. We will use this in the update to consume fuel.
            if (values.x != 0f || values.y != 0f)
                moving = true;
            else
                moving = false;
            ChangeVelocity(values);
            lastMoveValue = values;
        }

        public void Accelerate(InputAction.CallbackContext context)
        {
            // While the accelerating button is pressed, you accelerate
            if (context.started)
            {
                accelerating = true;
            } else if (context.canceled)
            {
                accelerating = false;
            }
            ChangeVelocity(lastMoveValue);
        }

        private void ChangeVelocity(Vector2 moveValue)
        {
            // If accelerating use the accelerated speed, if not, use normal speed
            var velocity = moveValue * (accelerating ? AcceleratedMoveSpeed : moveSpeed.GetValue());
            // Change the velocity force. The physx will do the rest.
            playerBody.velocity = velocity;
        }

        private void Update()
        {
            // If we are moving accelerated, consume fuel. Once you lose all your fuel, you lose.
            if (accelerating && moving)
            {
                float fuelConsumption = accelerationFuelConsumptionRate * Time.deltaTime;
                fuelReference.SetValue(fuelReference.GetValue() - fuelConsumption);
            }

            // Change ship turning depending on where is it moving
            // First get where do you have to rotate to
            if (lastMoveValue.y > 0.1f)
            {
                targetTurningAngle = maxTurningAngle;
            } else if (lastMoveValue.y < -0.1f)
            {
                targetTurningAngle = -maxTurningAngle;
            } else
            {
                targetTurningAngle = 0f;
            }
            // Get the angle with MoveTowards. The turning speed is also faster if accelerating
            turningAngle = Mathf.MoveTowards(turningAngle, targetTurningAngle, turningSpeed * Time.deltaTime * (accelerating ? 1.5f : 1f));
            // Change transform angle of the model. We change the model so it don't have any effect on the gameplay
            var euler = modelTransform.eulerAngles;
            euler.z = turningAngle;
            modelTransform.eulerAngles = euler;
        }
    }
}