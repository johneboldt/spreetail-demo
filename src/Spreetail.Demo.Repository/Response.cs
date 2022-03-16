using System;

namespace Spreetail.Demo.Repository
{
    /// <summary>
    /// Contains response information from an <see cref="IDictionaryRepository"/> call.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="error">An error message assoicated with the response or null/whitespace if there was no error.</param>
        public Response(string error)
        {
            Error = string.IsNullOrWhiteSpace(error) ? string.Empty : error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class with no error message.
        /// </summary>
        public Response()
        {
            Error = string.Empty;
        }

        /// <summary>
        /// Gets the error message associated with the response. Empty string indicates no error.
        /// </summary>
        public string Error {get;}

        /// <summary>
        /// Gets a value indicating whether there is an error associated with the response.
        /// </summary>
        public bool HasError => !string.IsNullOrWhiteSpace(Error);
    }
}