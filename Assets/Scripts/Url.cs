using UnityEngine;
using UnityEngine.Serialization;

// Shared by Google and AppLovin. The export tool supplies the
// platform-specific JavaScript implementation of InstallFullGame.
public class Url : MonoBehaviour
{
    public const string DefaultAndroidUrl = "https://play.google.com/store/apps/details?id=com.merge.match3mystery";
    public const string DefaultIOSUrl = "https://apps.apple.com/us/app/matching-mystery/id6746231183";

    [Header("Jump Switch")]
    [Tooltip("Bind the click methods to an install button. The export platform determines how the store opens.")]
    [SerializeField] private bool enableStoreJump = true;

    [Header("MM Store URLs")]
    [FormerlySerializedAs("gameAUrlAndroid")]
    [SerializeField] private string androidUrl = DefaultAndroidUrl;
    [FormerlySerializedAs("gameAUrlIOS")]
    [SerializeField] private string iosUrl = DefaultIOSUrl;

    public void SetStoreJumpEnabled(bool enabled)
    {
        enableStoreJump = enabled;
    }

    public void ClickButton()
    {
        Install(iosUrl, androidUrl);
    }

    public void ClickButtonAndroid()
    {
        Install(androidUrl, androidUrl);
    }

    public void ClickButtonIOS()
    {
        Install(iosUrl, iosUrl);
    }

    public void OpenStoreByDevice()
    {
        // AppLovin selects the device store in the exported HTML. Google ignores
        // these URLs and opens the final URL configured on its ad platform.
        ClickButton();
    }

    private void Install(string iosUrl, string androidUrl)
    {
        if (!enableStoreJump)
        {
            Debug.Log("Store jump is disabled on Url.");
            return;
        }

        Luna.Unity.LifeCycle.GameEnded();
        Luna.Unity.Playable.InstallFullGame(iosUrl, androidUrl);
    }
}
