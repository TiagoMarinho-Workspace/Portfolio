using UnityEngine;
using UnityEngine.VFX; // Required for VisualEffect
using System.Collections;
using System.Collections.Generic;

public class WeakpointLinker : MonoBehaviour
{
    public GameObject beamPrefab;
    private List<VisualEffect> activeVFX = new List<VisualEffect>();
    private BossController boss;

    void Start()
    {
        boss = GetComponent<BossController>();
        StartCoroutine(InitializeBeams());
    }

    IEnumerator InitializeBeams()
    {
        yield return null; 

        if (boss != null && boss.weakPoints != null)
        {
            foreach (WeakPoint wp in boss.weakPoints)
            {
                GameObject beamGO = Instantiate(beamPrefab, transform);
                VisualEffect vfx = beamGO.GetComponent<VisualEffect>();
                
                if (vfx != null)
                {
                    activeVFX.Add(vfx);
                    beamGO.SetActive(false); // Hide until staggered
                }
            }
        }
    }

    void Update()
    {
        if (boss == null || boss.weakPoints == null) return;

        bool shouldShowBeams = (boss.currentState == BossState.Staggered);

        for (int i = 0; i < boss.weakPoints.Count; i++)
        {
            // 1. Declare variables at the START of the loop
            if (i >= activeVFX.Count) break;
            
            WeakPoint wp = boss.weakPoints[i];
            VisualEffect vfx = activeVFX[i];

            // 2. Perform the logic check
            if (!shouldShowBeams || wp.isDestroyed)
            {
                if (vfx.gameObject.activeSelf) vfx.gameObject.SetActive(false);
                continue;
            }
            else
            {
                if (!vfx.gameObject.activeSelf) vfx.gameObject.SetActive(true);
            }

            // 3. Update the VFX properties
            // Make sure these names match your Blackboard EXACTLY
            vfx.SetVector3("BeamStart", transform.position);
            vfx.SetVector3("BeamEnd", wp.transform.position);
        }
    }
}