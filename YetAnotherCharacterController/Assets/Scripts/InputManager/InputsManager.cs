using UnityEngine;
using System.Collections;

public class InputsManager : SingleInputsManager {
    bool isLastDPADXDown = false;
    bool isLastDPADYDown = false;

    public enum DebugMode {
        NONE,
        AXIS,
        KEY,
        KEYHELD,
        KEYRELEASED,
        DPAD
    }

    [SerializeField] DebugMode debugMode = new DebugMode();

	public float GetAxis(string value) {
		switch (value) {
			case Keys.MOUSEX:
			case Keys.MOUSEY:
			case Keys.VERTICAL:
			case Keys.HORIZONTAL:
			case Keys.FIRE1:
				return Input.GetAxis(value);
			default:
				Debug.Log("Key not found : " + value);
				return 0;
		}
	}

    public float GetAxisPad(string value) {
        switch (value) {  
            case Keys.VERTICAL:
            case Keys.HORIZONTAL:
            case Keys.MOUSEX:
            case Keys.MOUSEY:
            case Keys.DPADX_AXIS:
            case Keys.DPADY_AXIS:
                return Input.GetAxis(value);
            case Keys.LT_AXIS:
            case Keys.RT_AXIS:
            case Keys.FIRE1:
                return Input.GetAxisRaw(value);
            default:
                Debug.Log("Key not found : " + value);
                return 0;
        }
    }

    public float GetDPADDownPad(string value) {
        switch (value) {
            case Keys.DPADX_AXIS:
                float valueXAxis = 0;

                if (!this.isLastDPADXDown) {
                    if ((valueXAxis = this.GetAxisPad(value)) != 0) {
                        this.isLastDPADXDown = true;
                    }
                }
                return valueXAxis;
            case Keys.DPADY_AXIS:
                float valueYAxis = 0;

                if (!this.isLastDPADYDown) {
                    if ((valueYAxis = this.GetAxisPad(value)) != 0) {
                        this.isLastDPADYDown = true;
                    }
                }
                return valueYAxis;
            default:
                Debug.Log("GetDPADDown : key not found : " + value);
                return 0;
        }
    }

	public bool GetKey(string value) {
		switch (value) {
			case Keys.VERTICAL:
			case Keys.HORIZONTAL:
			case Keys.FIRE1:
			case Keys.FIRE2:
			case Keys.FIRE3:
			case Keys.JUMP:
				return Input.GetButtonDown(value);
			default:
				Debug.Log("Keys not found : " + value);
				return false;
		}
	}

	public bool GetKeyPad(string value) {
        switch (value) {
            case Keys.A_KEY:
            case Keys.B_KEY:
            case Keys.X_KEY:
            case Keys.Y_KEY:
            case Keys.LJOY_KEY:
            case Keys.RJOY_KEY:
            case Keys.START_KEY:
            case Keys.SELECT_KEY:
            case Keys.LB_KEY:
            case Keys.RB_KEY:
			case Keys.FIRE1:
			case Keys.FIRE2:
			case Keys.FIRE3:
			case Keys.JUMP:
                return Input.GetButtonDown(value);
            default:
                Debug.Log("Keys not found : " + value);
                return false;
        }
    }

    public bool GetKeyHeld(string value) {
        switch (value) {

			case Keys.VERTICAL:
			case Keys.HORIZONTAL:
			case Keys.FIRE1:
			case Keys.FIRE2:
			case Keys.FIRE3:
			case Keys.JUMP:
				return Input.GetButton(value);
            default:
                Debug.Log("Keys not found : " + value);
                return false;
        }
    }

	public bool GetKeyHeldPad(string value) {
		switch (value) {
			case Keys.A_KEY:
			case Keys.B_KEY:
			case Keys.X_KEY:
			case Keys.Y_KEY:
			case Keys.LJOY_KEY:
			case Keys.RJOY_KEY:
			case Keys.START_KEY:
			case Keys.SELECT_KEY:
			case Keys.LB_KEY:
			case Keys.RB_KEY:
				return Input.GetButton(value);
			default:
				Debug.Log("Keys not found : " + value);
				return false;
		}
	}

