using UnityEngine;
using UnityEngine.UI;

public class FoodChecker : MonoBehaviour
{
    public Toggle[] toggles;

    // Bu besinin doğru olup olmadığını döner
    public bool IsCorrect()
    {
        foreach (Toggle t in toggles)
        {
            var option = t.GetComponent<ToggleOption>();

            if (option.isCorrect && t.isOn == false)
                return false;

            if (!option.isCorrect && t.isOn == true)
                return false;
        }

        return true;
    }
}
