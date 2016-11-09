using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SettingsInformation : MonoBehaviour {

	public GameObject afkImage;
	private ExternalImageLoader afkLoader;
	public InputField afkImageName;
	public InputField afkImageWidth;
	public InputField afkImageHeight;

	void Start()
	{
		afkLoader = afkImage.GetComponent<ExternalImageLoader> ();

		afkImageName.onEndEdit.AddListener (delegate {
			UpdateLoaderImage ();
		});

		afkImageWidth.onEndEdit.AddListener (delegate {
			UpdateLoaderWidth ();
		});

		afkImageHeight.onEndEdit.AddListener (delegate {
			UpdateLoaderHeight ();
		});
	}

	void UpdateLoaderImage()
	{
		afkLoader.fileName = afkImageName.text;

		if (afkLoader.width > 0 && afkLoader.height > 0)
			afkLoader.LoadImage ();
	}

	void UpdateLoaderWidth()
	{
		int x = int.Parse (afkImageWidth.text);
		afkLoader.width = x;

		if (afkLoader.image.sprite != null && afkLoader.height > 0)
			afkLoader.LoadImage ();
	}

	void UpdateLoaderHeight()
	{
		int x = int.Parse (afkImageHeight.text);
		afkLoader.height = x;

		if (afkLoader.width > 0 && afkLoader.image.sprite != null)
			afkLoader.LoadImage ();
	}
}
