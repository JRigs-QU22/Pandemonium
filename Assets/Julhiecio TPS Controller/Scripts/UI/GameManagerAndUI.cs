using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[AddComponentMenu("JU TPS/Gameplay/Game/Game Manager")]
public class GameManagerAndUI : MonoBehaviour
{
	
	[HideInInspector]public ThirdPersonController PlayerCharacter;

	[Header("UI Settings")]
	public bool IsMobile;
	public bool HiddenAllUiWhenDie;
	public bool HiddenUIWhenPressF2;
	public GameObject All_UI;
	public GameObject MobileUIPanel;
	public GameObject MobileButtonsPanel;
	public GameObject MobileButtonsDrivingPanel;
	public bool ShowInteractText;
	public Text InteractText;
	[HideInInspector] private Touchfield MobileTouchfield;
	[HideInInspector] private bool ControlMobilePanels = false;

	[Header("Crosshair Information")]
	public bool ScaleCrosshairWithPrecision;
	public bool HiddenWhenDriving;
	public Image Crosshair;
	[Range(0.01f, 0.5f)]
	public float CrosshairSensibility = 0.15f;
	Vector3 CrosshairStartSize;
	float CurrentSize;

	public Image Scope;

	[Header("Weapon Information")]
	public bool DisplayWeaponInformation;
	public Text WeaponName;
	public Text BulletsCount;

	[Header("Player Stats")]
	public bool DisplayHeatlhBar;
	public Image HealthBar;

	[Header("Weapon UI")]
	public Image smg;
	public Image pistol;
	public Image shotgun;
	private Image weaponUI;

	public ThirdPersonController tps;
	public GameObject crosshairdot;

	// addition

	void Start()
	{
		CrosshairStartSize = Crosshair.transform.localScale;
		CurrentSize = CrosshairStartSize.x;
		if (PlayerCharacter == null)
		{
			PlayerCharacter = FindObjectOfType<ThirdPersonController>();
		}
		Invoke("enable_mobile_panels_controll", 0.1f);
		MobileTouchfield = GameObject.Find("Touch").GetComponent<Touchfield>();
		MobileTouchfield.gameObject.SetActive(IsMobile);

		smg.gameObject.SetActive(false);
		pistol.gameObject.SetActive(false);
		shotgun.gameObject.SetActive(false);
	}

	void Update()
	{
		ControllCrosshair();

		DisplayWeaponInfo();

		DisplayHealth();

		MobileUI();

		if (PlayerCharacter.IsDead == true && HiddenAllUiWhenDie == true)
		{
			//this.gameObject.SetActive(false);
			All_UI.gameObject.SetActive(false);
		}

        if (Input.GetKeyDown(KeyCode.F2) && HiddenUIWhenPressF2 == true)
        {
			All_UI.SetActive(!All_UI.activeInHierarchy);
        }
	

		if (ShowInteractText == true)
		{
			if (PlayerCharacter.ToPickupWeapon == true)
			{
				InteractText.text = "Press [F] to pick up weapon";
			}
			if (PlayerCharacter.ToEnterVehicle == true)
			{
				InteractText.text = "Press [F] to drive the vehicle";
			}
			if (PlayerCharacter.ToEnterVehicle == false && PlayerCharacter.ToPickupWeapon == false )
			{
				InteractText.text = "";
			}
		}

		if (tps.WeaponID >= 0)
		{
			crosshairdot.SetActive(false);
		}
        else if(tps.WeaponID == -1)
		{
			crosshairdot.SetActive(true);
		}
		
	}
	
