

namespace CustomExceptions.Common
{
    [Serializable]
    public class RecordAlreadyExistException:Exception
    {
        public RecordAlreadyExistException()
        {

        }
        public RecordAlreadyExistException(string m):base(m)
        {

        }
    }
    

}
