using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnitMover))]
[RequireComponent(typeof(UnitWeapon))]
[RequireComponent(typeof(UnitHealth))]
public class Unit : MonoBehaviour
{
    public UnitMover mover;
    public Unit target;
    public float gunCooldown;
    public Squad squad;
    public UnitWeapon weapon;

	public GameObject muzzleEffect_prefab;
	public GameObject tracerEffect_prefab;

	public GameObject muzzleEffect;
	public GameObject tracerEffect;

    public enum Class { Rifleman, HeavyAssault, Sniper, MG, Commander }
    public Class unitClass = Class.Rifleman;

    public Image imgClass1;
    public Image imgClass2;
    public Image imgClass3;
    public Image imgClass4;

    public Sprite class1;
    public Sprite class2;
    public Sprite class3;
    public Sprite class4;

    public Cover cover;

    void Awake()
    {
        mover = GetComponent<UnitMover>();
        squad = transform.parent.GetComponent<Squad>();
        weapon = GetComponent<UnitWeapon>();

		muzzleEffect = Instantiate(muzzleEffect_prefab, this.transform.GetChild(1) // hips
			.GetChild(2) // spine
			.GetChild(0) //spine 1
			.GetChild(0) //spine 2
			.GetChild(2) //right should
			.GetChild(0) // right arm
			.GetChild(0) // rightfore arm
			.GetChild(0) //right hand
			.GetChild(5));

		tracerEffect = Instantiate(tracerEffect_prefab, this.transform.GetChild(1) // hips
			.GetChild(2) // spine
			.GetChild(0) //spine 1
			.GetChild(0) //spine 2
			.GetChild(2) //right should
			.GetChild(0) // right arm
			.GetChild(0) // rightfore arm
			.GetChild(0) //right hand
			.GetChild(5));

		//Set unit muzzle color
		switch (unitClass) {
		case Class.Rifleman:
			{
				tracerEffect.GetComponent<ParticleSystem>().startColor = Color.red;
				muzzleEffect.GetComponent<ParticleSystem>().startColor = Color.red;

				break;
			}
		case Class.HeavyAssault:
			{
				tracerEffect.GetComponent<ParticleSystem>().startColor = Color.cyan;
				muzzleEffect.GetComponent<ParticleSystem>().startColor = Color.cyan;

				break;
			}
		case Class.Sniper:
			{
				tracerEffect.GetComponent<ParticleSystem>().startColor = Color.magenta;
				muzzleEffect.GetComponent<ParticleSystem>().startColor = Color.magenta;

				break;
			}
		case Class.MG:
			{
				tracerEffect.GetComponent<ParticleSystem>().startColor = Color.green;
				muzzleEffect.GetComponent<ParticleSystem>().startColor = Color.green;

				break;
			}
		case Class.Commander:
			{
				tracerEffect.GetComponent<ParticleSystem> ().startColor = Color.yellow;
				muzzleEffect.GetComponent<ParticleSystem>().startColor = Color.yellow;

				break;
			}
		}

        ChangeImg();
    }

