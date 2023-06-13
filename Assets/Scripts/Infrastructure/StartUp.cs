using System;
using CameraLogic;
using Logic.Hero;
using Services;
using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] private GameObject hero;
        
        private void Awake()
        {
            RegisterInputService();

            ConstructHero();

            SetCameraFollow();
        }

        private void ConstructHero()
        {
            hero.GetComponent<HeroMove>().Construct();
            hero.GetComponent<HeroAttack>().Construct();
            hero.GetComponent<FootprintSpawner>().Construct();
        }

        private void SetCameraFollow()
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }

        private void RegisterInputService()
        {
            IInputService inputService;
            if (Application.isMobilePlatform)
            {
                inputService = new MobileInputService();
            }
            else
            {
                inputService = new StandaloneInputService();
            }
            
            AllServices.Container.RegisterSingle<IInputService>(inputService);
        }
    }
}