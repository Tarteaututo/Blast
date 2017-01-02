using UnityEngine;
using System.Collections;

[System.Serializable]
public class LinkedParticlePoolerSettings {
	public ParticlePooler linkedPooler;

	[System.Serializable]
	public class Settings {
		public bool isEnabled;
		public ParticlePooler.ActiveRandom activeRandom;
		public Material material;

		public void Initialize(ParticlePooler pooler) {
			pooler.activeRandom = this.activeRandom;
			pooler.enabled = this.isEnabled;
			if (!this.material)
				this.material = pooler.objectMaterial;
		}
	}

	public Settings onStartSettings = new Settings();
	public Settings onLoadSettings = new Settings();

	private bool isActive = false;

	public void Awake() {
		this.onStartSettings.Initialize(linkedPooler);
		this.isActive = this.onStartSettings.isEnabled;
		this.Load(!this.isActive);
	}

	public void Load(bool activation) {
		this.isActive = !this.isActive;

		if (this.isActive) {
			this.linkedPooler.SetPoolerAble(this.onLoadSettings.isEnabled);
			this.linkedPooler.activeRandom = this.onLoadSettings.activeRandom;
			if (this.onLoadSettings.material)
				this.linkedPooler.objectMaterial = this.onLoadSettings.material;

		} else {
			this.linkedPooler.SetPoolerAble(this.onStartSettings.isEnabled);
			this.linkedPooler.activeRandom = this.onStartSettings.activeRandom;
			if (this.onStartSettings.material)
				this.linkedPooler.objectMaterial = this.onStartSettings.material;

		}
	}
}
