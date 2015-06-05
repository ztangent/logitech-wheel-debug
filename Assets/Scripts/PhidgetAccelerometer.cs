using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Phidgets;
using Phidgets.Events;

public class PhidgetAccelerometer : MonoBehaviour {

	public Text AttachedText;
	public Text AccelerationText;
	public Text TiltText;
	public Text TimestampText;

	public float timestamp { get; private set; }
	public double magnitude { get; private set; }
	public List<double> acceleration
	{
		get { return new List<double>(_acceleration); }
	}
	public List<double> tilt
	{
		get { return new List<double>(_tilt); }
	}

	private double[] _acceleration = new double[3];
	private double[] _tilt = new double[3];
	private Accelerometer device;

	// Use this for initialization
	void Start () {
		device = new Accelerometer();
		device.open();
	}
	
	// Update is called once per frame
	void Update () {
		if (device.Attached) {
			if (AttachedText){
				AttachedText.text = "Connected";
				AttachedText.color = Color.green;
			}
			magnitude = 0;
			for (int i = 0; i < device.axes.Count; i++) {
				_acceleration[i] = device.axes[i].Acceleration;
				magnitude += _acceleration[i] * _acceleration[i];
			}
			magnitude = Math.Sqrt(magnitude);
			for (int i = 0; i < 3; i++) {
				double a1 = _acceleration[(i+1)%3];
				double a2 = _acceleration[(i+2)%3];
				_tilt[i] = Math.Atan2(a2,a1) * 180 / Math.PI;
			}
			if (AccelerationText) {
				AccelerationText.text = "ACCELERATION\n\n";
				for (int i = 0; i < _acceleration.Length; i++) {
						AccelerationText.text += string.Format ("Axis {0}: {1:n6}\n", i, _acceleration[i]);
				}
				AccelerationText.text += string.Format ("Magnitude: {0:n6}\n", magnitude);
			}
			if (TiltText) {
				TiltText.text = "TILT\n\n";
				for (int i = 0; i < _tilt.Length; i++) {
					TiltText.text += string.Format ("Axis {0}: {1:n1}\n", i, _tilt[i]);
				}
			}
			timestamp = Time.realtimeSinceStartup;
			if (TimestampText) {
				TimestampText.text = string.Format ("Timestamp: {0}", timestamp);
			}
		} else {
			if (AttachedText){
				AttachedText.text = "Disconnected";
				AttachedText.color = Color.red;
			}
		}
	}

	void OnDestroy() {
		device.close();
		device = null;
	}
}
