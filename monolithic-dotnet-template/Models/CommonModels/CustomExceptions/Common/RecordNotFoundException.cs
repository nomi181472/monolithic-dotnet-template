

namespace CustomExceptions.Common
{
    [Serializable]
    public class RecordNotFoundException:Exception
    {
        public RecordNotFoundException()
        {


        }
        public RecordNotFoundException(string m):base(m)
        {

        }

    }
}
