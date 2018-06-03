using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//弹痕融合
[RequireComponent(typeof(ObjectPool))]
public class BulletMark : MonoBehaviour {

	private ObjectPool pool;	//对象池

	private Transform effectParent;	//特效资源管理父物体

	private Texture2D m_BulletMark;	//弹痕贴图
	private Texture2D m_MainTexture;//主贴图
	private Texture2D m_MainTextureBackup;//主贴图备份
	private GameObject prefab_Effect;	//弹痕特效

	[SerializeField]
	private MaterialType  materialType;//模型自身材质

	private Queue<Vector2> bulletMarkQueue = null;	//弹痕队列

    [SerializeField]private int hp;     //临时测试生命值

    public int Hp
    {
        get { return hp; }
        set {
            hp = value;
            if(hp <= 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }



	// Use this for initialization
	void Start () {

		switch(materialType)
		{
			case MaterialType.Metal:
				//m_BulletMark = Resources.Load<Texture2D>("Gun/BulletMarks/Bullet Decal_Metal");	
				//prefab_Effect = Resources.Load<GameObject>("Effects/Gun/Bullet Impact FX_Metal");
				//effectParent = GameObject.Find("TempObject/Effect_Metal_Parent").GetComponent<Transform>();
				ResourcesLoad("Bullet Decal_Metal", "Bullet Impact FX_Metal", "Effect_Metal_Parent");
				break;
			case MaterialType.Stone:
				/*m_BulletMark = Resources.Load<Texture2D>("Gun/BulletMarks/Bullet Decal_Stone");
				prefab_Effect = Resources.Load<GameObject>("Effects/Gun/Bullet Impact FX_Stone");	
				effectParent = GameObject.Find("TempObject/Effect_Stone_Parent").GetComponent<Transform>();*/
				ResourcesLoad("Bullet Decal_Stone", "Bullet Impact FX_Stone", "Effect_Stone_Parent");
				break;
			case MaterialType.Wood:
				/*m_BulletMark = Resources.Load<Texture2D>("Gun/BulletMarks/Bullet Decal_Wood");
				prefab_Effect = Resources.Load<GameObject>("Effects/Gun/Bullet Impact FX_Wood");	
				effectParent = GameObject.Find("TempObject/Effect_Wood_Parent").GetComponent<Transform>();*/
				ResourcesLoad("Bullet Decal_Wood", "Bullet Impact FX_Wood", "Effect_Wood_Parent");
				break;
		}
		if(gameObject.GetComponent<ObjectPool>() == null)
			pool = gameObject.AddComponent<ObjectPool>();
		else
			pool = gameObject.GetComponent<ObjectPool>();
		
		m_MainTexture = (Texture2D)gameObject.GetComponent<MeshRenderer>().material.mainTexture;
		m_MainTextureBackup = GameObject.Instantiate<Texture2D>(m_MainTexture);
		bulletMarkQueue = new Queue<Vector2>();
	}

	//资源加载
	private void ResourcesLoad(string bulletMark, string effect, string parent)
	{
		m_BulletMark = Resources.Load<Texture2D>("Gun/BulletMarks/" + bulletMark);
		prefab_Effect = Resources.Load<GameObject>("Effects/Gun/" + effect);
		effectParent = GameObject.Find("TempObject/" + parent).GetComponent<Transform>();
	}
	
	//弹痕融合
	// Update is called once per frame
	public void CreateBulletMark(RaycastHit hit)
	{
		//textureCoord:贴图uv坐标点
		Vector2 uv = hit.textureCoord;
		//将uv添加到队列
		bulletMarkQueue.Enqueue(uv);
		
		//宽度 横向 x轴
		for(int i=0;i<m_BulletMark.width; i++)
		{
			//高度 纵向 y轴
			for(int j=0;j<m_BulletMark.height; j++)
			{
				//uv.x * 主贴图宽度 - 弹痕贴图宽度/2 + i
				float x = uv.x * m_MainTexture.width - m_BulletMark.width/2 + i;
				//uv.y * 主贴图高度 - 弹痕贴图高度/2 + j
				float y = uv.y * m_MainTexture.height - m_BulletMark.height/2 + j;
				//获取到弹痕贴图上的颜色.
				Color color = m_BulletMark.GetPixel(i,j);
				//主贴图位置融合弹痕贴图的颜色
				if(color.a>=0.2f)
				{
					m_MainTexture.SetPixel((int)x,(int)y,color);
				}
			}
		}
		m_MainTexture.Apply();
		//播放特效
		PlayEffect(hit);


		//自动10s后消除
		Invoke("RemoveBulletMark",10);
	}

	//弹痕移除
	private void RemoveBulletMark()
	{
		if(bulletMarkQueue.Count>0)
		{
			Vector2 uv = bulletMarkQueue.Dequeue();
			for(int i=0;i<m_BulletMark.width; i++)
			{
				//高度 纵向 y轴
				for(int j=0;j<m_BulletMark.height; j++)
				{
					float x = uv.x * m_MainTexture.width - m_BulletMark.width/2 + i;
					float y = uv.y * m_MainTexture.height - m_BulletMark.height/2 + j;
					//获取到弹痕贴图上的颜色.
					Color color = m_MainTextureBackup.GetPixel((int)x,(int)y);
					//主贴图位置融合弹痕贴图的颜色
					//if(color.a>=0.2f)
					m_MainTexture.SetPixel((int)x,(int)y,color);
					
				}
			}
			m_MainTexture.Apply();
		}
	}

	//播放特效
	private void PlayEffect(RaycastHit hit)
	{
		GameObject effect = null;
		if(pool.Data()){
			//取出数据使用
			effect = pool.GetObject();
			effect.GetComponent<Transform>().position = hit.point;
            effect.GetComponent<Transform>().rotation = Quaternion.LookRotation(hit.normal);
		}
		else{
			//创建新的
			effect = GameObject.Instantiate<GameObject>(prefab_Effect,hit.point,Quaternion.LookRotation(hit.normal),effectParent);	
			effect.name = "Effect_" + materialType;
			StartCoroutine(Delay(effect,1));
		}

	}

	private IEnumerator Delay(GameObject go, float time)
	{
		yield return new WaitForSeconds(time);
		pool.AddObject(go);
	}


}
