using System.IO;
using UnityEngine;

namespace Geekbrains
{
	public sealed class SaveDataRepository
	{
		private readonly IData<SerializableGameObject> _data;

		private const string _folderName = "dataSave";
		private const string _fileName = "data.dat";
		private readonly string _path;

		public SaveDataRepository()
		{
			_data = new JsonData<SerializableGameObject>();
			_path = Path.Combine(Application.dataPath, _folderName);
			
		}

		public void Save()
		{
			if (!Directory.Exists(Path.Combine(_path)))
			{
				Directory.CreateDirectory(_path);
			}
			var player = new SerializableGameObject
			{
				Pos = GameObject.FindGameObjectWithTag(TagManager.PLAYER).transform.position, 
				Name = "Player",
				IsEnable = true
			};

			_data.Save(player, Path.Combine(_path, _fileName));
		}

		public void Load()
		{
			var file = Path.Combine(_path, _fileName);
			if (!File.Exists(file)) return;
			var newPlayer = _data.Load(file);
			var player = GameObject.FindGameObjectWithTag(TagManager.PLAYER).transform;
			player.position = newPlayer.Pos;
			player.name = newPlayer.Name;
			player.gameObject.SetActive(newPlayer.IsEnable);

			Debug.Log(newPlayer);
		}
	}
}