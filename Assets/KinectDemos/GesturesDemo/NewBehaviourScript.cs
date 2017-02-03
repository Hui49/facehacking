/// <summary>
///  
///  MR.yang in Beijng  Q: 275727971  2014/09/12 17:59
///  
/// </summary>/
using UnityEngine;
using System.Collections;

public class Kclick : MonoBehaviour {

	public Texture2D[] HandTex; 
	public float       HandSpeed;

	public GUITexture  HandCource;
	Vector2     HandPositon;
	Ray         ray;
	RaycastHit  hit;
	Vector3     HandToWorld;
	float       	   time;
	int 		       TexNumber;

	public GameObject  Cube;

	void Update () {

		HandPositon.x = Screen.width * HandCource.transform.position.x;
		HandPositon.y = Screen.height* HandCource.transform.position.y;

		HandToWorld = new Vector3( HandPositon.x, HandPositon.y, 5 );


		//体感控制
		#region
		if( Physics.Raycast(ray, out hit  ) ){

			//Debug.Log("OK!");


			if( hit.collider.gameObject.tag =="Button" ){


				time += Time.deltaTime;

				if( time > HandSpeed  ){

					time = 0;

					TexNumber++;	
					if( TexNumber < HandTex.Length    ){


						HandCource.texture = HandTex[TexNumber];						


					}else{


						switch( hit.collider.gameObject.name ){

						case "BUtton1"   :    if( Cube.activeSelf == true ){
								Cube.SetActive( false );
						}else{
							Cube.SetActive( true );
						}
						break;

						case "BUtton2"   :    if( Cube.activeSelf == true ){
								Cube.SetActive( false );
						}else{
							Cube.SetActive( true );
						}
						break;


						}
						TexNumber =0;

					}

				}

			}else{


				TexNumber =0;
				HandCource.texture = HandTex[TexNumber];	
			}

		}else{

			TexNumber =0;
			HandCource.texture = HandTex[TexNumber];	
		}


		#endregion



	}



}