	public bool GetKeyReleasedPad(string value) {
        switch (value) {
            case Keys.A_KEY:
            case Keys.B_KEY:
            case Keys.X_KEY:
            case Keys.Y_KEY:
            case Keys.LJOY_KEY:
            case Keys.RJOY_KEY:
            case Keys.START_KEY:
            case Keys.SELECT_KEY:
            case Keys.LB_KEY:
            case Keys.RB_KEY:
				return Input.GetButtonUp(value);
            default:
                Debug.Log("Keys not found : " + value);
                return false;
        }
    }

	public bool GetKeyReleased(string value) {
		switch (value) {

			case Keys.VERTICAL:
			case Keys.HORIZONTAL:
			case Keys.FIRE1:
			case Keys.FIRE2:
			case Keys.FIRE3:
			case Keys.JUMP:
				return Input.GetButtonUp(value);
			default:
				Debug.Log("Keys not found : " + value);
				return false;
		}
	}

	void Update() {
        this.SwitchDebugMode();
		/*
        if (this.isLastDPADXDown && Input.GetAxisRaw(Keys.DPADX_AXIS) == 0) {
            this.isLastDPADXDown = false;
        }

        if (this.isLastDPADYDown && Input.GetAxisRaw(Keys.DPADY_AXIS) == 0) {
            this.isLastDPADYDown = false;
        }
		*/
    }

	public void SwitchDebugMode() {
		switch (debugMode) {
			case DebugMode.NONE:
				break;
			case DebugMode.AXIS:
				this.DebugAxis();
				break;
			case DebugMode.KEY:
				this.DebugKey();
				break;
			case DebugMode.KEYHELD:
				this.DebugKeyHeld();
				break;
			case DebugMode.KEYRELEASED:
				this.DebugKeyReleased();
				break;
			case DebugMode.DPAD:
				//this.DebugDPADPad();
				break;
			default:
				break;
		}
	}

	/*
    public void SwitchDebugModePad() {
        switch (debugMode) {
            case DebugMode.NONE:
                break;
            case DebugMode.AXIS:
                this.DebugAxis();
                break;
            case DebugMode.KEY:
                this.DebugKey();
                break;
            case DebugMode.KEYHELD:
                this.DebugKeyHeld();
                break;
            case DebugMode.KEYRELEASED:
                this.DebugKeyReleased();
                break;
            case DebugMode.DPAD:
                //this.DebugDPADPad();
                break;
            default:
                break;
        }
    }
	*/

	/*
    public void DebugDPAD() {
        float floatValue;

        if ((floatValue = this.GetDPADDown(Keys.DPADY_AXIS)) == Keys.DPAD_BOTTOM) {
            Debug.Log(this.GetDPADDown(Keys.DPADY_AXIS) + "| bottom : " + Keys.DPAD_BOTTOM);

        } else if ((floatValue = this.GetDPADDown(Keys.DPADY_AXIS)) == Keys.DPAD_UP) {
            Debug.Log("DPAD UP : " + floatValue);
        }

        if ((floatValue = this.GetDPADDown(Keys.DPADY_AXIS)) == Keys.DPAD_BOTTOM) {
            Debug.Log("DPAD BOTTOM : " + floatValue);
        }

        if ((floatValue = this.GetDPADDown(Keys.DPADX_AXIS)) == Keys.DPAD_RIGHT) {
            Debug.Log("DPAD_RIGHT : " + floatValue);
        }

        if ((floatValue = this.GetDPADDown(Keys.DPADX_AXIS)) == Keys.DPAD_LEFT) {
            Debug.Log("DPAD_LEFT : " + floatValue);
        }
    }
	*/

