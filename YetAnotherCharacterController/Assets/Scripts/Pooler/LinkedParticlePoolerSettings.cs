using UnityEngine;
using System.Collections;

[System.Serializable]
public class LinkedParticlePoolerSettings {
	public ParticlePooler linkedPooler;

	[System.Serializable]
	public class Settings {
		public ParticlePooler.ActiveRandom activeRandom;
		public bool isEnabled;

		public void Initialize(ParticlePooler pooler) {
			pooler.activeRandom = this.activeRandom;
			pooler.enabled = this.isEnabled;
		}
	}

	public Settings onStartSettings = new Settings();
	public Settings onLoadSettings = new Settings();

	public void Awake() {
		this.onStartSettings.Initialize(linkedPooler);
	}

	public void Load(bool activation) {
		if (activation) {
			this.linkedPooler.SetPoolerAble(this.onLoadSettings.isEnabled);
			this.linkedPooler.activeRandom = this.onLoadSettings.activeRandom;
		} else {
			this.linkedPooler.SetPoolerAble(this.onStartSettings.isEnabled);
			this.linkedPooler.activeRandom = this.onStartSettings.activeRandom;
		}
	}
}
