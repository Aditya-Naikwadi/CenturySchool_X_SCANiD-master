using System;
using System.Security.Cryptography;
using System.Text;

namespace CenturyRayonSchool.Model
{
    public class PaymentGatewayModule
    {
        // public string secret_key = "1300012494405020";
        public string secret_key = "3848171326401000";


        public string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(inputString);

            byte[] hash = sha512.ComputeHash(bytes);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i <= hash.Length - 1; i++)
            {

                stringBuilder.Append(hash[i].ToString("x2"));

            }

            return stringBuilder.ToString();
        }

        public string encryptFile(string textToEncrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.ECB;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;

            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }

            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;

            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();

            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));

        }

    }


    public class TransactionMode
    {
        public string merchantid { get; set; }
        public string merchantid_encrypt { get; set; }
        public string reference_no { get; set; }
        public string reference_no_encrypt { get; set; }
        public string sub_merchantid { get; set; }
        public string sub_merchantid_encrypt { get; set; }
        public string pgamount { get; set; }
        public string pgamount_encrypt { get; set; }
        public string fee_rcptid { get; set; }
        public string fee_rcptid_encrypt { get; set; }
        public string receiptdate { get; set; }
        public string receiptdate_encrypt { get; set; }
        public string studentname { get; set; }
        public string studentname_encrypt { get; set; }
        public string grno { get; set; }
        public string cid { get; set; }
        public string grno_encrypt { get; set; }

        public string std { get; set; }
        public string div { get; set; }
        public string stddiv { get { return std + "-" + div; } }
        public string stddiv_encrypt { get; set; }

        public string academicyear { get; set; }
        public string academicyear_encrypt { get; set; }

        public string mobileno { get; set; }
        public string emailid { get; set; }
        public string mobileno_encrypt { get; set; }
        public string emailid_encrypt { get; set; }

        public string mandatory_field
        {
            get
            {
                return reference_no + "|" + sub_merchantid + "|" + pgamount + "|" + fee_rcptid + "|" + receiptdate + "|" + studentname + "|" + grno + "|" + stddiv + "|" + academicyear + "|" + mobileno;
            }
        }
        public string mandatory_field_New
        {
            get
            {
                return reference_no + "|" + sub_merchantid + "|" + pgamount + "|" + academicyear + "|" + studentname + "|" + grno + "|" + stddiv + "|" + mobileno + "|" + emailid;
            }
        }

        public string mandatory_field_encrypt { get; set; }
    }
}