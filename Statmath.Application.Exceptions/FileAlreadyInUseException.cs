using System;

namespace Statmath.Application.Exceptions
{
    [Serializable]
    public class FileAlreadyInUseException : Exception
    {
        public FileAlreadyInUseException()
        {
        }

        public FileAlreadyInUseException(string filePath) :
            base(@$"The file {filePath} is in use by another process")
        {
        }
    }
}
