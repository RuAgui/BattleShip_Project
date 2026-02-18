using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración de Motores")]
    [SerializeField] float thrustSpeed = 60f;
    [SerializeField] float strafeSpeed = 40f;
    [SerializeField] float acceleration = 2f;

    [Header("Dinámicas de Vuelo")]
    [SerializeField] float lookSensitivity = 15f;    // Ajustada para el nuevo cálculo
    [SerializeField] float maxTurnSpeed = 90f;      // Límite de giro (grados por segundo)
    [SerializeField] float rotationSmoothness = 2f;
    [SerializeField] float tiltAmount = 35f;

    private Rigidbody rb;
    private Vector2 movementInput;
    private Vector2 lookInput;

    private float pitch;
    private float yaw;
    private float roll;
    private float currentForwardSpeed;
    private float currentStrafeSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearDamping = 2.5f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();

    }

    private void HandleMovement()
    {
        // Suavizado de avance y lateral
        currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, movementInput.y * thrustSpeed, Time.fixedDeltaTime * acceleration);
        currentStrafeSpeed = Mathf.Lerp(currentStrafeSpeed, movementInput.x * strafeSpeed, Time.fixedDeltaTime * acceleration);

        Vector3 forwardMove = transform.forward * currentForwardSpeed;
        Vector3 rightMove = (Quaternion.Euler(0, yaw, 0) * Vector3.right) * currentStrafeSpeed;

        rb.AddForce(forwardMove + rightMove, ForceMode.Acceleration);
    }

    private void HandleRotation()
    {
        // 1. Calculamos la intención de giro
        float yawInput = lookInput.x * lookSensitivity;
        float pitchInput = -lookInput.y * lookSensitivity;

        // 2. Limitamos la velocidad (para que no sea instantáneo)
        yawInput = Mathf.Clamp(yawInput, -maxTurnSpeed, maxTurnSpeed);
        pitchInput = Mathf.Clamp(pitchInput, -maxTurnSpeed, maxTurnSpeed);

        // 3. ¡IMPORTANTE! El Yaw NO se limita (Clamp), para poder dar vueltas de 360º
        yaw += yawInput * Time.deltaTime;

        // El Pitch SÍ se limita para no quedarnos boca abajo permanentemente
        pitch += pitchInput * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        // 4. Calculamos la inclinación lateral (Roll)
        float targetRoll = -movementInput.x * tiltAmount;
        roll = Mathf.Lerp(roll, targetRoll, Time.deltaTime * rotationSmoothness);

        // 5. Aplicamos la rotación final
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
    }

    public void OnMove(InputAction.CallbackContext context) => movementInput = context.ReadValue<Vector2>();
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}