using System;
using UnityEngine;  

using System.Data;  
using System.Collections;   
using MySql.Data.MySqlClient;
using MySql.Data;
using System.IO;

	public class Character
{	

	private int ID;
	private String name; 

	//[SerializeField]
	//private GameObject m_object;

	public Character (int id)
	{	
	
		SqlAccess sql = new  SqlAccess();
		DataSet ds  = sql.SelectWhere(id);
		if(ds != null)
		{

			DataTable table = ds.Tables[0];
			name = table.Rows[0][0].ToString();



			//	return name;
		}

		
		}
	public string getname ()
	{
		
		return name;
		//	return name;
	}}

