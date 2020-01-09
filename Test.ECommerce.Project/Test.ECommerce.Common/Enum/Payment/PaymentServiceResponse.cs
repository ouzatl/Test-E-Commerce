using System.Runtime.Serialization;

namespace Test.ECommerce.Common.Enum.Payment
{
    public enum PaymentServiceResponse
    {
        [EnumMember]
        Exception = 0,

        [EnumMember]
        Success = 1,

        [EnumMember]
        NotFound = -1,
    }
}