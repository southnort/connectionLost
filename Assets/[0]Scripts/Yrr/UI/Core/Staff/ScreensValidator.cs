using System.Reflection;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Yrr.UI
{
    internal sealed class ScreensValidator : MonoBehaviour
    {
        private void Start()
        {
            var windows = transform.GetComponentsInChildren<UIScreen>(true);

            foreach (var screen in windows)
            {
                ValidateFields(screen);
                ValidateButtons(screen);
            }
        }


        private static void ValidateFields(UIScreen screen)
        {
            var type = screen.GetType();

            foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (!field.GetCustomAttributes<SerializeField>().Any())
                    continue;

                if (field.GetValue(screen) == null)
                {
                    Debug.LogError($"Validating UI: {type.Name}.{field.Name} is not set");
                }
            }
        }

        private static void ValidateButtons(Component screen)
        {
            var buttons = screen.transform.GetComponentsInChildren<Button>(true);

            foreach (var button in buttons)
            {
                var onClick = button.onClick;


                if (onClick.GetPersistentEventCount() == 0 || onClick.GetPersistentTarget(0)==null)
                {
                    Debug.LogError($"Validating UI: {screen.GetType().Name}.{button.name} button empty");
                }
            }
        }
    }
}
