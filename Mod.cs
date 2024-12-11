using UnityEngine;
using MelonLoader;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using Il2Cpp;

namespace LightRunners
{
    public class Mod : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("If your game still runs bad after this, ur computer is just cooked");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                silverboost();
            }
        }

        private void silverboost()
        {
            var postprocessfilter = UnityEngine.Object.FindObjectOfType<PostProcessVolume>();
            if (postprocessfilter != null)
            {
                if (postprocessfilter.enabled)
                {
                    postprocessfilter.enabled = false;
                }
            }

            QualitySettings.SetQualityLevel(0, true);
            QualitySettings.shadows = ShadowQuality.Disable;
            QualitySettings.masterTextureLimit = 0;
            QualitySettings.antiAliasing = 0;
            QualitySettings.vSyncCount = 0;

            foreach (ReflectionProbe probe in GameObject.FindObjectsOfType<ReflectionProbe>())
            {
                probe.enabled = false;
            }
            var postProcessEffects = GameObject.FindObjectsOfType<PostProcessLayer>();
            foreach (var effect in postProcessEffects)
            {
                effect.enabled = false;
            }

            Camera.main.farClipPlane = 1;

            DisableRealtimeLighting();
            SetLowPhysicsQuality();
            SetLowLOD();
            ReduceDrawDistance();
            SetOcclusionCulling();
        }

        private void DisableRealtimeLighting()
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (var light in lights)
            {
                light.enabled = false;
            }
        }

        private void SetLowPhysicsQuality()
        {
            Time.fixedDeltaTime = 0.03f;
            Physics.defaultSolverIterations = 5;
            Physics.defaultSolverVelocityIterations = 5;
        }

        private void SetLowLOD()
        {
            LODGroup[] lodGroups = GameObject.FindObjectsOfType<LODGroup>();
            foreach (var lodGroup in lodGroups)
            {
                lodGroup.ForceLOD(0);
            }
        }

        private void ReduceDrawDistance()
        {
            Camera.main.farClipPlane = 1;
        }

        private void SetOcclusionCulling()
        {
            Camera.main.useOcclusionCulling = false;
        }
    }
}
