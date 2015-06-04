using UnityEngine;
using System.IO;
using System.Collections;

public class Logger : MonoBehaviour {

	public bool IsLogging;
	public bool PrintToConsole;
	public bool PrintToFile;
	public string LogFile;
	public KeyCode LogKey;
	public LogitechSteeringWheel SteeringWheel;
	public PhidgetAccelerometer Accelerometer;

	private TextWriter Writer;
	private static string header = 	string.Format ("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\n",
       "WheelTime",
       "AccelTime",
       "WheelAngle",
       "WheelX",
       "AccelAxis0",
       "AccelAxis1",
       "AccelAxis2");

	// Use this for initialization
	void Start () {
		if (!File.Exists(LogFile)){
			try {
				Writer = File.CreateText(LogFile);
			} catch {
				Debug.LogError ("Could not create log file!");
				PrintToFile = false;
			}
		} else {
			Writer = File.AppendText(LogFile);
		}
	}

	void Update () {
		if (Input.GetKeyUp (LogKey)) {
			IsLogging = !IsLogging;
			if (IsLogging){
				Print("Started logging");
				Print(header);
			} else {
				Print("Stopped logging");
			}
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		if (IsLogging) {
			string line = string.Format ("{0}\t{1}\t{2:0.000000}\t{3:00000}\t{4:0.000000}\t{5:0.000000}\t{6:0.000000}",
	            SteeringWheel.timestamp,
	            Accelerometer.timestamp,
	            SteeringWheel.wheelAngle,
	            SteeringWheel.rec.lX,
	            Accelerometer.acceleration [0],
	            Accelerometer.acceleration [1],
	            Accelerometer.acceleration [2]);
			Print(line);
		}
	}

	void OnDestroy () {
		if (Writer != null) {
			Writer.Close();
		}
	}

	void Print(string line) {
		if (PrintToConsole){
			Debug.Log(line);
		}
		if (PrintToFile){
			Writer.WriteLine(line);
		}
	}
}