	public void DebugAxis() {
        float floatValue;

        if ((floatValue = this.GetAxis(Keys.MOUSEX)) != 0)
            Debug.Log("Axis MOUSEX : " + floatValue);

		if ((floatValue = this.GetAxis(Keys.MOUSEY)) != 0)
			Debug.Log("Axis MOUSEY : " + floatValue);

		if ((floatValue = this.GetAxis(Keys.VERTICAL)) != 0)
			Debug.Log("VERTICAL : " + floatValue);

		if ((floatValue = this.GetAxis(Keys.HORIZONTAL)) != 0)
			Debug.Log("HORIZONTAL: " + floatValue);
	}

	public void DebugAxisPad() {
		float floatValue;

		if ((floatValue = this.GetAxis(Keys.RT_AXIS)) != 0)
			Debug.Log("Axis RT_AXIS : " + floatValue);

		if ((floatValue = this.GetAxis(Keys.LT_AXIS)) != 0)
			Debug.Log("Axis LT_AXIS : " + floatValue);

		if ((floatValue = this.GetAxis(Keys.DPADX_AXIS)) != 0)
			Debug.Log("Axis DPADX : " + floatValue);

		if ((floatValue = this.GetAxis(Keys.DPADY_AXIS)) != 0)
			Debug.Log("Axis DPADY : " + floatValue);
	}

	public void DebugKey() {
		bool boolValue;

		if ((boolValue = this.GetKey(Keys.VERTICAL)))
			Debug.Log("VERTICAL : " + boolValue);

		if ((boolValue = this.GetKey(Keys.HORIZONTAL)))
			Debug.Log("HORIZONTAL: " + boolValue);

		if ((boolValue = this.GetKey(Keys.FIRE1)))
			Debug.Log("FIRE1: " + boolValue);

		if ((boolValue = this.GetKey(Keys.FIRE2)))
			Debug.Log("FIRE2: " + boolValue);

		if ((boolValue = this.GetKey(Keys.FIRE3)))
			Debug.Log("FIRE3: " + boolValue);
	}

    public void DebugKeyPad() {
        bool boolValue;

        if ((boolValue = this.GetKeyReleasedPad(Keys.A_KEY)))
            Debug.Log("Get A_KEY : " + boolValue);
        if ((boolValue = this.GetKeyReleasedPad(Keys.B_KEY)))
            Debug.Log("Get B_KEY : " + boolValue);

        if ((boolValue = this.GetKeyReleasedPad(Keys.X_KEY)))
            Debug.Log("Get X_KEY : " + boolValue);

        if ((boolValue = this.GetKeyReleasedPad(Keys.Y_KEY)))
            Debug.Log("Get Y_KEY : " + boolValue);

        if ((boolValue = this.GetKeyReleasedPad(Keys.LB_KEY)))
            Debug.Log("Axis LB_KEY : " + boolValue);

        if ((boolValue = this.GetKeyReleasedPad(Keys.RB_KEY)))
            Debug.Log("Axis RB_KEY : " + boolValue);

        if ((boolValue = this.GetKeyReleasedPad(Keys.LJOY_KEY)))
            Debug.Log("LJOY_CLICK : " + boolValue);

        if ((boolValue = this.GetKeyReleasedPad(Keys.RJOY_KEY)))
            Debug.Log("RJOY_CLICK : " + boolValue);

        if ((boolValue = this.GetKeyReleasedPad(Keys.START_KEY)))
            Debug.Log("START_KEY : " + boolValue);

        if ((boolValue = this.GetKeyReleasedPad(Keys.SELECT_KEY)))
            Debug.Log("SELECT_KEY : " + boolValue);

	}

    public void DebugKeyHeld() {
        bool boolValue;

		if ((boolValue = this.GetKeyHeld(Keys.VERTICAL)))
			Debug.Log("VERTICAL : " + boolValue);

		if ((boolValue = this.GetKeyHeld(Keys.HORIZONTAL)))
			Debug.Log("HORIZONTAL: " + boolValue);


		if ((boolValue = this.GetKeyHeld(Keys.FIRE1)))
			Debug.Log("FIRE1: " + boolValue);

		if ((boolValue = this.GetKeyHeld(Keys.FIRE2)))
			Debug.Log("FIRE2: " + boolValue);

		if ((boolValue = this.GetKeyHeld(Keys.FIRE3)))
			Debug.Log("FIRE3: " + boolValue);
	}

