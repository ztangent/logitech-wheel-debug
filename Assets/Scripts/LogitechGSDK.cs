using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System;

public class LogitechGSDK  {
	//LED SDK
	public const int LOGITECH_LED_MOUSE = 0x0001;
	public const int LOGITECH_LED_KEYBOARD = 0x0002;
	public const int LOGITECH_LED_ALL = LOGITECH_LED_MOUSE | LOGITECH_LED_KEYBOARD;
	
	[DllImport("LogitechLed", CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiLedInit();
		
	[DllImport("LogitechLed", CallingConvention = CallingConvention.Cdecl)]
	public static extern int LogiLedSaveCurrentLighting(int deviceType);
	
	[DllImport("LogitechLed", CallingConvention = CallingConvention.Cdecl)]
	public static extern int LogiLedSetLighting(int deviceType, int redPercentage, int greenPercentage, int bluePercentage);
	
	[DllImport("LogitechLed", CallingConvention = CallingConvention.Cdecl)]
	public static extern int LogiLedFlashLighting(int deviceType, int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);	
	
	[DllImport("LogitechLed", CallingConvention = CallingConvention.Cdecl)]
	public static extern int LogiLedPulseLighting(int deviceType, int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration);	

	[DllImport("LogitechLed", CallingConvention = CallingConvention.Cdecl)]
	public static extern int LogiLedRestoreLighting(int deviceType);
	
	[DllImport("LogitechLed", CallingConvention = CallingConvention.Cdecl)]
    public static extern void LogiLedShutdown();
	
	//END OF LED SDK
	
	
	//LCD SDK
	public const int LOGI_LCD_COLOR_BUTTON_LEFT   = (0x00000100);
	public const int LOGI_LCD_COLOR_BUTTON_RIGHT  = (0x00000200);
	public const int LOGI_LCD_COLOR_BUTTON_OK     = (0x00000400);
	public const int LOGI_LCD_COLOR_BUTTON_CANCEL = (0x00000800);
	public const int LOGI_LCD_COLOR_BUTTON_UP 	  = (0x00001000);
	public const int LOGI_LCD_COLOR_BUTTON_DOWN   = (0x00002000);
	public const int LOGI_LCD_COLOR_BUTTON_MENU   = (0x00004000);
	
	public const int LOGI_LCD_MONO_BUTTON_0 = (0x00000001);
	public const int LOGI_LCD_MONO_BUTTON_1 = (0x00000002);
	public const int LOGI_LCD_MONO_BUTTON_2 = (0x00000004);
	public const int LOGI_LCD_MONO_BUTTON_3 = (0x00000008);

	public const int LOGI_LCD_MONO_WIDTH = 160;
	public const int LOGI_LCD_MONO_HEIGHT = 43;

	public const int LOGI_LCD_COLOR_WIDTH = 320;
	public const int LOGI_LCD_COLOR_HEIGHT = 240;
	
	
	public const int LOGI_LCD_TYPE_MONO  = (0x00000001);
	public const int LOGI_LCD_TYPE_COLOR  = (0x00000002);



	[DllImport("LogitechLcd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern bool LogiLcdInit(String friendlyName, int lcdType);
		
	[DllImport("LogitechLcd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern bool LogiLcdIsConnected(int lcdType);
	
	[DllImport("LogitechLcd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern bool LogiLcdIsButtonPressed(int button);
	
	[DllImport("LogitechLcd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern void LogiLcdUpdate();
	
	[DllImport("LogitechLcd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern void LogiLcdShutdown();
	
	// Monochrome LCD functions
	[DllImport("LogitechLcd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern bool LogiLcdMonoSetBackground(byte [] monoBitmap);
	
	[DllImport("LogitechLcd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern bool LogiLcdMonoSetText(int lineNumber, String text);
	
	// Color LCD functions
	[DllImport("LogitechLcd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern bool LogiLcdColorSetBackground(byte [] colorBitmap);
	
	[DllImport("LogitechLcd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern bool LogiLcdColorSetTitle(String text, int red , int green , int blue );
	
	[DllImport("LogitechLcd", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern bool LogiLcdColorSetText(int lineNumber, String text, int red, int green, int blue);
	
	//END OF LCD SDK
	
	//G-KEY SDK
	
	public const int LOGITECH_MAX_MOUSE_BUTTONS = 20;
	public const int LOGITECH_MAX_GKEYS = 29;
	public const int LOGITECH_MAX_M_STATES = 3;

	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	public struct GkeyCode
	{
		public ushort complete;
		// index of the G key or mouse button, for example, 6 for G6 or Button 6
    	public int keyIdx{
			get{
				return complete & 255;
			}
		}
		// key up or down, 1 is down, 0 is up
		public int keyDown{
			get{
				return (complete >> 8) & 1;
			}
		}
		// mState (1, 2 or 3 for M1, M2 and M3)
		public int mState{
			get{
				return (complete >> 9) & 3;
			}
		}
		// indicate if the Event comes from a mouse, 1 is yes, 0 is no.
		public int mouse{
			get{
				return (complete >> 11) & 15;
			}
		}
		// reserved1
		public int reserved1{
			get{
				return (complete >> 15) & 1;
			}
		}
        // reserved2
		public int reserved2{
			get{
				return (complete >> 16) & 131071;
			}
		}
	}
	
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void logiGkeyCB(GkeyCode gkeyCode, [MarshalAs(UnmanagedType.LPWStr)]String gkeyOrButtonString, IntPtr context); // ??
	
	[DllImport("LogitechGKey", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern int LogiGkeyInitWithoutCallback();
	
	[DllImport("LogitechGKey", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern int LogiGkeyInitWithoutContext(logiGkeyCB gkeyCB);
	
	[DllImport("LogitechGKey", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern int LogiGkeyIsMouseButtonPressed(int buttonNumber);	
	
	[DllImport("LogitechGKey", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr LogiGkeyGetMouseButtonString(int buttonNumber);
	
	public static String LogiGkeyGetMouseButtonStr(int buttonNumber){
		String str = Marshal.PtrToStringUni(LogiGkeyGetMouseButtonString(buttonNumber));
		return str;
	}
	
	[DllImport("LogitechGKey", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern int LogiGkeyIsKeyboardGkeyPressed(int gkeyNumber, int modeNumber);
	
	[DllImport("LogitechGKey")]
	private static extern IntPtr  LogiGkeyGetKeyboardGkeyString(int gkeyNumber, int modeNumber);
	
	public static String LogiGkeyGetKeyboardGkeyStr(int gkeyNumber, int modeNumber){
		String str = Marshal.PtrToStringUni(LogiGkeyGetKeyboardGkeyString(gkeyNumber, modeNumber));
		return str;
	}
	
	[DllImport("LogitechGKey", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern void LogiGkeyShutdown();
	
	//STEERING WHEEL SDK
	public const int LOGI_MAX_CONTROLLERS				= 2;

	//Force types
	
	public const int LOGI_FORCE_NONE					= -1;
	public const int LOGI_FORCE_SPRING					= 0;
	public const int LOGI_FORCE_CONSTANT				= 1;
	public const int LOGI_FORCE_DAMPER					= 2;
	public const int LOGI_FORCE_SIDE_COLLISION			= 3;
	public const int LOGI_FORCE_FRONTAL_COLLISION		= 4;
	public const int LOGI_FORCE_DIRT_ROAD				= 5;
	public const int LOGI_FORCE_BUMPY_ROAD				= 6;
	public const int LOGI_FORCE_SLIPPERY_ROAD			= 7;
	public const int LOGI_FORCE_SURFACE_EFFECT			= 8;
	public const int LOGI_NUMBER_FORCE_EFFECTS			= 9;
	public const int LOGI_FORCE_SOFTSTOP				= 10;
	public const int LOGI_FORCE_CAR_AIRBORNE			= 11;
	
	
	//Periodic types  for surface effect
	
	public const int LOGI_PERIODICTYPE_NONE			= -1;
	public const int LOGI_PERIODICTYPE_SINE			= 0;
	public const int LOGI_PERIODICTYPE_SQUARE			= 1;
	public const int LOGI_PERIODICTYPE_TRIANGLE		= 2;
	
	
	//Devices types
	
	public const int LOGI_DEVICE_TYPE_NONE				= -1;
	public const int LOGI_DEVICE_TYPE_WHEEL			= 0;
	public const int LOGI_DEVICE_TYPE_JOYSTICK			= 1;
	public const int LOGI_DEVICE_TYPE_GAMEPAD			= 2;
	public const int LOGI_DEVICE_TYPE_OTHER			= 3;
	public const int LOGI_NUMBER_DEVICE_TYPES			= 4;
	
	//Manufacturer types
	
	public const int LOGI_MANUFACTURER_NONE			= -1;
	public const int LOGI_MANUFACTURER_LOGITECH		= 0;
	public const int LOGI_MANUFACTURER_MICROSOFT		= 1;
	public const int LOGI_MANUFACTURER_OTHER			= 2;
	
	
	//Model types
	
	public const int LOGI_MODEL_G27					= 0;
	public const int LOGI_MODEL_DRIVING_FORCE_GT		= 1;
	public const int LOGI_MODEL_G25					= 2;
	public const int LOGI_MODEL_MOMO_RACING			= 3;
	public const int LOGI_MODEL_MOMO_FORCE				= 4;
	public const int LOGI_MODEL_DRIVING_FORCE_PRO		= 5;
	public const int LOGI_MODEL_DRIVING_FORCE			= 6;
	public const int LOGI_MODEL_NASCAR_RACING_WHEEL	= 7;
	public const int LOGI_MODEL_FORMULA_FORCE			= 8;
	public const int LOGI_MODEL_FORMULA_FORCE_GP		= 9;
	public const int LOGI_MODEL_FORCE_3D_PRO			= 10;
	public const int LOGI_MODEL_EXTREME_3D_PRO			= 11;
	public const int LOGI_MODEL_FREEDOM_24				= 12;
	public const int LOGI_MODEL_ATTACK_3				= 13;
	public const int LOGI_MODEL_FORCE_3D				= 14;
	public const int LOGI_MODEL_STRIKE_FORCE_3D		= 15;
	public const int LOGI_MODEL_G940_JOYSTICK			= 16;
	public const int LOGI_MODEL_G940_THROTTLE			= 17;
	public const int LOGI_MODEL_G940_PEDALS			= 18;
	public const int LOGI_MODEL_RUMBLEPAD				= 19;
	public const int LOGI_MODEL_RUMBLEPAD_2			= 20;
	public const int LOGI_MODEL_CORDLESS_RUMBLEPAD_2	= 21;
	public const int LOGI_MODEL_CORDLESS_GAMEPAD		= 22;
	public const int LOGI_MODEL_DUAL_ACTION_GAMEPAD	= 23;
	public const int LOGI_MODEL_PRECISION_GAMEPAD_2	= 24;
	public const int LOGI_MODEL_CHILLSTREAM			= 25;
	public const int LOGI_NUMBER_MODELS				= 26;
	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	public struct LogiControllerPropertiesData
	{
    	public bool forceEnable;
    	public int overallGain;
    	public int springGain;
        public int damperGain;
        public bool defaultSpringEnabled;
        public int defaultSpringGain;
        public bool combinePedals;
        public int wheelRange;
        public bool gameSettingsEnabled;
        public bool allowGameSettings;
	}
	

    [StructLayout(LayoutKind.Sequential, Pack=2)]
    public struct  DIJOYSTATE2ENGINES {
        public int				lX;                     /* x-axis position              */
        public int				lY;                     /* y-axis position              */
        public int				lZ;                     /* z-axis position              */
        public int				lRx;                    /* x-axis rotation              */
        public int				lRy;                    /* y-axis rotation              */
        public int				lRz;                    /* z-axis rotation              */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int			[]	rglSlider;              /* extra axes positions         */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint	[]rgdwPOV;                          /* POV directions               */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte  [] rgbButtons;                     /* 128 buttons                  */
        public int				lVX;                    /* x-axis velocity              */
        public int				lVY;                    /* y-axis velocity              */
        public int				lVZ;                    /* z-axis velocity              */
        public int				lVRx;                   /* x-axis angular velocity      */
        public int				lVRy;                   /* y-axis angular velocity      */
        public int				lVRz;                   /* z-axis angular velocity      */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public  int			[]	rglVSlider;             /* extra axes velocities        */
        public int				lAX;                    /* x-axis acceleration          */
        public int				lAY;                    /* y-axis acceleration          */
        public int				lAZ;                    /* z-axis acceleration          */
        public int				lARx;                   /* x-axis angular acceleration  */
        public int				lARy;                   /* y-axis angular acceleration  */

        public int				lARz;                   /* z-axis angular acceleration  */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public  int			[]	rglASlider;             /* extra axes accelerations     */
        public int				lFX;                    /* x-axis force                 */
        public int				lFY;                    /* y-axis force                 */
        public int				lFZ;                    /* z-axis force                 */
        public int				lFRx;                   /* x-axis torque                */
        public int				lFRy;                   /* y-axis torque                */
        public int				lFRz;                   /* z-axis torque                */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] rglFSlider;                        /* extra axes forces            */
    };

	[DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern bool LogiSteeringInitialize(bool ignoreXInputControllers);
	
	[DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
	public static extern bool LogiUpdate();
	
	[DllImport("LogitechSteeringWheel", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr LogiGetStateENGINES(int index);

    public static DIJOYSTATE2ENGINES LogiGetStateUnity(int index)
    {
        DIJOYSTATE2ENGINES ret = new DIJOYSTATE2ENGINES();
        ret.rglSlider = new int[2];
        ret.rgdwPOV = new uint[4];
        ret.rgbButtons = new byte[128];
        ret.rglVSlider = new int[2];
        ret.rglASlider = new int[2];
        ret.rglFSlider = new int[2];
        try
        {
            ret = (DIJOYSTATE2ENGINES)Marshal.PtrToStructure(LogiGetStateENGINES(index), typeof(DIJOYSTATE2ENGINES));
        }
        catch (System.ArgumentException)
        {
            Debug.Log("Exception catched");
        }
            return ret;
	}

	public static float LogiSteeringGetAngle(int index)
	{
		DIJOYSTATE2ENGINES state = LogiGetStateUnity(index);
		LogitechGSDK.LogiControllerPropertiesData properties = new LogitechGSDK.LogiControllerPropertiesData();
		LogitechGSDK.LogiGetCurrentControllerProperties(index, ref properties);
		float angle = (float)state.lX / 65536 * properties.wheelRange;
		return angle;
	}

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr LogiGetFriendlyProductName(int index);

    public static String LogiSteeringGetFriendlyProductName(int index)
    {
        String str = Marshal.PtrToStringAnsi(LogiGetFriendlyProductName(index));
        return str;
    }

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiIsConnected(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiIsDeviceConnected(int index, int deviceType);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiIsManufacturerConnected(int index, int manufacturerName);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiIsModelConnected(int index, int modelName);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiButtonTriggered(int index, int buttonNbr);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiButtonReleased(int index, int buttonNbr);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiButtonIsPressed(int index, int buttonNbr);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiGenerateNonLinearValues(int index, int nonLinCoeff);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiGetNonLinearValue(int index, int inputValue);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiHasForceFeedback(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiIsPlaying(int index, int forceType);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlaySpringForce(int index, int offsetPercentage, int saturationPercentage, int coefficientPercentage);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopSpringForce(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayConstantForce(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopConstantForce(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayDamperForce(int index, int coefficientPercentage);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopDamperForce(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlaySideCollisionForce(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayFrontalCollisionForce(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayDirtRoadEffect(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopDirtRoadEffect(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayBumpyRoadEffect(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopBumpyRoadEffect(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlaySlipperyRoadEffect(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopSlipperyRoadEffect(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlaySurfaceEffect(int index, int type, int magnitudePercentage, int period);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopSurfaceEffect(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayCarAirborne(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopCarAirborne(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlaySoftstopForce(int index, int usableRangePercentage);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopSoftstopForce(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiSetPreferredControllerProperties(LogiControllerPropertiesData properties);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiGetCurrentControllerProperties(int index, ref LogiControllerPropertiesData properties);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiGetShifterMode(int index);

    [DllImport("LogitechSteeringWheel", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayLeds(int index, float currentRPM, float rpmFirstLedTurnsOn, float rpmRedLine);
}
