using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour {

	public int AverageFPS {get; private set;}
	public int HighestFPS {get; private set;}
	public int LowestFPS {get; private set;}

	public int frameRange = 60;

	int[] fpsBuffer;
	int fpsBufferIndex;

	void InitializeBuffer() {
		if (this.frameRange <= 0) {
			this.frameRange = 1;
		}
		this.fpsBuffer = new int[frameRange];
		this.fpsBufferIndex = 0;
	}

	void Update() {
		if (this.fpsBuffer == null || this.fpsBuffer.Length != frameRange) {
			this.InitializeBuffer();
		}
		this.UpdateBuffer();
		this.CalculateFPS();
	}

	void UpdateBuffer() {
		this.fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
		if (this.fpsBufferIndex >= this.frameRange) {
			this.fpsBufferIndex = 0;
		}
	}

	void CalculateFPS() {
		int sum = 0;
		int highest = 0;
		int lowest = int.MaxValue;

		for (int i = 0; i < this.frameRange; i++) {
		//	sum += this.fpsBuffer[i];
			int fps = this.fpsBuffer[i];
			sum += fps;
			if (fps > highest) {
				highest = fps;
			}
			if (fps < lowest) {
				lowest = fps;
			}
		}
		this.AverageFPS = sum / this.frameRange;
		this.HighestFPS = highest;
		this.LowestFPS = lowest;
	}
}
