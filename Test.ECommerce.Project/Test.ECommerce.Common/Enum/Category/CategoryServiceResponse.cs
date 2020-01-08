using System.Runtime.Serialization;

namespace Test.ECommerce.Common.Enum.Category
{
    public enum CategoryServiceResponse
    {
        [EnumMember]
        Exception = 0,

        [EnumMember]
        Success = 1,

        [EnumMember]
        NotFound = -1,
    }
}