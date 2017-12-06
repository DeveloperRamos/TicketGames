using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace TicketGames.CrossCutting.Cryptography
{
    public class Hash
    {
        //Usar Enum para o cypherType
        //Lançar exception caso o cypherType não seja conhecido.
        public string GetHash(string value, string saltValue, CypherType cypher)
        {
            saltValue = (saltValue == null) ? string.Empty : saltValue;
            StringBuilder strSenhaHash = new StringBuilder();
            byte[] arrSenhaHash = null;
            switch (cypher)
            {
                case CypherType.SHA512:
                    SHA512 alg = SHA512.Create();

                    if (saltValue.Length > 0)
                        value = string.Concat(saltValue.Substring(0, 18), value, saltValue.Substring(18));

                    byte[] result = alg.ComputeHash(Encoding.ASCII.GetBytes(value));
                    arrSenhaHash = alg.ComputeHash(result);

                    foreach (byte b in result)
                    {
                        strSenhaHash.Append(b.ToString("X2"));
                    }
                    return strSenhaHash.ToString();

                case CypherType.SHA1:
                    HashAlgorithm AlgoritimoHash = new SHA1Managed();

                    byte[] arrSenhaBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
                    arrSenhaHash = AlgoritimoHash.ComputeHash(arrSenhaBytes);

                    foreach (byte b in arrSenhaHash)
                    {
                        strSenhaHash.Append(b.ToString("X2"));
                    }

                    return strSenhaHash.ToString();

                case CypherType.MD5:

                    string strSenhaMD5 = FormsAuthentication.HashPasswordForStoringInConfigFile(value, "md5");
                    return strSenhaMD5.ToLower();

                default:
                    SHA512 algoSha = SHA512.Create();

                    if (saltValue.Length > 0)
                        value = string.Concat(saltValue.Substring(0, 18), value, saltValue.Substring(18));

                    byte[] resultSha = algoSha.ComputeHash(Encoding.ASCII.GetBytes(value));
                    arrSenhaHash = algoSha.ComputeHash(resultSha);

                    foreach (byte b in resultSha)
                    {
                        strSenhaHash.Append(b.ToString("X2"));
                    }
                    return strSenhaHash.ToString();
            }
        }
    }
}
