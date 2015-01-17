using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

//This sample code shows how to use the CAPICOM's encryption object to encrypt and decrypt a field (or a string).
//Make sure the solution is referencing the CAPICOM dll.
//This class exposes 2 methods that encrypt and decrpyt a string.

namespace RealpageData
{
	/// <summary>
	/// Summary description for Engine.
	/// </summary>
	public class Engine
	{
		#region Constructors
        
		public Engine(string pwd)
		{
		    Pwd = pwd;
		}
		
		#endregion
		
		#region Private Members

		#region Properties

	    private string Pwd { get; set; }

		#endregion

		#region Methods


		#endregion

		#endregion

		#region Public Members

		#region Properties


		#endregion

		#region Methods
		
		public string Decrypt(string input)
		{
			//Create an instance of the CAPICOM EncryptedDataClass class
			CAPICOM.EncryptedData ed = new CAPICOM.EncryptedDataClass();

			//Set the Algorithm name to 3DES since this is what Realpage uses
			ed.Algorithm.Name = CAPICOM.CAPICOM_ENCRYPTION_ALGORITHM.CAPICOM_ENCRYPTION_ALGORITHM_3DES;

			//Set the Secret password. This is the key that the encryption object would use to encrypt
			//and decrypt the string.
            //ed.SetSecret("Bu7phF153A#", CAPICOM.CAPICOM_SECRET_TYPE.CAPICOM_SECRET_PASSWORD);
            ed.SetSecret(Pwd, CAPICOM.CAPICOM_SECRET_TYPE.CAPICOM_SECRET_PASSWORD);

			//Call the Decrypt method
			ed.Decrypt(input);

			//The decrypted value is stored in the Content Property
			return ed.Content;			
		}

		public string Encrypt(string input)
		{
			//Test input
			//string input = "4046649371";

			//Create an instance of the CAPICOM EncryptedDataClass class
			CAPICOM.EncryptedData ed = new CAPICOM.EncryptedDataClass();

			//Set the Secret password. This has to be the same for both encryption and decryption, or else 
			//the string wont be decrypted.
            //ed.SetSecret("Bu7phF153A#", CAPICOM.CAPICOM_SECRET_TYPE.CAPICOM_SECRET_PASSWORD);
            ed.SetSecret(Pwd, CAPICOM.CAPICOM_SECRET_TYPE.CAPICOM_SECRET_PASSWORD);

			//Set the Algorithm. Decryption has to use the same algorithm
			ed.Algorithm.Name = CAPICOM.CAPICOM_ENCRYPTION_ALGORITHM.CAPICOM_ENCRYPTION_ALGORITHM_3DES;

			//Set the Content property to the value that needs to be encrypted
			ed.Content = input;
			
			//Call the Encrypt method to encrypt the string
			return ed.Encrypt(CAPICOM.CAPICOM_ENCODING_TYPE.CAPICOM_ENCODE_ANY);
		}

		#endregion

		#endregion

	}
}
