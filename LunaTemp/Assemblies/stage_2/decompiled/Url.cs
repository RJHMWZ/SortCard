using Luna.Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class Url : MonoBehaviour
{
	public const string DefaultAndroidUrl = "https://play.google.com/store/apps/details?id=com.merge.match3mystery";

	public const string DefaultIOSUrl = "https://apps.apple.com/us/app/matching-mystery/id6746231183";

	[Header("Jump Switch")]
	[Tooltip("Bind the click methods to an install button. The export platform determines how the store opens.")]
	[SerializeField]
	private bool enableStoreJump = true;

	[Header("MM Store URLs")]
	[FormerlySerializedAs("gameAUrlAndroid")]
	[SerializeField]
	private string androidUrl = "https://play.google.com/store/apps/details?id=com.merge.match3mystery";

	[FormerlySerializedAs("gameAUrlIOS")]
	[SerializeField]
	private string iosUrl = "https://apps.apple.com/us/app/matching-mystery/id6746231183";

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
		ClickButton();
	}

	private void Install(string iosUrl, string androidUrl)
	{
		if (!enableStoreJump)
		{
			Debug.Log("Store jump is disabled on Url.");
			return;
		}
		LifeCycle.GameEnded();
		Playable.InstallFullGame(iosUrl, androidUrl);
	}
}
