using UnityEngine;
using System.Collections.Generic;

public class AirplaneController : MonoBehaviour
{
    [Header("Configurações")]
    public GameObject checkpointsBucket;    
    public float speed = 5f;        // Velocidade do avião
    public float rotationSpeed = 2f; // Velocidade da rotação do avião
    public int interpolationSteps = 10; // Número de pontos intermediários entre cada checkpoint

    private Transform[] checkpoints; // Lista de checkpoints originais
    private List<Vector3> smoothPath; // Caminho interpolado
    private int currentPathIndex = 0;

  

    public void GeneratePath()
    {
        AttachCheckpoints(); // Anexa os checkpoints do bucket

        GenerateSmoothPath(); // Gera os pontos intermediários 
    }

    public void AttachCheckpoints()
    {
        int count = checkpointsBucket.transform.childCount;
        checkpoints = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            checkpoints[i] = checkpointsBucket.transform.GetChild(i);
        }
    }


    void FixedUpdate()
    {
        if (checkpoints != null && smoothPath != null && smoothPath.Count > 0)
        {
            // Verifique se o currentPathIndex está dentro do intervalo válido
            if (currentPathIndex >= smoothPath.Count)
            {
                currentPathIndex = 0; // Reinicia o índice para o primeiro ponto do caminho
            }

            Vector3 targetPoint = smoothPath[currentPathIndex];

            // Direção para o ponto alvo
            Vector3 direction = (targetPoint - transform.position).normalized;

            // Rotação suave em direção ao ponto alvo
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Movimento suave para o ponto alvo
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

            // Verificar se alcançou o ponto alvo
            if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                currentPathIndex++;

                // Verificar se atingiu o último ponto
                if (currentPathIndex >= smoothPath.Count)
                {
                    currentPathIndex = 0; // Reinicia o percurso
                }
            }
        }
    }


    void GenerateSmoothPath()
    {
        smoothPath = new List<Vector3>();

        // Garantir pelo menos 4 checkpoints para o Catmull-Rom
        if (checkpoints.Length < 4)
        {            
            return;
        }

        // Adicionar pontos intermediários usando Catmull-Rom
        for (int i = 0; i < checkpoints.Length; i++)
        {
            Vector3 p0 = checkpoints[LoopIndex(i - 1)].position;
            Vector3 p1 = checkpoints[LoopIndex(i)].position;
            Vector3 p2 = checkpoints[LoopIndex(i + 1)].position;
            Vector3 p3 = checkpoints[LoopIndex(i + 2)].position;

            for (int j = 0; j <= interpolationSteps; j++)
            {
                float t = j / (float)interpolationSteps;
                smoothPath.Add(CatmullRom(p0, p1, p2, p3, t));
            }
        }
    }

    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Fórmula de Catmull-Rom para pontos intermediários
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * (
            (2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3
        );
    }

    int LoopIndex(int index)
    {
        // Garante que o índice seja cíclico (loop nos checkpoints)
        if (index < 0) return checkpoints.Length - 1;
        if (index >= checkpoints.Length) return 0;
        return index;
    }
    
}
