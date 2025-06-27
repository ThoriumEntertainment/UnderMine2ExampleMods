using System.Collections;
using Thor.Core;
using UnityEngine;

namespace Mod_ddf88184e367440189877626c055ac2e
{
    public class Mod : MonoBehaviour
    {
        [SerializeField]
        private AssetReference<ExtendedBehaviorTree> m_hardModeGlobalBehavior;

        private void Awake()
        {
            StartCoroutine(HardModeCoroutine());
        }

        private IEnumerator HardModeCoroutine()
        {
            // Wait for a valid player
            while (!Services.Players.PrimaryPlayer.IsValid())
            {
                yield return null;
            }
            
            yield return null;
            
            if (!Services.Runner.Run(m_hardModeGlobalBehavior.Asset, null, null))
            {
                Debug.LogError("[HardMode Mod] Unable to run global behavior");
            }
        }
    }
}
