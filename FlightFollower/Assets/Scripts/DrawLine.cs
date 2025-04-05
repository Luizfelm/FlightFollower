using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DrawLine : MonoBehaviour
{
    [Header("Line Settings")]
    public Transform tip;

    [Header("Input Settings")]
    public InputActionProperty activateAction;

    private LineRenderer lineRenderer;
    private List<Vector3> linePositions = new();
    private int index;

    void Start()
    {        
        
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.LogError("Nenhum LineRenderer foi encontrado no GameObject. Por favor, adicione um componente LineRenderer.");
        }
        else
        {
            // Configuração inicial do LineRenderer
            lineRenderer.positionCount = 0;
        }
    }    

    public void Draw()
    {
        if(lineRenderer != null)
        {
            if (lineRenderer.positionCount == 0)
            {
                // Configuração inicial ao começar a desenhar
                index = 0;
                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, tip.position);
                linePositions.Clear();
                linePositions.Add(tip.position);
            }

            var currentPosition = lineRenderer.GetPosition(index);

            if (Vector3.Distance(currentPosition, tip.position) > 0.01f)
            {
                index++;
                lineRenderer.positionCount = index + 1;
                lineRenderer.SetPosition(index, tip.position);
                linePositions.Add(tip.position);
            }
        }
    }

    public void ClearLine()
    {
        lineRenderer.positionCount = 0; // Reseta os pontos
        linePositions.Clear();         // Limpa a lista de posições
        index = 0;
    }
}