    void Start()
    {
        ChangeImg();

        if (unitClass == Class.HeavyAssault)
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().stoppingDistance = 20f;
        }
    }

    void Update()
    {
        gunCooldown -= Time.deltaTime;
        gunCooldown = Mathf.Max(gunCooldown, 0);
    }

    public void SetTarget(Unit target)
    {
        this.target = target;
    }

    public void SetImmediateMoveTarget(Vector3 target)
    {
        mover.SetTarget(target);
    }

    public void ChangeImg()
    {
        if (unitClass == Unit.Class.MG)
        {
            imgClass1.enabled = true;
            imgClass2.enabled = true;

            if (squad.team == 1)
            {
                imgClass1.color = Color.red;
                imgClass2.color = Color.red;
            }

            imgClass1.rectTransform.sizeDelta = new Vector2(200, 500);
            imgClass2.rectTransform.sizeDelta = new Vector2(500, 200);

            imgClass1.overrideSprite = class1;
            imgClass2.overrideSprite = class2;
        }

        if (unitClass == Unit.Class.Sniper)
        {
            imgClass1.enabled = true;
            imgClass2.enabled = true;
            imgClass3.enabled = true;

            if (squad.team == 1)
            {
                imgClass1.color = Color.red;
                imgClass2.color = Color.red;
            }

            imgClass1.rectTransform.sizeDelta = new Vector2(500, 500);
            imgClass2.rectTransform.sizeDelta = new Vector2(350, 350);
            imgClass3.rectTransform.sizeDelta = new Vector2(250, 250);

            imgClass1.overrideSprite = class1;
            imgClass2.overrideSprite = class2;
            imgClass3.overrideSprite = class3;

        }

        if (unitClass == Unit.Class.Rifleman)
        {
            imgClass1.enabled = true;
            imgClass2.enabled = true;

            if (squad.team == 1)
            {
                imgClass1.color = Color.red;
                imgClass2.color = Color.red;
            }

            imgClass1.rectTransform.sizeDelta = new Vector2(100, 500);
            imgClass2.rectTransform.sizeDelta = new Vector2(100, 500);

            imgClass1.rectTransform.rotation = Quaternion.Euler(0, 0, 45);
            imgClass2.rectTransform.rotation = Quaternion.Euler(0, 0, -45);

            imgClass1.overrideSprite = class1;
            imgClass2.overrideSprite = class2;
        }

        if (unitClass == Unit.Class.HeavyAssault)
        {
            imgClass1.enabled = true;
            imgClass2.enabled = true;
            imgClass3.enabled = true;
            imgClass4.enabled = true;

            if (squad.team == 1)
            {
                imgClass1.color = Color.red;
                imgClass2.color = Color.red;
            }

            imgClass1.rectTransform.sizeDelta = new Vector2(150, 500);
            imgClass2.rectTransform.sizeDelta = new Vector2(150, 500);
            imgClass3.rectTransform.sizeDelta = new Vector2(150, 500);
            imgClass4.rectTransform.sizeDelta = new Vector2(150, 500);

            imgClass1.rectTransform.localPosition = new Vector3(75, 0, 0);
            imgClass2.rectTransform.localPosition = new Vector3(-375, 0, 0);
            imgClass3.rectTransform.localPosition = new Vector3(-75, 0, 0);
            imgClass4.rectTransform.localPosition = new Vector3(-225, 0, 0);

            imgClass1.overrideSprite = class1;
            imgClass2.overrideSprite = class2;
            imgClass3.overrideSprite = class3;
            imgClass4.overrideSprite = class4;
        }

        if (unitClass == Unit.Class.Commander)
        {
            imgClass1.enabled = true;
            imgClass2.enabled = true;
            imgClass3.enabled = true;
            imgClass4.enabled = true;

            if (squad.team == 1)
            {
                imgClass1.color = Color.red;
                imgClass2.color = Color.red;
            }

            imgClass1.rectTransform.sizeDelta = new Vector2(250, 250);
            imgClass2.rectTransform.sizeDelta = new Vector2(250, 250);
            imgClass3.rectTransform.sizeDelta = new Vector2(250, 250);
            imgClass4.rectTransform.sizeDelta = new Vector2(250, 250);

            imgClass1.rectTransform.localPosition = new Vector3(150, 150, 0);
            imgClass2.rectTransform.localPosition = new Vector3(-150, -150, 0);
            imgClass3.rectTransform.localPosition = new Vector3(-150, 150, 0);
            imgClass4.rectTransform.localPosition = new Vector3(150, -150, 0);

            imgClass1.overrideSprite = class1;
            imgClass2.overrideSprite = class2;
            imgClass3.overrideSprite = class3;
            imgClass4.overrideSprite = class4;
        }
    }
}
