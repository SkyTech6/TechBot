using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;


public class ExternalImageLoader : MonoBehaviour {

	public string fileName;
	public int width;
	public int height;
	public Image image;
	int imageSaved = 0;

	byte[] bytes;

	//save info
	public string fileSaveName;
	public string widthSaveName;
	public string heightSaveName;
	public string imageSavedSave;

	void Start()
	{
		image = gameObject.GetComponent<Image> ();
		image.enabled = false;

		imageSaved = PlayerPrefs.GetInt (imageSavedSave);
		if (imageSaved == 1)
			LoadSavedImage ();
	}

	public void LoadImage()
	{
		bytes = File.ReadAllBytes(Application.persistentDataPath + "/" + fileName);

		Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
		texture.filterMode = FilterMode.Trilinear;
		texture.LoadImage(bytes);
		Sprite sprite = Sprite.Create(texture, new Rect(0,0,width, height), new Vector2(0.5f,0.0f), 1.0f);

		image.sprite = sprite;

		SaveImage ();
	}

	void SaveImage()
	{
		imageSaved = 1;

		PlayerPrefs.SetString (fileSaveName, fileName);
		PlayerPrefs.SetInt (widthSaveName, width);
		PlayerPrefs.SetInt (heightSaveName, height);
		PlayerPrefs.SetInt (imageSavedSave, imageSaved);
	}

	void LoadSavedImage()
	{
		fileName = PlayerPrefs.GetString (fileSaveName);
		width = PlayerPrefs.GetInt (widthSaveName);
		height = PlayerPrefs.GetInt (heightSaveName);

		LoadImage ();
	}
}
