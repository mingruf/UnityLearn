using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTileLauncher : MonoBehaviour
{
    public GameObject projectTilePrefab;
    public Transform launcherPoint;

    public void FireProjectTile()
    {
       GameObject gameObject =  Instantiate(projectTilePrefab, launcherPoint.position,projectTilePrefab.transform.rotation);
       Vector3 oriScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(
            oriScale.x * transform.localScale.x > 0 ? 1 : -1,
            oriScale.y,
            oriScale.z
            );
    }
}