	public void DebugKeyHeldPad() {
		bool boolValue;

		if ((boolValue = this.GetKeyHeldPad(Keys.A_KEY)))
			Debug.Log("Get A_KEY : " + boolValue);
		if ((boolValue = this.GetKeyHeldPad(Keys.B_KEY)))
			Debug.Log("Get B_KEY : " + boolValue);

		if ((boolValue = this.GetKeyHeldPad(Keys.X_KEY)))
			Debug.Log("Get X_KEY : " + boolValue);

		if ((boolValue = this.GetKeyHeldPad(Keys.Y_KEY)))
			Debug.Log("Get Y_KEY : " + boolValue);

		if ((boolValue = this.GetKeyHeldPad(Keys.LB_KEY)))
			Debug.Log("Axis LB_KEY : " + boolValue);

		if ((boolValue = this.GetKeyHeldPad(Keys.RB_KEY)))
			Debug.Log("Axis RB_KEY : " + boolValue);

		if ((boolValue = this.GetKeyHeldPad(Keys.LJOY_KEY)))
			Debug.Log("LJOY_CLICK : " + boolValue);

		if ((boolValue = this.GetKeyHeldPad(Keys.RJOY_KEY)))
			Debug.Log("RJOY_CLICK : " + boolValue);

		if ((boolValue = this.GetKeyHeldPad(Keys.START_KEY)))
			Debug.Log("START_KEY : " + boolValue);

		if ((boolValue = this.GetKeyHeldPad(Keys.SELECT_KEY)))
			Debug.Log("SELECT_KEY : " + boolValue);


	}

    public void DebugKeyReleased() {
        bool boolValue;


		if ((boolValue = this.GetKeyReleased(Keys.VERTICAL)))
			Debug.Log("VERTICAL : " + boolValue);

		if ((boolValue = this.GetKeyReleased(Keys.HORIZONTAL)))
			Debug.Log("HORIZONTAL: " + boolValue);

		if ((boolValue = this.GetKeyReleased(Keys.FIRE1)))
			Debug.Log("FIRE1: " + boolValue);

		if ((boolValue = this.GetKeyReleased(Keys.FIRE2)))
			Debug.Log("FIRE2: " + boolValue);

		if ((boolValue = this.GetKeyReleased(Keys.FIRE3)))
			Debug.Log("FIRE3: " + boolValue);
	}


	public void DebugKeyReleasedPad() {
		bool boolValue;

		if ((boolValue = this.GetKeyReleasedPad(Keys.A_KEY)))
			Debug.Log("Get A_KEY : " + boolValue);
		if ((boolValue = this.GetKeyReleasedPad(Keys.B_KEY)))
			Debug.Log("Get B_KEY : " + boolValue);

		if ((boolValue = this.GetKeyReleasedPad(Keys.X_KEY)))
			Debug.Log("Get X_KEY : " + boolValue);

		if ((boolValue = this.GetKeyReleasedPad(Keys.Y_KEY)))
			Debug.Log("Get Y_KEY : " + boolValue);

		if ((boolValue = this.GetKeyReleasedPad(Keys.LB_KEY)))
			Debug.Log("Axis LB_KEY : " + boolValue);

		if ((boolValue = this.GetKeyReleasedPad(Keys.RB_KEY)))
			Debug.Log("Axis RB_KEY : " + boolValue);

		if ((boolValue = this.GetKeyReleasedPad(Keys.LJOY_KEY)))
			Debug.Log("LJOY_CLICK : " + boolValue);

		if ((boolValue = this.GetKeyReleasedPad(Keys.RJOY_KEY)))
			Debug.Log("RJOY_CLICK : " + boolValue);

		if ((boolValue = this.GetKeyReleasedPad(Keys.START_KEY)))
			Debug.Log("START_KEY : " + boolValue);

		if ((boolValue = this.GetKeyReleasedPad(Keys.SELECT_KEY)))
			Debug.Log("SELECT_KEY : " + boolValue);


	}
}
        