using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pk.Com.Jazz.ECP.Models
{
    public partial class AppUserToken
    {
        public AppUserToken()
        {
            EntryDate = DateTime.Now;
        }

        [Display(Name = "Token Number", ShortName = "Id")]
        public int AppUserTokenId { get; set; }

        [Display(Name = "SignIn Email", ShortName = "Email"), DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "-none-")]
        public string UserAdLogin { get; set; }

        [Display(Name = "Secret Token", ShortName = "Token"), DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "-none-")]
        public string Token { get; set; }

        [Display(Name = "Token Type", ShortName = "Type"), DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "-none-")]
        public string TokenType { get; set; }

        [Display(Name = "Request Date", ShortName = "Date")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }

        [Display(Name = "User", ShortName = "User")]
        public string UserAdLoginShort
        {
            get { return UserAdLogin.Replace("@jazz.com.pk", ""); }
        }

        [Display(Name = "Token", ShortName = "Token")]
        public string DisplayName
        {
            get { return $"{UserAdLoginShort} ({TokenType})"; }
        }

        [Display(Name = "Token", ShortName = "Token")]
        public string Reference
        {
            get { return $"{AppUserTokenId}-{UserAdLoginShort}"; }
        }
        public static Dictionary<string, string> Types()
        {
            throw new NotImplementedException();
        }

        public static Dictionary<string, string> SearchTypes()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            keyValuePairs.Add("UserAdLogin", "Email");
            keyValuePairs.Add("TokenType", "Type");

            return keyValuePairs;
        }

        public static string HeaderRow(char delimiter)
        {
            throw new NotImplementedException();
        }

        public string DataRow(char delimiter)
        {
            throw new NotImplementedException();
        }
    }
}
