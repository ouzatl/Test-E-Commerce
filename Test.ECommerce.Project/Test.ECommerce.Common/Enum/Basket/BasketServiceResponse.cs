using System.Runtime.Serialization;

namespace Test.ECommerce.Common.Enum.Basket
{
    public enum BasketServiceResponse
    {
        [EnumMember]
        Exception = 0,

        [EnumMember]
        Success = 1,

        [EnumMember]
        NotFound = -1,

        [EnumMember]
        MaxStock = -2,
    }
}