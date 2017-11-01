using System.Collections.Generic;

namespace OpenIdPoc.Web.Models
{
    public class DictionaryResponseModel
    {
        public List<DictionaryItem> DictionaryItems { get; set; }
        public UserData UserData { get; set; }
    }
}