using ee.Core.Exceptions;

namespace ee.Core.Framework.Utiltity
{
    public class Validator
    {
        public static void Required(string value, string propertyName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EeException(ErrorCodes.InvalidParameter, $"{propertyName} is required.");
            }
        }
    }
}
