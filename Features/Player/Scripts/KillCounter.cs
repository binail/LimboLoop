using System;
using UnityEngine;

namespace Features.Player.Scripts
{
    public class KillCounter : MonoBehaviour
    {
        private SuicideController suicideController;
        private static int needToKill = 100;
        private static int killCounter;

        public static event Action<float> EnemyKilled; 

        private void Start()
        {
            killCounter = 0;
        }


        private void Update()
        {
            if (killCounter >= needToKill)
            {
                suicideController.AllowSuicide();
                enabled = false;
            }
        }

        public static void GetKillPoints(int _points)
        {
            killCounter = Mathf.Clamp(killCounter + _points, 0, needToKill);

            var progress = (float)killCounter / needToKill;
            
            EnemyKilled?.Invoke(progress);
        }

        public static void SetRequiredPoints(int _needToKill)
        {
            needToKill = _needToKill;
        }

        public void Inject(SuicideController _suicideController)
        {
            suicideController = _suicideController;
        }
    }
}