	public void MobileUI()
    {
		if (ControlMobilePanels == true)
		{
			if (IsMobile == false)
			{
				MobileUIPanel.SetActive(false);
			}
			else
			{
				MobileButtonsPanel.SetActive(!PlayerCharacter.IsDriving);
				MobileButtonsDrivingPanel.SetActive(PlayerCharacter.IsDriving);
			}
		}
    }
	public void ControllCrosshair()
    {
		if (ScaleCrosshairWithPrecision == false)
			return;
		//Crosshair Size
		CurrentSize = Mathf.Lerp(CurrentSize, CrosshairStartSize.x, 5 * Time.deltaTime);
		Vector3 crosshairsize = new Vector3(CurrentSize, CurrentSize, CurrentSize);
		Crosshair.transform.localScale = crosshairsize;

		if (PlayerCharacter.IsArmed)
		{
			//Shot increase the crosshair scale
			CurrentSize = CurrentSize + 3*PlayerCharacter.WeaponInUse.ShotErrorProbability * CrosshairSensibility;
			if(PlayerCharacter.IsAiming && PlayerCharacter.WeaponInUse.AimMode == Weapon.WeaponAimMode.Scope && PlayerCharacter.WeaponInUse.ScopeTexture != null)
            {
				Scope.sprite = PlayerCharacter.WeaponInUse.ScopeTexture;
				Scope.gameObject.SetActive(true);
            }
            else
            {
				Scope.gameObject.SetActive(false);
			}
        }
        else
        {
			Scope.gameObject.SetActive(false);
		}
		//Hidden crosshair when driving
		if (HiddenWhenDriving && PlayerCharacter.IsAiming == false)
		{
			Crosshair.gameObject.SetActive(!PlayerCharacter.IsDriving);
		}else if (HiddenAllUiWhenDie)
        {
			Crosshair.gameObject.SetActive(false);
		}
	}
	
	public void DisplayWeaponInfo()
    {
		if (DisplayWeaponInformation == false)
			return;
		if (PlayerCharacter.WeaponInUse != null && DisplayWeaponInformation)
		{
			if (PlayerCharacter.WeaponInUse.WeaponSwitchID == 0)
			{
				weaponUI = pistol;
				pistol.gameObject.SetActive(true);
				smg.gameObject.SetActive(false);
				shotgun.gameObject.SetActive(false);
			}
			else if (PlayerCharacter.WeaponInUse.WeaponSwitchID == 1)
			{
				weaponUI = smg;
				smg.gameObject.SetActive(true);
				pistol.gameObject.SetActive(false);
				shotgun.gameObject.SetActive(false);
			}
			else if (PlayerCharacter.WeaponInUse.WeaponSwitchID == 2)
			{
				weaponUI = shotgun;
				shotgun.gameObject.SetActive(true);
				smg.gameObject.SetActive(false);
				pistol.gameObject.SetActive(false);
			}
			//Bullets counts
			if (PlayerCharacter.WeaponInUse.TotalBullets > 0 || PlayerCharacter.WeaponInUse.BulletsAmounts > 0)
			{
				BulletsCount.text = PlayerCharacter.WeaponInUse.BulletsAmounts + ""; 
				//+ "/" + PlayerCharacter.WeaponInUse.TotalBullets
				BulletsCount.color = Color.white;
				float rvalue = PlayerCharacter.WeaponInUse.BulletsAmounts/10f;
				float gvalue = PlayerCharacter.WeaponInUse.BulletsAmounts/10f;
				
                if (PlayerCharacter.Shot)
                {
					rvalue = rvalue * 4f;
					gvalue = gvalue * 4f;
				}
				Color gunColor = new Color(1 - rvalue, 0 + gvalue, 0);
				//Debug.Log(rvalue);
				weaponUI.color = gunColor;
			}
			if (PlayerCharacter.WeaponInUse.TotalBullets <= 0 && PlayerCharacter.WeaponInUse.BulletsAmounts <= 0)
			{
				BulletsCount.text = "No ammo";
				BulletsCount.color = Color.red;
				weaponUI.color = Color.red;
			}
			//Weapon name
			WeaponName.text = PlayerCharacter.WeaponInUse.WeaponName;
			
			
		}
		else
		{
			//Bullets counts
			BulletsCount.text = "";

			//Weapon name
			WeaponName.text = "";

			smg.gameObject.SetActive(false);
			pistol.gameObject.SetActive(false);
			shotgun.gameObject.SetActive(false);
		}
	}
	public void DisplayHealth()
    {
		if (DisplayHeatlhBar == false)
			return;
		//LifeBar
		HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, PlayerCharacter.Health / 100, 10 * Time.deltaTime);
	}

	private void enable_mobile_panels_controll()
    {
		ControlMobilePanels = true;
    }
	public void Menu()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
