using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static LevelManager;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject targetPreFab;

    private List<GameObject> TargetsList = new();

    public void GenerateLevel(ModeData modeData)
    {
        ClearTargets();

        GetComponent<RectTransform>().offsetMax = new Vector2(0, -920);
        GetComponent<RectTransform>().offsetMin = new Vector2(0, 200);

        List<Vector2> occupiedPositions = new();
        List<TargetData> occupiedTargets = new();
        bool targetChosen = false;

        for (int i = 0; i < modeData.numberOfTargetsOnScreen; i++)
        {
            Vector2 positionToSet = FindTargetPosition(occupiedPositions, modeData.targetPositions);
            occupiedPositions.Add(positionToSet);

            TargetData targetToSet = FindTargetObjective(occupiedTargets, modeData.targetDataList);
            occupiedTargets.Add(targetToSet);

            targetToSet.position = positionToSet;
            targetToSet.size = modeData.targetSize;
            if (!targetChosen)
            {
                targetToSet.isTarget = true;
                targetChosen = true;
                GameManager.Instance.ChangeTargetObjective(targetToSet);
            }
            else
            {
                targetToSet.isTarget = false;
            }

            GameObject target = Instantiate(targetPreFab, positionToSet, Quaternion.identity, GetComponent<Transform>());
            target.name= targetToSet.targetObjective.ToString();
            target.GetComponent<Target>().AssignData(targetToSet);
            TargetsList.Add(target);
        }
    }

    private Vector2 FindTargetPosition(List<Vector2> occupiedPositions, List<Vector2> targetPositions)
    {
        Vector2 positionToSet = Vector2.zero;
        bool passed = false;
        while (!passed)
        {
            int randomIndex = Random.Range(0, targetPositions.Count);
            positionToSet = targetPositions[randomIndex];

            int i = 0;
            foreach (Vector2 occupiedPosition in occupiedPositions)
            {
                if (occupiedPosition == positionToSet) break;
                i++;
            }
            if (i == occupiedPositions.Count) passed = true;
        }
        return positionToSet;
    }

    private TargetData FindTargetObjective(List<TargetData> occupiedObjectives, List<TargetData> targetsData)
    {
        TargetData objectiveToSet = ScriptableObject.CreateInstance<TargetData>();
        bool passed = false;
        while (!passed)
        {
            int randomIndex = Random.Range(0, targetsData.Count);
            objectiveToSet = targetsData[randomIndex];

            int i = 0;
            foreach (TargetData occupiedObjective in occupiedObjectives)
            {
                if (occupiedObjective.targetObjective == objectiveToSet.targetObjective) break;
                i++;
            }
            if (i == occupiedObjectives.Count) passed = true;
        }
        return objectiveToSet;
    }

    private void ClearTargets()
    {
        foreach (GameObject target in TargetsList.ToList())
        { 
            TargetsList.Remove(target);
            Destroy(target);
        }
    }
}
