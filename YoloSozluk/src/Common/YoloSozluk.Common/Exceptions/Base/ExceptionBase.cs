using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.Dtos;

namespace YoloSozluk.Common.Exceptions.Base
{
    public class ExceptionBase : Exception
    {
        public List<ErrorBaseDto> Errors { get; set; } = new List<ErrorBaseDto>();
        public int StatusCode = 500;

        public ExceptionBase(string message, int statusCode = 500) : base(message)
        {
            this.StatusCode = statusCode;
        }


        public ExceptionBase(IEnumerable<ErrorBaseDto> validationErrors, string message) : base(message)
        {
            foreach (var item in validationErrors)
            {
                this.Errors.Add(item);
            }
        }
        public ExceptionBase(Exception ex) : base(ex.Message)
        {
            this.Source = ex.Source;
        }
    }
}
