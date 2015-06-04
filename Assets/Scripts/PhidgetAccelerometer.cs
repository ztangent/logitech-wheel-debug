using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Phidgets;
using Phidgets.Events;

public class PhidgetAccelerometer : MonoBehaviour {

	public Text AttachedText;
	public Text AccelerationText;
	public Text TimestampText;

	public double[] acceleration = new double[3];
	public long timestamp { get; private set; }

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
			for (int i = 0; i < device.axes.Count; i++) {
				acceleration[i] = device.axes[i].Acceleration;
			}
			if (AccelerationText) {
				AccelerationText.text = "ACCELERATION\n\n";
				for (int i = 0; i < 3; i++) {
						AccelerationText.text += string.Format ("Axis {0}: {1}\n", i, acceleration [i]);
				}
			}
			timestamp = System.DateTime.Now.TimeOfDay.Ticks;
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
