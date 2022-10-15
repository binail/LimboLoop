using System.Collections;
using Features.Player.Scripts;
using UnityEngine;

namespace Features.Enemy.Scripts.Waves
{
    [DisallowMultipleComponent]
    public class WavesProcessor : MonoBehaviour
    {
        [SerializeField] private EnemyWave[] _waves;
        [SerializeField] private EnemyFabric _fabric;

        private KillCounter _killCounter;
        public static int _waveNumber;
        private bool isAllowedSuicide;

        private void Start()
        {
            isAllowedSuicide = false;
            _killCounter = GetComponent<KillCounter>();
            KillCounter.SetRequiredPoints(_waves[_waveNumber]._needToKill);
        }

        private void Update()
        {
            isAllowedSuicide = SuicideController.IsAllowed;
        }

        public void Begin()
        {
            if (_waves.Length == 0)
                return;
            
            StartCoroutine(ProcessWave(_waves[_waveNumber]));
        }

        private IEnumerator ProcessWave(EnemyWave wave)
        {
            if (SuicideController.IsSuicided == true)
                yield break;
            
            yield return new WaitForSeconds(wave.Groups[0].DelayBefore);
            
            _fabric.Spawn(wave.Groups[0].GetEnemies());

            yield return new WaitForSeconds(wave.Groups[0].DelayAfter);

            while (isAllowedSuicide == false && SuicideController.IsSuicided == false)
            {
                for (var i = 1; i < wave.Groups.Count; i++)
                {
                    yield return new WaitForSeconds(wave.Groups[i].DelayBefore);

                    if (SuicideController.IsSuicided == true)
                        yield break;
                    
                    if (isAllowedSuicide == true) yield break;

                    _fabric.Spawn(wave.Groups[i].GetEnemies());

                    yield return new WaitForSeconds(wave.Groups[i].DelayAfter);
                }
            }
        }
    }
}