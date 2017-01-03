﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NewLoader))]
public class LoaderLinkedPooler : LoaderLinked {
	[SerializeField] LinkedParticlePoolerSettings[] linkedPoolerSettings = new LinkedParticlePoolerSettings[0];

	protected override void InitializeLinkedsElements() {
		foreach (LinkedParticlePoolerSettings element in this.linkedPoolerSettings) {
			Debug.Log("SetState");

			element.Awake();
		}
	}

	protected override void SetState(bool isActive) {
		foreach(LinkedParticlePoolerSettings element in this.linkedPoolerSettings) {
			Debug.Log("SetState By Loader");
			element.Load(isActive);
		}
	}
}