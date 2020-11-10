
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SLogger.Data
{
    public class CustomerEfIInterceptor : DbCommandInterceptor
    {
        public Exception WrapEntityFrameworkException(DbCommand command, Exception ex)
        {
            var newEx = new Exception("Entityframework command failed.", ex);
            AddParamsToException(newEx, command.Parameters);
            return newEx;
        }
        
        private void AddParamsToException(Exception ex, DbParameterCollection parameters)
        {
            foreach(DbParameter p in parameters)
            {
                ex.Data.Add(p.ParameterName, p.Value.ToString());
            }
        }

        public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            base.CommandFailed(command, eventData);
        
            WrapEntityFrameworkException(command, eventData.Exception);
        }

    }
}
