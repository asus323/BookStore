using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Validations
{
    public class BannedKeywordAttribute : ValidationAttribute
    {
        public BannedKeywordAttribute()
        {
            BannedKeywords = new List<string>()
            {
                "shit", "fuck"
            };
        }
        public List<string> BannedKeywords { get; set; }
        public override string FormatErrorMessage(string name)
        {
            return "لطفا از کلمات ممنوعه در عنوان استفاده نکنید";
        }

        public override bool IsValid(object value)
        {
            var title = (string)value;
            if (BannedKeywords.Contains(title.ToLower())) return false;
            return true;
        }
    }
}