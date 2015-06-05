using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogitechSteeringWheel : MonoBehaviour {

    LogitechGSDK.LogiControllerPropertiesData properties;
	public int index;

	public float timestamp { get; private set; }
	public float wheelAngle { get; private set; }
	public LogitechGSDK.DIJOYSTATE2ENGINES rec;

	public Text connectionStatus;
	public Text timestampText;
    public Text actualState;
    public Text activeForces;
    public Text propertiesEdit;
    public Text buttonStatus;
    public Text forcesLabel;
    string[] activeForceAndEffect;

    [Header("Wheel Settings")]
    public bool doUpdate;
    public int wheelRange = 900;
    public bool forceEnable = true;
    public int overallGain = 20;
    public int springGain = 20;
    public int damperGain = 20;
    public bool allowGameSettings = true;
    public bool combinePedals = false;
    public bool defaultSpringEnabled = true;
    public int defaultSpringGain = 20;

	// Use this for initialization
	void Start () {
		if (forcesLabel){
	        forcesLabel.text = "KEYS TO ACTIVATE FORCES \n\n";
	        forcesLabel.text += "Spring force : S\n";
	        forcesLabel.text += "Constant force : C\n";
	        forcesLabel.text += "Damper force : D\n";
	        forcesLabel.text += "Side collision : Left or Right Arrow\n";
	        forcesLabel.text += "Front collision : Up arrow\n";
	        forcesLabel.text += "Dirt road effect : I\n";
	        forcesLabel.text += "Bumpy road effect : B\n";
	        forcesLabel.text += "Slippery road effect : L\n";
	        forcesLabel.text += "Surface effect : U\n";
	        forcesLabel.text += "Car Airborne effect : A\n";
	        forcesLabel.text += "Soft Stop Force : O\n";
	        forcesLabel.text += "Set example controller properties : PageUp\n";
	        forcesLabel.text += "Play Leds : P\n";
		}
        activeForceAndEffect = new string[9];
		LogitechGSDK.LogiSteeringInitialize(false);
    
		DisableFeedback ();
	}

    void DisableFeedback()
    {
        LogitechGSDK.LogiStopSpringForce(index);
        LogitechGSDK.LogiStopConstantForce(index);
        LogitechGSDK.LogiStopDamperForce(index);
        LogitechGSDK.LogiStopDirtRoadEffect(index);
        LogitechGSDK.LogiStopBumpyRoadEffect(index);
        LogitechGSDK.LogiStopSlipperyRoadEffect(index);
        LogitechGSDK.LogiStopSurfaceEffect(index);
        LogitechGSDK.LogiStopCarAirborne(index);
        LogitechGSDK.LogiStopSoftstopForce(index);

        properties.wheelRange = 90;
        properties.forceEnable = true;
        properties.overallGain = 80;
        properties.springGain = 80;
        properties.damperGain = 80;
        properties.allowGameSettings = false;
        properties.combinePedals = false;
        properties.defaultSpringEnabled = false;
        properties.defaultSpringGain = 80;
        LogitechGSDK.LogiSetPreferredControllerProperties(properties);
    }

	// Update is called once per frame
	void Update () {
		//All the test functions are called on the first device plugged in(index = 0)
		if(LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(index)){
			if (connectionStatus){
				connectionStatus.text = "Connected";
				connectionStatus.color = Color.green;
			}

            //CONTROLLER PROPERTIES
			if (propertiesEdit) {
				LogitechGSDK.LogiControllerPropertiesData actualProperties = new LogitechGSDK.LogiControllerPropertiesData();
				LogitechGSDK.LogiGetCurrentControllerProperties(index, ref actualProperties);

				propertiesEdit.text = "Current Controller : "+LogitechGSDK.LogiSteeringGetFriendlyProductName(index)+"\n";
	            propertiesEdit.text += "Current controller properties : \n\n";
	            propertiesEdit.text += "forceEnable = " + actualProperties.forceEnable + "\n";
	            propertiesEdit.text += "overallGain = " + actualProperties.overallGain + "\n";
	            propertiesEdit.text += "springGain = " + actualProperties.springGain + "\n";
	            propertiesEdit.text += "damperGain = " + actualProperties.damperGain + "\n";
	            propertiesEdit.text += "defaultSpringEnabled = " + actualProperties.defaultSpringEnabled + "\n";
	            propertiesEdit.text += "combinePedals = " + actualProperties.combinePedals + "\n";
	            propertiesEdit.text += "wheelRange = " + actualProperties.wheelRange + "\n";
	            propertiesEdit.text += "gameSettingsEnabled = " + actualProperties.gameSettingsEnabled + "\n";
	            propertiesEdit.text += "allowGameSettings = " + actualProperties.allowGameSettings + "\n";
			}

			timestamp = Time.realtimeSinceStartup;
			if (timestampText) {
				timestampText.text = string.Format("Timestamp: {0}", timestamp);
			}

			rec = LogitechGSDK.LogiGetStateUnity(index);
			wheelAngle = LogitechGSDK.LogiSteeringGetAngle(index);

			//CONTROLLER STATE
			if (actualState) {
				actualState.text = "CURRENT STATE \n\n";
				actualState.text += "wheel angle:" + wheelAngle + "\n";
	            actualState.text += "x-axis position :" + rec.lX + "\n";
	            actualState.text += "y-axis position :" + rec.lY + "\n";
	            actualState.text += "z-axis position :" + rec.lZ + "\n";
	            actualState.text += "x-axis rotation :" + rec.lRx + "\n";
	            actualState.text += "y-axis rotation :" + rec.lRy + "\n";
	            actualState.text += "z-axis rotation :" + rec.lRz + "\n";
	            actualState.text += "extra axes positions 1 :" + rec.rglSlider[0] + "\n";
	            actualState.text += "extra axes positions 2 :" + rec.rglSlider[1] + "\n";
	            switch (rec.rgdwPOV[0])
	            {
	                case (0): actualState.text += "POV : UP\n"; break;
	                case (4500): actualState.text += "POV : UP-RIGHT\n"; break;
	                case (9000): actualState.text += "POV : RIGHT\n"; break;
	                case (13500): actualState.text += "POV : DOWN-RIGHT\n"; break;
	                case (18000): actualState.text += "POV : DOWN\n"; break;
	                case (22500): actualState.text += "POV : DOWN-LEFT\n"; break;
	                case (27000): actualState.text += "POV : LEFT\n"; break;
	                case (31500): actualState.text += "POV : UP-LEFT\n"; break;
	                default: actualState.text += "POV : CENTER\n"; break;
	            }
				int shifterTipe = LogitechGSDK.LogiGetShifterMode(index);
				string shifterString = "";
				if (shifterTipe == 1) shifterString = "Gated";
				else if (shifterTipe == 0) shifterString = "Sequential";
				else  shifterString = "Unknown";
				actualState.text += "\nSHIFTER MODE:" + shifterString;
			}
			
			//Button status :
			if (buttonStatus) {
	            buttonStatus.text = "BUTTONS PRESSED \n\n";
	            for (int i = 0; i < 128; i++)
	            {
	                if (rec.rgbButtons[i] == 128)
	                {
	                    buttonStatus.text += "Button " + i + " pressed\n";
	                }

	            }
			}

            // FORCES AND EFFECTS 
			if (activeForces){
	            activeForces.text = "ACTIVE FORCES AND EFFECTS\n\n";
			}

            //Spring Force -> S
            if (Input.GetKeyUp(KeyCode.S)){
               if (LogitechGSDK.LogiIsPlaying(index, LogitechGSDK.LOGI_FORCE_SPRING))
               {
                   LogitechGSDK.LogiStopSpringForce(index);
                   activeForceAndEffect[0] = "";
               }
               else
               {
                   LogitechGSDK.LogiPlaySpringForce(index, 50, 50, 50);
                   activeForceAndEffect[0] = "Spring Force\n ";
               }
            }

            //Constant Force -> C
            if (Input.GetKeyUp(KeyCode.C))
            {
                if (LogitechGSDK.LogiIsPlaying(index, LogitechGSDK.LOGI_FORCE_CONSTANT))
                {
                    LogitechGSDK.LogiStopConstantForce(index);
                    activeForceAndEffect[1] = "";
                }
                else
                {
                    LogitechGSDK.LogiPlayConstantForce(index, 50);
                    activeForceAndEffect[1] = "Constant Force\n ";
                }
            }

            //Damper Force -> D
            if (Input.GetKeyUp(KeyCode.D))
            {
                if (LogitechGSDK.LogiIsPlaying(index, LogitechGSDK.LOGI_FORCE_DAMPER))
                {
                    LogitechGSDK.LogiStopDamperForce(index);
                    activeForceAndEffect[2] = "";
                }
                else
                {
                    LogitechGSDK.LogiPlayDamperForce(index, 50);
                    activeForceAndEffect[2] = "Damper Force\n ";
                }
            }

            //Side Collision Force -> left or right arrow
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
				if(Input.GetKeyUp(KeyCode.LeftArrow)) {
					LogitechGSDK.LogiPlaySideCollisionForce(index, 10);
				} else if(Input.GetKey(KeyCode.RightArrow)) {
					LogitechGSDK.LogiPlaySideCollisionForce(index, -10);
				}

            }

            //Front Collision Force -> up arrow
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                LogitechGSDK.LogiPlayFrontalCollisionForce(index, 60);
            }

            //Dirt Road Effect-> I
            if (Input.GetKeyUp(KeyCode.I))
            {
                if (LogitechGSDK.LogiIsPlaying(index, LogitechGSDK.LOGI_FORCE_DIRT_ROAD))
                {
                    LogitechGSDK.LogiStopDirtRoadEffect(index);
                    activeForceAndEffect[3] = "";
                }
                else
                {
                    LogitechGSDK.LogiPlayDirtRoadEffect(index, 50);
                    activeForceAndEffect[3] = "Dirt Road Effect\n ";
                }

            }
            
            //Bumpy Road Effect-> B
            if (Input.GetKeyUp(KeyCode.B))
            {
                if (LogitechGSDK.LogiIsPlaying(index, LogitechGSDK.LOGI_FORCE_BUMPY_ROAD))
                {
                    LogitechGSDK.LogiStopBumpyRoadEffect(index);
                    activeForceAndEffect[4] = "";
                }
                else
                {
                    LogitechGSDK.LogiPlayBumpyRoadEffect(index, 50);
                    activeForceAndEffect[4] = "Bumpy Road Effect\n";
                }

            }

            //Slippery Road Effect-> L
            if (Input.GetKeyUp(KeyCode.L))
            {
                if (LogitechGSDK.LogiIsPlaying(index, LogitechGSDK.LOGI_FORCE_SLIPPERY_ROAD))
                {
                    LogitechGSDK.LogiStopSlipperyRoadEffect(index);
                    activeForceAndEffect[5] = "";
                }
                else
                {
                    LogitechGSDK.LogiPlaySlipperyRoadEffect(index, 50);
                    activeForceAndEffect[5] = "Slippery Road Effect\n ";
                }
            }

            //Surface Effect-> U
            if (Input.GetKeyUp(KeyCode.U))
            {
                if (LogitechGSDK.LogiIsPlaying(index, LogitechGSDK.LOGI_FORCE_SURFACE_EFFECT))
                {
                    LogitechGSDK.LogiStopSurfaceEffect(index);
                    activeForceAndEffect[6] = "";
                }
                else
                {
                    LogitechGSDK.LogiPlaySurfaceEffect(index, LogitechGSDK.LOGI_PERIODICTYPE_SQUARE, 50, 1000);
                    activeForceAndEffect[6] = "Surface Effect\n";
                }
            }

            //Car Airborne -> A
            if (Input.GetKeyUp(KeyCode.A))
            {
                if (LogitechGSDK.LogiIsPlaying(index, LogitechGSDK.LOGI_FORCE_CAR_AIRBORNE))
                {
                    LogitechGSDK.LogiStopCarAirborne(index);
                    activeForceAndEffect[7] = "";
                }
                else
                {
                    LogitechGSDK.LogiPlayCarAirborne(index);
                    activeForceAndEffect[7] = "Car Airborne\n ";
                }
            }

            //Soft Stop Force -> O
            if (Input.GetKeyUp(KeyCode.O))
            {
                if (LogitechGSDK.LogiIsPlaying(index, LogitechGSDK.LOGI_FORCE_SOFTSTOP))
                {
                    LogitechGSDK.LogiStopSoftstopForce(index);
                    activeForceAndEffect[8] = "";
                }
                else
                {
                    LogitechGSDK.LogiPlaySoftstopForce(index, 20);
                    activeForceAndEffect[8] = "Soft Stop Force\n";
                }
            }

            //Set preferred controller properties -> PageUp
            if (Input.GetKeyUp(KeyCode.PageUp))
            {
                //Setting example values
                properties.wheelRange = 90;
                properties.forceEnable = false;
                properties.overallGain = 80;
                properties.springGain = 80;
                properties.damperGain = 80;
                properties.allowGameSettings = true;
                properties.combinePedals = false;
                properties.defaultSpringEnabled = true;
                properties.defaultSpringGain = 80;
                LogitechGSDK.LogiSetPreferredControllerProperties(properties);

            }

            //Play leds -> P
            if (Input.GetKeyUp(KeyCode.P))
            {
                LogitechGSDK.LogiPlayLeds(index, 20, 20, 20);
            }

			if (activeForces) {
	            for (int i = 0; i < 9; i++)
	            {
	                activeForces.text += activeForceAndEffect[i];
	            }
			}

		}
		else if(!LogitechGSDK.LogiIsConnected(index))
		{
			if (connectionStatus) {
				connectionStatus.text = "Disconnected";
				connectionStatus.color = Color.red;
			}
			if (actualState) {
			 	actualState.text = "Please plug in a steering wheel";
			}
		}
		else{
			if (actualState){
				actualState.text = "Window needs to be in foreground for SDK to operate correctly";
			}
		}

        if (doUpdate)
        {
            properties.wheelRange = wheelRange;
            properties.forceEnable = forceEnable;
            properties.overallGain = overallGain;
            properties.springGain = springGain;
            properties.damperGain = damperGain;
            properties.allowGameSettings = allowGameSettings;
            properties.combinePedals = combinePedals;
            properties.defaultSpringEnabled = defaultSpringEnabled;
            properties.defaultSpringGain = defaultSpringGain;
            LogitechGSDK.LogiSetPreferredControllerProperties(properties);
            doUpdate = false;
        }
	}

    

}
