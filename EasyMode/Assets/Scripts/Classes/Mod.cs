using System.Collections;
using System.Collections.Generic;
using Thor.Core;
using UnityEngine;

namespace Mod_f41b71faab70436c8f46dd8c6b5377cd
{
    public class Mod : MonoBehaviour
    {
        // Set to true when the mod has performed initial setup
        private bool mInitialized = false;

        [SerializeField]
        private List<AssetReference<ExtendedBehaviorTree>> m_PlayerLoadedFloorBehaviors = new List<AssetReference<ExtendedBehaviorTree>>();

        private WaitForSeconds m_shortWaiter = new WaitForSeconds(2);

        private void Update()
        {
            if (!mInitialized && Services.Players.PrimaryPlayer.IsValid())
            {
                Services.Events.RegisterGameEvent(GameEventType.FloorLoadEnd, OnFloorLoadEnd);
                mInitialized = true;
            }
        }

        private void OnFloorLoadEnd(GameEvent gameEvent)        
        {
            StartCoroutine(RunOnFloorLoadBehaviorsCoroutine());            
        }

        private IEnumerator RunOnFloorLoadBehaviorsCoroutine()
        {
            SimEntity primaryPlayerSimEntity = Services.Players.PrimaryPlayer.SimEntity;
            SimEntity secondaryPlayerSimEntity = null;
            if (Services.Players.SecondaryPlayer.IsValid())
            {
                secondaryPlayerSimEntity = Services.Players.SecondaryPlayer.SimEntity;
            }

            // Wait a bit for the player to have fallen and be visible
            yield return m_shortWaiter;

            foreach (var behavior in m_PlayerLoadedFloorBehaviors)
            {
                // Run the behavior targeting the primary player
                if (!Services.Runner.Run(behavior.Asset, primaryPlayerSimEntity, primaryPlayerSimEntity))
                {
                    Debug.LogError($"[EasyMode Mod] Unable to run floor load behavior");
                }

                // If in coop, also target the second player
                if (secondaryPlayerSimEntity != null)
                {
                    if (!Services.Runner.Run(behavior.Asset, secondaryPlayerSimEntity, secondaryPlayerSimEntity))
                    {
                        Debug.LogError($"[EasyMode Mod] Unable to run floor load behavior on secondary player");
                    }
                }
            }
        }
    }
}
