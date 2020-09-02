using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	public float speed = 20f;
	public float damage = 50;
	public float ExplodeDamage;
	public float explosionRadius = 0f;
	public bool enableblood = false;
	public GameObject BloodEffect;
	public bool isIceDamage;

	private enemy e;

    private Vector3 targetPoint;
	private Quaternion targetRotation;
	public GameObject ExplodeEffect;

	public GameObject SoundEffect;
	public bool FrozenBullet;
	public float FreezeTime;
	public float OfflineIncreseDamageRate;
	public GameObject FrozeEffect;
	public GameObject BurnEffectPrefab;


	public void Start()
	{
		Vector3 temp = transform.position;
		temp.z = -1;
		transform.position = temp;
	}
	
	public void seek (Transform _target)
	{
		target = _target;
	}

	// Update is called once per frame
	void Update () {


		if (target == null || target.tag == "Untagged")
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;
		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}

	void HitTarget ()
	{
		if (explosionRadius > 0f)
		{
			Damage(target);
			Explode(target);
		} else if(FrozenBullet)
		{	
		//	Damage(target);
			Froze(target);
		} else
		{
			Damage(target);
		}
		if(enableblood)
		{
			GameObject Blood = Instantiate(BloodEffect,transform.position,Quaternion.Euler(new Vector3(0, 0, 0)));
			Vector3 temp = transform.position;
			temp.z = -5;
			Blood.transform.position = temp;
			Destroy(Blood,2f);
		}

		Destroy(gameObject);
	}

	void Explode (Transform TransformEnemy)
	{
		e = TransformEnemy.GetComponent<enemy>();
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
		foreach (Collider2D collider in colliders)
		{
			
			if (collider.tag == "enemy" & collider.GetComponent<enemy>() != e )
			{
				if(collider.GetComponent<enemy>().GetCanAvoidExplode())
					Debug.Log("AvoidExplode");
				else
					ExplosionDamage(collider.transform);
			}
		}
	}

	void Froze (Transform TransformEnemy)
	{
		e = TransformEnemy.GetComponent<enemy>();
		if(e.hasSpeedUp())
			return;
		Vector3 Position = transform.position + new Vector3(0,2.3f,0);
		GameObject Ice = Instantiate(FrozeEffect,Position,Quaternion.identity);
		Ice.transform.parent = TransformEnemy;
		e.Stop(FreezeTime,OfflineIncreseDamageRate,Ice);
	}

	void Damage (Transform TransformEnemy)
	{
		e = TransformEnemy.GetComponent<enemy>();
		if (e != null)
		{
			if(explosionRadius > 0)		
			{
				GameObject BurnEffect = Instantiate(BurnEffectPrefab,e.transform.position,Quaternion.identity);
				BurnEffect.transform.parent = e.transform;
			//	GameObject ExplodeEffectInfo = Instantiate (ExplodeEffect,TransformEnemy.position,Quaternion.identity);
			//	Destroy(ExplodeEffectInfo,5f);
			}
			e.TakeDamage(enableblood,isIceDamage,damage,0);
		}
	}

	void ExplosionDamage (Transform Enemy )
	{
		e = Enemy.GetComponent<enemy>();
		if (e != null)
		{
			GameObject BurnEffect = Instantiate(BurnEffectPrefab,Enemy.position,Quaternion.identity);
			BurnEffect.transform.parent = Enemy;
			if(e.isFly)
				e.TakeDamage(enableblood,isIceDamage,ExplodeDamage/3,0);
			else
				e.TakeDamage(enableblood,isIceDamage,ExplodeDamage,0);
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
