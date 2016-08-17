using CQRSlite.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Domain.Commands
{
    public class BaseCommand : ICommand
    {
        /// <summary>
        /// The Aggregate ID of the Aggregate Root being changed
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Expected Version which the Aggregate will become.
        /// </summary>
        public int ExpectedVersion { get; set; }
    }
}
