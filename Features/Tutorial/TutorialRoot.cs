using System.Collections;
using Features.Cam;
using Features.Enemy.Scripts;
using Features.Enemy.Scripts.Abstract;
using Features.Enemy.Scripts.Waves;
using Features.Player.Scripts;
using UnityEngine;

namespace Features.Tutorial
{
    [DisallowMultipleComponent]
    public class TutorialRoot : MonoBehaviour
    {
        [SerializeField] private DialogueWindow _window;
        [SerializeField] private EnemyFabric _fabric;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private KillCounter _killCounter;
        [SerializeField] private CameraMovement _camera;
        [SerializeField] private GameObject _block;
        [SerializeField] private TutorialTrigger _enemyTrigger;
        [SerializeField] private SuicideTip _tip;
        
        private void Start()
        {
            StartCoroutine(ProcessTutorial());
        }

        private IEnumerator ProcessTutorial()
        {
            var playerTransform = _playerSpawner.Spawn();
            _block.SetActive(true);
            _fabric.Inject(playerTransform);
            _camera.Inject(playerTransform);
            var suicide = playerTransform.GetComponent<SuicideController>();
            suicide.Inject(_camera);
            _killCounter.Inject(suicide);
            var player = playerTransform.GetComponent<PlayerMovement>();
            _tip.Inject(playerTransform);
            KillCounter.SetRequiredPoints(1);
            
            _camera.ToFollow();
            
            yield return new WaitForSeconds(7f);
            
            var clicked = false;
            player.SetLock(true);
            _window.Open("You are dead! Now, you are in limbo!", () => { clicked = true;});

            while (clicked == false)
                yield return null;

            clicked = false;
            player.SetLock(false);
            yield return new WaitForSeconds(0.2f);
            
            player.SetLock(true);
            _window.Open("If you want to get out and be reborn, you have to go through all of the 9 loops of limbo.\n" +
                         "To go to the next loop you should charge your sword by killing demons and kill yourself with a sword while transferring enough life energy."
                         , () => { clicked = true;});
            
            while (clicked == false)
                yield return null;

            clicked = false;
            player.SetLock(false);
            yield return new WaitForSeconds(0.2f);
            player.SetLock(true);
            _window.Open("The closer the circle of limbus is to the exit, the more life energy the sword needs in order for you to be reborn on the next one.\n" +
                "Now lets move further, go along the path.\n" +
                "Also try pressing the LSHIFT to dash in the direction of the mouse!", () => { clicked = true;});

            while (clicked == false)
                yield return null;

            clicked = false;
            player.SetLock(false);
            _block.SetActive(false);
            
            while (_enemyTrigger.IsTriggered == false)
                yield return null;
            
            yield return new WaitForSeconds(0.2f);
            player.SetLock(true);
            _window.Open("All loops are protected by demons. Kill this one...", () => { clicked = true;});
            
            while (clicked == false)
                yield return null;

            clicked = false;
            player.SetLock(false);
            yield return new WaitForSeconds(0.2f);

            
            _fabric.Spawn(new [] { EnemyType.Range });

            yield return null;
            yield return null;
            
            while (EnemyEntity.Count != 0)
                yield return null;
            
            yield return new WaitForSeconds(0.2f);
            player.SetLock(true);
            _window.Open("Well done! When you kill enemies your ultimate is charging.\n" +
                         "Ultimate is required to make a successful death and go to the next loop.\n" +
                         "Now, when you have charged your ultimate go to the obelisk and use it (press R)", () => { clicked = true;});
            
            while (clicked == false)
                yield return null;

            clicked = false;
            player.SetLock(false);
            WavesProcessor._waveNumber = -1;
        }
    }
}