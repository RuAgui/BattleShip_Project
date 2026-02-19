using UnityEngine;
using System.Collections;

public class MotherShip : BaseShip
{
    [Header("SPAWNER SETTINGS")]
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private float spawnRate = 20f;
    [SerializeField] private MotherShip targetMothership; // Referencia a la otra MotherShip

    private void Start()
    {
        Health = 5000;  // Salud de la MotherShip
        if (shipPrefab != null)
        {
            StartCoroutine(SpawnRoutine()); // Iniciar la rutina de spawn
        }
        else
        {
            Debug.LogError("No se ha asignado el prefab de la nave en el inspector.");
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate); //Espera el tiempo definido entre spawns

            //Rango de naves que salen de la nave nodriza. Aleatorio.

            int groupSize = Random.Range(1, 6);

            for (int i = 0; i < groupSize; i++)
            {
                // Seleccionar un punto de spawn aleatorio
                int randomIndex = Random.Range(0, spawnPoint.Length);
                Transform spawnLocation = spawnPoint[randomIndex];

                // Instanciamos nave
                GameObject newShip = Instantiate(shipPrefab, spawnLocation.position, spawnLocation.rotation);

                // Muy importante, asignar el target a la nave que acabamos de crear, para que se dirija a la MotherShip enemiga

                UnitAI ai = newShip.GetComponent<UnitAI>();
                if (ai != null)
                {
                    ai.targetMotherShip = targetMothership; // Asignar la referencia a la MotherShip enemiga

                }
            }
            // PAUSA LARGA ENTRE GRUPOS DE NAVES.
            yield return new WaitForSeconds(spawnRate);
        }
        
    }
}
