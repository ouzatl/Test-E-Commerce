using System;
using System.Runtime.Serialization;

namespace Test.ECommerce.Common.Enum
{
    [Serializable]
    public class ServiceResponse<StatusEnumT, DataT>
    {
        private StatusEnumT _status;
        private DataT _data;
        private string _message;

        public StatusEnumT Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public DataT Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public ServiceResponse()
        {
            _data = default(DataT);
        }

        public ServiceResponse(StatusEnumT status)
        {
            _data = default(DataT);
            _status = status;
        }

        public ServiceResponse(StatusEnumT status, DataT data)
        {
            _status = status;
            _data = data;
        }

        public ServiceResponse(StatusEnumT status, string message)
        {
            _data = default(DataT);
            _status = status;
            _message = message;
        }
    }
}