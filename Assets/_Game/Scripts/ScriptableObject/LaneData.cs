using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LaneType
{
    LeftLane = 0,
    MiddleLane = 1,
    RightLane = 2
};

[CreateAssetMenu(menuName = "LaneData")]
public class LaneData : ScriptableObject
{
    [SerializeField] Transform[] laneTransforms;
    [SerializeField] Vector3 occupationDetectionHalfExtend;

    public Transform[] GetTransform()
    {
        return laneTransforms;
    }

    public Vector3 RandomLane()
    {
        int randomIndex = Random.Range(0, laneTransforms.Length);
        return laneTransforms[randomIndex].position;
    }

    public Vector3 RandomTwoLane()
    {
        int randomIndex = Random.Range(1, laneTransforms.Length);
        return laneTransforms[randomIndex].position;
    }



}
