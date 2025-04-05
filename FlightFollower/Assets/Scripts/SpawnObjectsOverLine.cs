using UnityEngine;

public class SpawnObjectsOverLine : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject objectToSpawn; // Prefab do objeto a ser instanciado
    [SerializeField] private Transform parent;

    private int numberOfObjects; // Número total de objetos a serem instanciados
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.LogError("Nenhum LineRenderer foi encontrado no GameObject. Por favor, adicione um componente LineRenderer.");
        }

        if (objectToSpawn == null)
        {
            Debug.LogError("Nenhum objeto a ser instanciado foi atribuído. Por favor, configure o prefab no Inspetor.");
        }
    }

    public void SpawnObjects()
    {
        if (lineRenderer == null || objectToSpawn == null || lineRenderer.positionCount < 2)
        {
            Debug.LogWarning("Configuração inválida. Certifique-se de que o LineRenderer possui pelo menos 2 pontos e que um prefab foi atribuído.");
            return;
        }

        // Obtém os pontos do LineRenderer
        Vector3[] linePositions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(linePositions);

        numberOfObjects = Mathf.Max(linePositions.Length / 10,1); 

        // Calcula o comprimento total da linha
        float totalDistance = 0f;
        for (int i = 1; i < linePositions.Length; i++)
        {
            totalDistance += Vector3.Distance(linePositions[i - 1], linePositions[i]);
        }

        // Espaçamento uniforme entre os objetos
        float spacing = totalDistance / (numberOfObjects - 1);
        float accumulatedDistance = 0f;

        // Percorre a linha e instancia os objetos
        int segmentIndex = 0;
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Verifica se estamos no último segmento
            while (segmentIndex < linePositions.Length - 1 &&
                   accumulatedDistance > Vector3.Distance(linePositions[segmentIndex], linePositions[segmentIndex + 1]))
            {
                accumulatedDistance -= Vector3.Distance(linePositions[segmentIndex], linePositions[segmentIndex + 1]);
                segmentIndex++;
            }

            // Garante que o índice não exceda os limites
            if (segmentIndex >= linePositions.Length - 1) break;

            // Calcula a posição exata no segmento atual
            Vector3 spawnPosition = Vector3.Lerp(
                linePositions[segmentIndex],
                linePositions[segmentIndex + 1],
                accumulatedDistance / Vector3.Distance(linePositions[segmentIndex], linePositions[segmentIndex + 1])
            );

            // Instancia o objeto
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, parent);

            // Avança a distância acumulada
            accumulatedDistance += spacing;
        }
    }

    public void ClearObjects()
    {
        if (parent != null)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
    }

}
